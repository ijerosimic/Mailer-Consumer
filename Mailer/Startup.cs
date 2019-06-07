using System.Data.SqlClient;
using AprossMailer.Data;
using AprossMailer.Factories;
using AprossMailer.Helpers;
using AprossMailer.Repository;
using AprossMailer.Services;
using AprossMailer.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AprossMailer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"));
            var connectionString = builder.ConnectionString;

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddOptions();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", corsBuilder =>
                {
                    corsBuilder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IApiRepository, ApiRepository>();
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IMessageFactory, MessageFactory>();
            services.AddScoped<IAttachmentCreator, AttachmentCreator>();
            services.AddScoped<IContentValidator, ContentValidator>();
            services.AddScoped<IEmailAddressValidator, EmailAddressValidator>();
            services.AddScoped<IValidationManager, ValidationManager>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
