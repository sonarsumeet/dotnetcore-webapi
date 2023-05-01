using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetCoreWebAPI.DataAccess;
using NetCoreWebAPI.Mapper;
using NetCoreWebAPI.MiddleWare;
using NetCoreWebAPI.Models;
using NetCoreWebAPI.RepoImplem;
using NetCoreWebAPI.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI
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
            services.AddControllers();


            services.AddAutoMapper(typeof(MapperConfig));

            services.AddSingleton<IJWTAuthenticate, JWTAuthenticate>();
            services.AddScoped<IUsers, UsersDB>();
            services.AddScoped<IProducts, ProductDB>();

            var logger = new LoggerConfiguration()
                .WriteTo.File("Logs\\NetCoreWebAPI\\1.txt", rollingInterval: RollingInterval.Minute)
                .CreateLogger();


           

            services.AddDbContext<ApplicationDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultValue"));
            });

            services.AddAuthentication(x =>
            {

                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o => {
                var key = System.Text.Encoding.UTF8.GetBytes(Configuration["JWTKeys:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = Configuration["JWTKeys:issuer"],
                    ValidAudience = Configuration["JWTKeys:Audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),


                };

            });

           // services.AddSingleton<IJWTAuthenticate, JWTAuthenticate>();
           
               

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            //app.Run(async context =>
            //{
            //    context.Response.Redirect("https://www.google.com/");
            //});

            app.UseMiddleware<ExceptionMessage>();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
