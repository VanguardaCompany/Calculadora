using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Calculadora.DAL;
using Microsoft.EntityFrameworkCore;
using Rotativa;
using Rotativa.AspNetCore;

namespace Calculadora.web
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
            var connection = @"Server=VM-DESENV\VANGUARDA;Database=Vanguarda;Trusted_Connection=True;MultipleActiveResultSets=true";
            services.AddDbContext<VanguardaContext>(options =>
                        options.UseSqlServer(connection)
            );
            services.AddMvc().AddSessionStateTempDataProvider();

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSession();
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Clientes}/{action=IndexCliente}/{sortOrder='',currentFilter='',page=1}");
            });
            app.UseDeveloperExceptionPage();

            RotativaConfiguration.Setup(env);
        }
    }
}
