using Locusnine_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locusnine_API.Services
{
	public interface IDataService
	{
		Task<List<User>> GetAll();
		Task<User> GetById(string id);
		Task<User> Create(User entity);
		Task<User> Update(User entity);
		Task Delete(User entity);

		Task<List<string>> GetAllEmailMatchingFirstName (string firstName);

	}
}
