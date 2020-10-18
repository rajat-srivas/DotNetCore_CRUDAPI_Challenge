using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Locusnine_API.Model;
using Locusnine_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace Locusnine_API.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	[Produces("application/json")]
	public class UserController : ControllerBase
	{
		private readonly IUserService userService;
		public UserController(IUserService user)
		{
			userService = user;
		}

		[HttpGet]
		[Route("")]
		public async Task<IActionResult> Get()
		{
			var users = await userService.GetUsers();
			return Ok(users);
		}


		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var userById = await userService.GetUserById(id);
			return Ok(userById);
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateUser([FromBody] User userDetails)
		{
			var newsUser = await userService.AddUser(userDetails);
			return Ok(newsUser);
		}

		[HttpPatch]
		[Route("update")]
		public async Task<IActionResult> UpdateUser([FromBody] User userDetails)
		{
			await userService.UpdateUser(userDetails);
			return NoContent();
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			userService.DeleteUser(id);
			return Ok();
		}
	}
}
