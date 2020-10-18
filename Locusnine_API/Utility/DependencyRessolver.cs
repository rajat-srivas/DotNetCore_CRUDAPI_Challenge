using Locusnine_API.Model;
using Locusnine_API.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locusnine_API.Utility
{
	public static class DependencyRessolver
	{
		public static void AddDependency(this IServiceCollection services)
		{
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IDataService, DataService>();
		}
	}
}
