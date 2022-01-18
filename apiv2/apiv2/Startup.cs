using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiv2.Data;
using apiv2.Repositories.ApprenticeRepository;
using apiv2.Repositories.DepartmentRepository;
using apiv2.Repositories.GenericRepository;
using apiv2.Services.ApprenticeService;
using apiv2.UOF;
using apiv2.Utilities;
using apiv2.Utilities.JWTUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace apiv2
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
            //// ENABLE CORS
            //services.AddCors(c => {
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //});
            services.AddControllers();
            
            //// JSON Serializer
            //services.AddControllersWithViews().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            //    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
            //    = new DefaultContractResolver());

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IJWTUtils, JWTUtils>();
            services.AddScoped<IApprenticeService, ApprenticeService>();

            services.AddDbContext<ProjectContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));


            // region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IApprenticeRepository, ApprenticeRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ENABLE CORS

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<JWTMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
