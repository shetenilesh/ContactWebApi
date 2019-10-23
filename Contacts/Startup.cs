using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Contacts.Models;
using Contacts.Interfaces;
using Contacts.Repositories;
using AutoMapper;
using Contacts.AutoMapper;

namespace Contacts
{
	/// <summary>
	/// Start up.
	/// </summary>
	public class Startup
    {
		/// <summary>
		/// Constructs an instance of the Startup object.
		/// </summary>
		/// <param name="configuration">The configuration for this Startup object.</param>
		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		/// <summary>
		/// The configuration for this Startup object.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Adds services to the container.
		/// </summary>
		/// <param name="services">The services to add to the container.</param>
		/// <remarks>
		/// This method is called by the runtime.
		/// </remarks>
		public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
			services.AddTransient<IContactRepository, ContactRepository>();
			services.AddDbContext<ContactDBContext>(context => { context.UseInMemoryDatabase("ContactManagement"); });
			
			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AutoMapperConfiguration());
			});

			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Contacts API", Version = "v1" });
				c.IncludeXmlComments(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "ContactsWebApi.xml"));
			});
		}

        /// <summary>
		/// Configures the HTTP request pipeline.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <param name="env">The hosting environment.</param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contacts Api V1");
			});
		}
    }
}
