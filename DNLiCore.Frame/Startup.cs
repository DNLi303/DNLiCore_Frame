using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DNLiCore.Frame;
using DNLiCore.Model;
using DNLiCore.Service;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace DNLiCore.Frame
{
    public class Startup
    {
        public IFreeSql Fsql { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            string SqlDBName = Configuration["SqlConfig:SqlDBName"].ToString();
            string SqlType = Configuration["SqlConfig:SqlType"].ToString();
            string SqlHost = Configuration["SqlConfig:SqlHost"].ToString();
            string SqlPoint = Configuration["SqlConfig:SqlPoint"].ToString();
            string SqlUser = Configuration["SqlConfig:SqlUser"].ToString();
            string SqlPassWord = Configuration["SqlConfig:SqlPassWord"].ToString();
            string sqlconnection = "";
            DataType type = DataType.MySql;
            if (SqlType.ToUpper().Equals("MYSQL"))
            {
                type = DataType.MySql;
                sqlconnection = "Database=" + SqlDBName + ";Data Source=" + SqlHost + ";Port=" + SqlPoint + ";User Id=" + SqlUser + ";Password=" + SqlPassWord + ";Charset=utf8;TreatTinyAsBoolean=false;";
            }
            if (SqlType.ToUpper().Equals("SQLSERVER"))
            {
                sqlconnection = "Server=" + SqlHost + "," + SqlPoint + ";Database=" + SqlDBName + ";User Id=" + SqlUser + ";Password=" + SqlPassWord + ";";
                type = DataType.SqlServer;
            }
            //if (SqlType.ToUpper().Equals("SQLITE"))
            //{
            //    type = DataType.Sqlite;
            //}

            Fsql = new FreeSqlBuilder()
                        .UseConnectionString(type, sqlconnection)
                        .UseAutoSyncStructure(false)
                        .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //数据库注入
            services.AddSingleton<IFreeSql>(Fsql);

            //服务反射注入
            Assembly assembly = Assembly.Load("DNLiCore.Service");
            List<Type> ts = assembly.GetTypes().ToList();
            foreach (var item in ts.Where(s => !s.IsInterface))
            {
                var interfaceType = item.GetInterfaces();
                if (item.IsGenericType) continue;
                services.AddScoped(item);
            }

            //缓存管理
            services.AddMemoryCache();


            services.AddMvc(option => { option.Filters.Add(typeof(AuthorFilter)); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(option =>
            {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               // app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/View/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            //使用NLog作为日志记录工具
            loggerFactory.AddNLog();
            //引入Nlog配置文件
            env.ConfigureNLog("nlog.config");
            app.UseMvc(routes =>
            {
                string InstallFlag = Configuration["InstallFlag"].ToString();
                if (InstallFlag == "0")
                {
                    routes.MapRoute(
                        name: "default",
                       template: "{controller=View}/{action=Login}/{id?}");
                }
                else
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=View}/{action=Login}/{id?}");
                }
            });
        }
    }
}
