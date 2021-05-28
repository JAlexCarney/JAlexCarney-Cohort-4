using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.DAL.Repos;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace FieldAgent.Web
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,

                       ValidIssuer = "http://localhost:2000",
                       ValidAudience = "http://localhost:2000",
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"))
                   };
                   services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
               });

            services.AddCors(options => options.AddPolicy("corspolicy", (builder) =>
            {
                builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            }));

            services.AddRazorPages();

            services.AddControllersWithViews();

            services.AddTransient<IReportsRepository, ADOReportsRepository>();
            services.AddTransient<IAgencyAgentRepository, EFAgencyAgentRepository>();
            services.AddTransient<IAgencyRepository, EFAgencyRepository>();
            services.AddTransient<IAgentRepository, EFAgentRepository>();
            services.AddTransient<IAliasRepository, EFAliasRepository>();
            services.AddTransient<ILocationRepository, EFLocationRepository>();
            services.AddTransient<IMissionRepository, EFMissionRepository>();
            services.AddTransient<ISecurityClearanceRepository, EFSecurityClearanceRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors("corspolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
