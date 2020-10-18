using Locusnine_API.DataContext;
using Locusnine_API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Locusnine_API.Services
{
	public class DataService : IDataService
	{
		Locusnine_Context dbContext;
		private ILogger<IDataService> loggerService;
		public DataService(Locusnine_Context context, ILogger<IDataService> logger)
		{
			dbContext = context;
			loggerService = logger;
		}

		public async Task<User> Create(User userToCreate)
		{
			var createdEntity = await dbContext.Users.AddAsync(userToCreate);
			SaveChanges();
			return userToCreate;
		}

		public async Task Delete(User userToDelete)
		{
			dbContext.Users.Remove(dbContext.Users.Where(x=>x.UserId == userToDelete.UserId).First());
			dbContext.SaveChanges();
		}
		public async Task<List<User>> GetAll()
		{
			var allDetails = await dbContext.Users.ToListAsync();
			return allDetails;
		}

		public async Task<User> GetById(string id)
		{
			var details = dbContext.Users.Where(x => x.UserId == id).First();
			return details;
		}
		private void SaveChanges()
		{
			dbContext.SaveChangesAsync();
		}

		public async Task<User> Update(User entity)
		{
			dbContext.Entry(entity).State = EntityState.Modified;
			SaveChanges();
			return entity;
		}

		public async Task<List<string>> GetAllEmailMatchingFirstName(string firstName)
		{
			return await dbContext.Users.Where(x => x.Email.StartsWith(firstName)).Select(x => x.Email).ToListAsync();
		}
	}
}
