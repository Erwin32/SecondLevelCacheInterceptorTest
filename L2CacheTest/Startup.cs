using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core.Configurations;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace L2CacheTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddEasyCaching(o =>
            {
                o.UseRedis(cfg =>
                {
                    cfg.EnableLogging = true;
                    cfg.SerializerName = "Pack";
                    cfg.DBConfig.Endpoints.Add(new ServerEndPoint {Host = "test-redis", Port = 6379});
                    cfg.DBConfig.AllowAdmin = true;
                    cfg.DBConfig.ConnectionTimeout = 10000;
                }, "DbRedis").WithMessagePack("Pack");
            });

            services.AddEFSecondLevelCache(o =>
            {
                o.UseEasyCachingCoreProvider("DbRedis", isHybridCache: false).DisableLogging(false);
                o.CacheAllQueries(CacheExpirationMode.Absolute, TimeSpan.FromMinutes(10));
            });

            services.AddDbContextPool<TestContext>((sp, o) =>
            {
                o.UseSqlServer("Server=host.docker.internal;Database=testdb;User Id=test;Password=test;");
                o.AddInterceptors(sp.GetRequiredService<SecondLevelCacheInterceptor>());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}