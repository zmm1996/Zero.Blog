﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
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
using Zero.Web.Api.Extensions.AddConfigureServices;
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
              //  options.UseLazyLoadingProxies();//懒加载
                options.UseSqlServer(connStr);
            });

            //身份认证  AddJwtBearerAuthentication已经使用
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            //.AddCookie(options=> {
            //    options.LoginPath = "http://www.baidu.com";
            //});


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
                options.SerializerSettings.DateFormatString = "yyy-MM-dd HH:mm:ss";//解决返回时间带t

                //ef添加导航属性之后，返回值会无限循环引用
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddFluentValidation();

            //推荐加上
            //todo:iis无法访问
            //services.AddHsts(option =>
            //{
            //    option.Preload = true;
            //    option.IncludeSubDomains = true;
            //    option.MaxAge = TimeSpan.FromDays(60);
            //    option.ExcludedHosts.Add("example.com");
            //    option.ExcludedHosts.Add("www.example.com");

            //});
            ////将http转为https
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 5001;

            //});

            //swagger
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

            services.AddConfigureServicesExtension();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            using (var serviceScope=app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MyContext>();
                //初始化数据库，首次执行，已存在数据库不会创建
                context.Database.EnsureCreated();
            }

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
            //todo:iis无法使用
           // app.UseHttpsRedirection();

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



            app.UseSwagger(o =>
            {
                //路径过滤
                o.PreSerializeFilters.Add((document, request) =>
                {
                    //加载所有
                    document.Paths = document.Paths.ToDictionary(p => p.Key.ToLowerInvariant(), p => p.Value);

                    //只加载api/v1的
                    //IDictionary<string, PathItem> paths = new Dictionary<string, PathItem>();

                    //foreach (var path in document.Paths)
                    //{
                    //    if (path.Key.StartsWith("/api/v1"))
                    //        paths.Add(path.Key, path.Value);
                    //}

                    //document.Paths = paths;
                });
            });
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
