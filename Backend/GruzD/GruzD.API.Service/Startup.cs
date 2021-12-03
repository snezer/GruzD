using System.Text;

using AutoMapper;
using GruzD.DAL.PgSql;
using GruzD.Web.Contracts;
using GruzD.Web.Data;
using GruzD.Web.Hubs;
using GruzD.Web.Infrastructure;
using GruzD.Web.Infrastructure.Security;
using GruzD.Web.Models;
using IdentityModel;
using GruzD.Web.Infrastructure.Security;
using MediatR;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GruzD.Web.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("authDb"));
            });

            services.AddDbContext<LogicDataContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("mainDb"));
            });


            services
                .AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;

                    options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
                    options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
                //.AddAspNetIdentity<ApplicationUser>()
                ;

            services.AddAuthentication()
                .AddIdentityServerJwt();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAuthorization(options =>
            {
                foreach (var claim in KnownClaims.Claims)
                {
                    options.AddPolicy(claim.Value, policy => policy.RequireClaim(claim.Value));
                }
            });

            services.AddScoped<ClaimsLoader>();

            services.Configure<JwtSettings>(this.Configuration.GetSection("JwtSettings"));
            services.Configure<JwtBearerOptions>(IdentityServerJwtConstants.IdentityServerJwtBearerScheme,
                options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JwtSettings:SecretKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GruzD service", Version = "v1" });
            });

            services.AddCors();
            services.AddControllersWithViews(options =>
            {
            })
            .AddNewtonsoftJson(o =>
            {
                o.UseMemberCasing();
            })
            .AddMvcOptions(config =>
            {
                config.ModelBinderProviders.Insert(0, new InvariantDecimalModelBinderProvider());
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            StartUpService(services);
        }

        public static void StartUpService(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, ClaimsLoader claimsLoader)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var logicContext = serviceScope.ServiceProvider.GetRequiredService<LogicDataContext>();
                logicContext.Database.EnsureCreated();
            }

            this.SeedAthentication(userManager, claimsLoader);
            app.UseForwardedHeaders();
            bool.TryParse(this.Configuration["AppSettings:AllowDevMiddleware"], out var startDevMiddle);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GruzD v1"));

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin=>true));

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapHub<DefaultHub>("/defaulthub");
            });
        }

        private void SeedAthentication(UserManager<ApplicationUser> userManager, ClaimsLoader claimsLoader)
        {
            claimsLoader.Load().GetAwaiter().GetResult();

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@localhost",
                    FullName = "Admin",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Pa55w0rd#").Result;
            }

            var adminUser = userManager.FindByNameAsync("admin").Result;
            if (adminUser != null && !userManager.IsInRoleAsync(adminUser, KnownRoles.Admin).Result)
            {
                IdentityResult result = userManager.AddToRoleAsync(adminUser, KnownRoles.Admin).Result;
            }
        }
    }
}
