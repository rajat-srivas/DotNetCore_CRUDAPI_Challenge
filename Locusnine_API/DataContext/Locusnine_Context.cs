using Locusnine_API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locusnine_API.DataContext
{
	public class Locusnine_Context : DbContext
	{
		public Locusnine_Context()
		{

		}
		public Locusnine_Context(DbContextOptions<Locusnine_Context> options)
		   : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
	}
}
