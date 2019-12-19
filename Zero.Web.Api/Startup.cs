using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Zero.Core.Intefaces.Sys;
using Zero.Infrastructure.DataBase;
using Zero.Infrastructure.Repository.Sys;

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

            //获取连接字符串
            string connStr = _configuration["ConnectionStrings:DefaultConnection"];
            // _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(connStr);
            });

            services.AddMvc(options =>
            {

            }).AddJsonOptions(options =>
            {
                //返回json格式
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

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

            });

            services.AddScoped<ISysUserRepo, SysUserRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //
            app.UseStaticFiles();
            //认证
            app.UseAuthentication();

            //跨域
            app.UseCors("any");

            app.UseHttpsRedirection();

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
