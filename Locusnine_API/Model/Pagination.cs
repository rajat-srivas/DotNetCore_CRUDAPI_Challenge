using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Locusnine_API.Model
{
	public class Pagination
	{
		[Key]
		public int Id { get; set; }
		public int Limit { get; set; }

		public int PageSize { get; set; }

		public int Count { get; set; }

		public int PageNumber { get; set; }
	}
}
