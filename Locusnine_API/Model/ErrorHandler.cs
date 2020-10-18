using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locusnine_API.Model
{
	public class ErrorHandler
	{
		public int StatusCode { get; set; }

		public string ErrorMessage { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
