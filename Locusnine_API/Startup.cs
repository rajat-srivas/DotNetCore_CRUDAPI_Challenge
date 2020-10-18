using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locusnine_API.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Locusnine_API.Utility;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Locusnine_API.Model;

namespace Locusnine_API
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddCors();
			services.AddSwaggerGen();
			services.AddDependency();
			services.AddDbContext<Locusnine_Context>(options =>
		 options.UseSqlServer(Configuration.GetConnectionString("Locusnine_Challenge")));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ILogger<Startup> logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseExceptionHandler(applicationError =>
			{
				applicationError.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";

					var feature = context.Features.Get<IExceptionHandlerFeature>();
					if (feature != null)
					{
						logger.LogError($"Error Details: {feature.Error}");
						await context.Response.WriteAsync(new ErrorHandler
						{
							StatusCode = context.Response.StatusCode,
							ErrorMessage = "Internal Server Error"
						}.ToString());
					}
				});
			});

			loggerFactory.AddFile("Logs/Locusnine_General_Logs-{Date}.txt");
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
