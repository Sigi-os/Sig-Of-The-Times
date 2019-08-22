using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Serilog;
using Serilog.Formatting.Json;
using Swashbuckle.AspNetCore.Swagger;


namespace Sig.Profile.Service
{
    public class Startup
    {
        /// <summary>
        /// Configuration of IConfiguration
        /// </summary>
        public static IConfiguration Configuration { get; set; }


        /// <summary>
        /// For Containter
        /// </summary>
        //public IContainer ApplicationContainer { get; private set; } 


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional:true)
                    .AddJsonFile($"sharedsettings.json", optional:true)
                    .AddJsonFile($"/app/settings/sharedsettings.json", optional:true)
                    .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CreateAddLogger(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IConfiguration>(Configuration);

            var assemblies = new[] {
                Assembly.Load("KT.MobileFunding.Providers"),
                Assembly.Load("KT.MobileFunding.Business"),
                Assembly.Load("KT.MobileFunding.Domain")
            };


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        //nonstandard
        /// <summary>
        /// Setting up Logging using Serilog
        /// </summary>
        /// <param name="services"></param>
        private static void CreateAddLogger(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            services.AddLogging();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }
    }
}
