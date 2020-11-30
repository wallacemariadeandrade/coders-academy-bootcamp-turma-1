using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CodersAcademy.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CodersAcademy.API
{
    public class Startup
    {
        private readonly string allowedOrigins = "_allowedOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(
                    name: allowedOrigins,
                    builder => {
                        builder.WithOrigins(this.Configuration.GetSection("ConnectionStrings:AllowedOrigins").Get<string[]>())
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    }
                );
            });

            services.AddControllers();

            services.AddDbContext<MusicContext>(c => 
            {
                c.UseSqlite(this.Configuration.GetConnectionString("BootcampConnection"));
            });

            services.AddScoped<AlbumRepository>();
            services.AddScoped<UserRepository>();
            services.AddAutoMapper(typeof(Startup).Assembly); // O AutoMapper varre o assembly procurando o profile

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Coders Academy Bootcamp",
                    Version = "v1"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coders Academy Bootcamp"));

            app.UseRouting();

            app.UseCors(allowedOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
