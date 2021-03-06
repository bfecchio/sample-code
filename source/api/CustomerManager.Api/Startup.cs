﻿using Microsoft.AspNetCore.Mvc;
using CustomerManager.Core.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using CustomerManager.Core.Helpers;
using CustomerManager.Core.Services;
using CustomerManager.Core.Validators;
using CustomerManager.Core.Repositories;
using Microsoft.Extensions.Configuration;
using CustomerManager.Core.Services.Impl;
using CustomerManager.Core.Validators.Impl;
using CustomerManager.Core.Repositories.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = new AppSettingsHelper(Configuration);

            #region Dependency Injection

            services.AddSingleton(Configuration);
            services.AddSingleton<IAppSettingsHelper>(appSettings);
            services.AddTransient<IDbContext>(ctx => new DbContext(ctx.GetService<IAppSettingsHelper>()));
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerValidator, CustomerValidator>();
            services.AddTransient<ICustomerService, CustomerService>();

            #endregion
            
            services
                .AddCors()
                .AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());            
            app.UseMvc();
        }
    }
}
