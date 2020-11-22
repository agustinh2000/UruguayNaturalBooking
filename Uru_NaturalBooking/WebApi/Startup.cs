
using BusinessLogic;
using BusinessLogicInterface;
using DataAccess;
using DataAccessInterface;
using Domain;
using Filters;
using Importation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddCors(cors =>
            {
                cors.AddPolicy("UruguayNaturalPolicy", options =>
                {
                    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            //services.AddControllers();

            services.AddDbContext<DbContext, ContextObl>(
                o => o.UseSqlServer(Configuration.GetConnectionString("DBConnection"))
            );


            services.AddScoped(typeof(IRepository<TouristSpot>), typeof(BaseRepository<TouristSpot>));
            services.AddScoped(typeof(IRepository<Region>), typeof(BaseRepository<Region>));
            services.AddScoped(typeof(IRepository<Category>), typeof(BaseRepository<Category>));
            services.AddScoped(typeof(IRepository<Lodging>), typeof(BaseRepository<Lodging>));
            services.AddScoped(typeof(IRepository<Reserve>), typeof(BaseRepository<Reserve>));
            services.AddScoped(typeof(IRepository<UserSession>), typeof(BaseRepository<UserSession>));
            services.AddScoped(typeof(IRepository<Review>), typeof(BaseRepository<Review>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITouristSpotRepository, TouristSpotRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserSessionRepository, UserSessionRepository>();
            services.AddScoped<ILodgingRepository, LodgingRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<ITouristSpotManagement, TouristSpotManagement>();
            services.AddScoped<IRegionManagement, RegionManagement>();
            services.AddScoped<ICategoryManagement, CategoryManagement>();
            services.AddScoped<ILodgingManagement, LodgingManagement>();
            services.AddScoped<IReserveManagement, ReserveManagement>();
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<IReviewManagement, ReviewManagement>();

            services.AddScoped<AuthorizationFilter>();
            services.AddScoped<ReflectionLogic>();
            services.AddScoped<ILodgingManagementForImportation, LodgingManagementForImportation>(); 

            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Obligatorio {groupName}",
                    Version = groupName,
                    Description = "Obligatorio",
                    Contact = new OpenApiContact
                    {
                        Name = "Obligatorio",
                        Email = string.Empty,
                        Url = new Uri("https://www.turismo.gub.uy/"),
                    }
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Obligatorio API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("UruguayNaturalPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
