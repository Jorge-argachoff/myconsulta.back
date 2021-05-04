using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Repositorios;
using Domain.Services;
using Infra.Context;
using Infra.Dapper;
using Infra.Entities;
using Infra.Interfaces;
using Infra.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Domain.Dtos;
using Microsoft.OpenApi.Models;

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
            services.AddCors(options => options.AddPolicy("CorsPolicy", 
            builder => 
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowCredentials();
            }));
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient(_ => new ConsultaDb(Configuration.GetConnectionString("IdentityConnection")));
            services.AddTransient(_ => new ConsultaDb(Configuration.GetConnectionString("SqlServerConnection")));

            services.AddDbContext<TokenContext>(options => options.UseMySql(Configuration.GetConnectionString("IdentityConnection")));

            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));

            services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<TokenContext>().AddDefaultTokenProviders();

            //Infra
            services.AddTransient<IConsultaRepository, ConsultaRepository>();
            services.AddTransient<IConfiguracaoRepository, ConfiguracaoRepository>();
            services.AddTransient<IPessoaRepository, PessoaRepository>();



            //Domain
            services.AddTransient<IConsultaService, ConsultaServices>();
            services.AddTransient<IPessoaService, PessoaServices>();
            services.AddTransient<IConfiguracaoService, ConfiguracaoServices>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            //JWT
            var existe = Configuration.GetSection("AppTokenSettings").Exists();
            var appSettingsSection = Configuration.GetSection("AppTokenSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appsSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appsSettings.Secret);

             services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #region OLD
            // services.AddAuthentication(x =>
            // {
            //     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            // }).AddJwtBearer(x =>
            // {

            //     x.RequireHttpsMetadata = true;
            //     x.SaveToken = true;
            //     x.TokenValidationParameters = new TokenValidationParameters()
            //     {
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(key),
            //         ValidateIssuer = true,
            //         ValidAudience = appsSettings.ValidoEm,
            //         ValidIssuer = appsSettings.Emissor

            //     };

            // });
            #endregion

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyConsultaAPI", Version = "v1" });
                
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement{ 
                        {
                            new OpenApiSecurityScheme{
                                Reference = new OpenApiReference{
                                    Id = "Bearer", //The name of the previously defined security scheme.
                                    Type = ReferenceType.SecurityScheme
                                }
                            },new List<string>()
                        }
                    });
                                    
                }
            );
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

             

            app.UseSignalR(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chathub");
            });

            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
