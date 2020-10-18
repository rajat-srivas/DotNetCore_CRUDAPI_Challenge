using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Locusnine_API.Utility.LocusnineUtilities;


namespace Locusnine_API.Model
{
	public class User : IEntity
	{
		[JsonProperty(PropertyName = "fullName")]
		public string FullName { get; set; }

		[JsonProperty(PropertyName = "email")]
		public string Email { get; set; }

		[JsonProperty(PropertyName = "status")]
		public Status Status { get; set; }

		[JsonProperty(PropertyName = "roleType")]
		public string RoleType { get; set; }

		[JsonProperty(PropertyName = "mobileNumber")]
		public string MobileNumber { get; set; }

		[Key]
		public string UserId { get; set; }
	}
}
