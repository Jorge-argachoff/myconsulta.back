using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositorios;
using Domain.Services;
using Infra;
using Infra.Dapper;
using Infra.Entities;
using Infra.Interfaces;
using Infra.Repositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace myConsulta
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient(_ => new ConsultaDb(Configuration.GetConnectionString("SqlServerConnection")));

            services.AddDbContext<TokenContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<TokenContext>().AddDefaultTokenProviders() ;

            //Infra
            services.AddTransient<IConsultaRepository,ConsultaRepository>();
            services.AddTransient<IConfiguracaoRepository, ConfiguracaoRepository>();
            services.AddTransient<IPessoaRepository, PessoaRepository>();


            //Domain
            services.AddTransient<IConsultaService, ConsultaServices>();
            services.AddTransient<IPessoaService, PessoaServices>();
            services.AddTransient<IConfiguracaoService, ConfiguracaoServices>();

            //JWT
var existe = Configuration.GetSection("AppTokenSettings").Exists();
            var appSettingsSection = Configuration.GetSection("AppTokenSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appsSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appsSettings.Secret);

            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            
            }).AddJwtBearer(x=> {

                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidAudience = appsSettings.ValidoEm,
                    ValidIssuer = appsSettings.Emissor
                
                };
            
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;                
                options.Password.RequireLowercase = false;                
                options.Password.RequireUppercase = false;               
                options.Password.RequireNonAlphanumeric = false;

                
            });

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
            app.UseCors(builder => builder
                             .AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowCredentials());
            
            
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
