using Locusnine_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locusnine_API.Services
{
	public interface IUserService
	{
		Task<User> GetUserById(string id);

		Task<List<User>> GetUsers();

		Task<User> AddUser(User userDetails);

		Task UpdateUser(User userDetails);

		Task DeleteUser(string id);
	}
}
