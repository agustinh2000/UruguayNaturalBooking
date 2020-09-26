
using BusinessLogic;
using BusinessLogicInterface;
using DataAccess;
using DataAccessInterface;
using Domain;
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
            services.AddScoped<ITouristSpotManagement, TouristSpotManagement>();
            services.AddScoped<IRegionManagement, RegionManagement>();
            services.AddScoped<ICategoryManagement, CategoryManagement>(); 

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
