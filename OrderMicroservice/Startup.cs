using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderManagement.Domain.Aggregates.OrderAggregate;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Data.Contexts;
using OrderManagement.Infrastructure.Repositories;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OrderManagement.API
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
            services.AddCors();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddControllers();
            services.AddDbContext<OrderManagementContext>(setup => setup.UseSqlServer(connectionString));
            services.AddScoped<IRepository<cart>, Repository<cart>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<Order_Item>, Repository<Order_Item>>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Order Management API",
                    Description = "Allows wokring with orders information in database",
                    TermsOfService = new Uri("http://www.cognizant.com"),
                    //Contact = new OpenApiContact()
                    //{
                    //    Name = "Mahesh",
                    //    Email = "Maheshwaran.L@outlook.com",
                    //    Url = new Uri("https://github.com/Mahegit1")
                    //}
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    In = ParameterLocation.Header,
                    Description = "Jwt token for authorized user"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { new OpenApiSecurityScheme() { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }, new string[] { } }
                });
            });
            string constr = Configuration.GetConnectionString("DefaultCon");
            string key = Configuration["JwtSettings:key"];
            string issuer = Configuration["JwtSettings:issuer"];
            string audience = Configuration["JwtSettings:audience"];
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            SecurityKey securityKey = new SymmetricSecurityKey(keyBytes);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = securityKey
                };
            });
        

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                app.UseCors(options =>
                {
                    options.AllowAnyOrigin();   //accept request from all client
                    options.AllowAnyMethod();   //support all http operations like get, post, put, delete
                    options.AllowAnyHeader();   //support all http headers like content-type, accept, etc
                });
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "Order Management API"));
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
                
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
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
