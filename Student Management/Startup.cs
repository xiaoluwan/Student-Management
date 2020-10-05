using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Models;

namespace Student_Management {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _configuration;//IConfiguration用于读取各种配置资源信息
        public Startup(IConfiguration configuration) {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("StudentDBConnection"))//读取appsettings.json 连接字符串
                );
            services.AddMvc();//添加Xml序列化器格式化程序 .AddXmlSerializerFormatters()
            services.AddTransient<IStudentRepository, SQLStudentRepository>();//添加单例 //接口绑定 AddScoped与其他方法的区别
            //AddSingleton服务方法注册到了MockStudentRepositroy之中。AddSingleton方法在第一请求服务器创建服务的时候会创建一个
            //单个实例，并且在该服务所调用的地方复用这个实例，也就是说应用程序之中只有MockStudentRepositroy一个仓储

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //运行时将调用此方法。使用此方法来配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=Home}/{action=index}/{id?}");
            }
            );

            // app.UseMvc();
            //app.Run(async (context) => {
            //    var configval= _configuration["MyKey"];
            //    await context.Response.WriteAsync(configval);
            //});
        }
    }
}
