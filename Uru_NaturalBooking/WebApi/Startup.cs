
using BusinessLogic;
using BusinessLogicInterface;
using DataAccess;
using DataAccessInterface;
using Domain;
using Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
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
            }); ;

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
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<ILodgingRepository, LodgingRepository>();

            services.AddScoped<ITouristSpotManagement, TouristSpotManagement>();
            services.AddScoped<IRegionManagement, RegionManagement>();
            services.AddScoped<ICategoryManagement, CategoryManagement>();
            services.AddScoped<ILodgingManagement, LodgingManagement>();
            services.AddScoped<IReserveManagement, ReserveManagement>();
            services.AddScoped<IUserManagement, UserManagement>(); 

            services.AddScoped<AuthorizationFilter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
