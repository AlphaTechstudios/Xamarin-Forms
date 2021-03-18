using ChatApp.DL;
using ChatApp.Managers;
using ChatApp.Managers.Interfaces;
using ChatApp.Models.Settings;
using ChatApp.Repositories;
using ChatApp.Repositories.Common;
using ChatApp.Repositories.Interfaces;
using ChatApp.WebAPI.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.WebAPI
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
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<ChatAppContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ChatAppDbContext"));
            });
            services.AddControllers();
            services.AddSignalR();

            // Configure jwt authentication.
            var secretKey = Encoding.ASCII.GetBytes(appSettings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = true,
                    ValidIssuer = appSettings.JwtIssuer,
                    ValidateAudience = true,
                    ValidAudiences = new List<string> { appSettings.JwtMobileAudience, appSettings.JwtWebAudience },
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<IUsersManager, UsersManager>();
            services.AddScoped<ITokensManager, TokensManager>();
            services.AddScoped<IConversationsManager, ConversationsManager>();

            services.AddScoped<IConversationRepliesRepository, ConversationRepliesRepository>();
            services.AddScoped<IConversationsRepository, ConversationsRepository>();
            services.AddScoped<ITokensRepository, TokensRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IFriendsRepository, FriendsRepository>();
            services.AddScoped<IConnectionsRepository, ConnectionsRepository>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<AppSettings>(appSettingsSection);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatApp.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatApp.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("chathub");
            });


        }
    }
}
