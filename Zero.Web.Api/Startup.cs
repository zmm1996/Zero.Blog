using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Zero.Core.Intefaces;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;
using Zero.Infrastructure.Repository.Sys;
using Zero.Infrastructure.Resources.FluentValidation.Rabc;
using Zero.Infrastructure.Resources.ViewModels;
using Zero.Web.Api.Auth;
using Zero.Web.Api.Extensions;
using Zero.Web.Api.Filters;
using Zero.Web.Util.Extensions.AuthContext;

namespace Zero.Web.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //跨域1
            services.AddCors(o =>
            {
                o.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin()//允许所有
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();//
                });
            });

            services.AddHttpContextAccessor();

            //获取连接字符串
            string connStr = _configuration["ConnectionStrings:DefaultConnection"];
            // _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(connStr);
            });

            //身份认证
            services.AddAuthentication();


            //注入配置文件
            var appSettingSection = _configuration.GetSection("AppSettings");
            services.Configure<AppAuthenticationSettings>(appSettingSection);

            //JWT认证
            var appSettings = appSettingSection.Get<AppAuthenticationSettings>();//将配置文件转为实体自己用
            services.AddJwtBearerAuthentication(appSettings);


            services.AddAutoMapper(typeof(MappingProfile));//使用Automapper映射

            services.AddMvc(options =>
            {
                //全局过滤器
                //options.Filters.Add<CustomAuthorization>();xs
                //modelstate验证
                options.Filters.Add<ValidateModelAttribute>();

            }).AddJsonOptions(options =>
            {
                //返回json格式
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }).AddFluentValidation();

            //推荐加上
            services.AddHsts(option =>
            {
                option.Preload = true;
                option.IncludeSubDomains = true;
                option.MaxAge = TimeSpan.FromDays(60);
                option.ExcludedHosts.Add("example.com");
                option.ExcludedHosts.Add("www.example.com");

            });
            //将http转为https
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5001;

            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info()
                {
                    Title = "权限管理Api",
                    Version = "v1",
                    Description = "ASP.NET CORE WebApi",
                    Contact = new Contact
                    {
                        Name = "zmm",
                        Email = "145897656@qq.com"
                    }


                });
                // 使用反射获取xml文件。并构造出文件的路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                options.IncludeXmlComments(xmlPath, true);

                //这里是给Swagger添加验证的部分
                options.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "请输入带有Bearer的Token", Name = "Authorization", Type = "apiKey" });
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                       {
                           "Bearer",
                           Enumerable.Empty<string>()
                       }
                    });
            });

            //fluentvalidator验证

            services.AddTransient<IValidator<SysRoleCreateOrUpdateViewModel>, RoleValidator>();
            services.AddTransient<IValidator<SysUserCreateOrUpdateViewModel>, UserValidator>();
            services.AddTransient<IValidator<UserRoleViewModel>, UserRoleValidator>();

            //仓储
            services.AddScoped<ISysUserRepo, SysUserRepo>();
            services.AddScoped<ISysRoleRepo, SysRoleRepo>();
            services.AddScoped<IUserRoleRepo, UserRoleRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePages();
            }
            //跨域
            app.UseCors("any");

            //
            app.UseStaticFiles();
            //身份授权认证
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseExceptionMiddleware();

           var httpContextAccessor= app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();

            AuthContextService.Configure(httpContextAccessor);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=value}/{action=inde}/{Id?}"

                        );
            });



            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebApi");
                options.RoutePrefix = string.Empty;
            });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
