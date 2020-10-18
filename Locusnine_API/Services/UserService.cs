using Locusnine_API.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Locusnine_API.Services
{
	public class UserService : IUserService
	{
		private const string EmailDomain = "@locusnine.com";
		private readonly IDataService _dataService;
		private ILogger<IUserService> loggerService;
		public UserService(IDataService dataService, ILogger<IUserService> logger)
		{
			_dataService = dataService;
			loggerService = logger;
		}

		public async Task<User> AddUser(User userDetails)
		{

			loggerService.LogInformation("Start AddUser");
			if (userDetails == null) throw new ArgumentNullException(nameof(userDetails));
			userDetails.Email = await GenerateUserEmail(userDetails.FullName.Split(" ")[0].ToLower());
			userDetails.UserId = Guid.NewGuid().ToString();
			userDetails.Status = Utility.LocusnineUtilities.Status.Pending;
			var createdUser = await _dataService.Create(userDetails);
			loggerService.LogInformation($"User Created: {userDetails.UserId}");
			return createdUser;
		}

		private async Task<string> GenerateUserEmail(string firstName)
		{
			var generatedEmail = string.Empty;
			var emailNumberPart = new List<int>();
			int largestCount = 0;
			var existingEmails = await _dataService.GetAllEmailMatchingFirstName(firstName);
			if (existingEmails.Count == 0)
			{
				generatedEmail = $"{firstName}{EmailDomain}";
			}
			else
			{
				existingEmails.ForEach(x =>
				{
					var emailWithoutDomain = x.Split('@')[0];
					var namePart = Regex.Match(emailWithoutDomain, @"([A-Za-z]+)").Value;
					var numberPart = Regex.Match(emailWithoutDomain, @"\d+").Value;
					if (string.IsNullOrEmpty(numberPart))
						return;
					if (namePart.Equals(firstName))
					{
						emailNumberPart.Add(Convert.ToInt32(numberPart));
					}
				});
				if (emailNumberPart.Count > 0)
				{
					emailNumberPart.Sort();
					emailNumberPart.Reverse();
					largestCount = emailNumberPart[0];
					generatedEmail = $"{ firstName}{(largestCount + 1).ToString()}{EmailDomain}";
				}
				else
				{
					generatedEmail = $"{ firstName}1{EmailDomain}";
				}
			}

			return generatedEmail;
		}


		public async Task DeleteUser(string id)
		{

			loggerService.LogInformation($"Start Delete for user {id}");
			var entityToDelete = await _dataService.GetById(id);
			if (entityToDelete != null)
			{
				await _dataService.Delete(entityToDelete);
			}
			else
			{
				throw new InvalidOperationException("User to delete not found");
			}

		}

		public async Task<User> GetUserById(string id)
		{
			loggerService.LogInformation($"Start GetUserById {id}");
			var userDetail = await _dataService.GetById(id);
			if(userDetail == null)
			{
				throw new InvalidOperationException("User to delete not found");
			}	
			return userDetail;
		}

		public async Task<List<User>> GetUsers()
		{
			loggerService.LogInformation($"Start GetUsers");
			var allUsers = await _dataService.GetAll();
			return allUsers;
		}

		public async Task UpdateUser(User userDetails)
		{
			loggerService.LogInformation($"Start UpdateUser for User {userDetails.UserId}");
			if (userDetails == null) throw new ArgumentNullException(nameof(userDetails));
			var userToUpdate = await _dataService.GetById(userDetails.UserId);
			if (userToUpdate != null)
			{
				userToUpdate.RoleType = userDetails.RoleType;
				userToUpdate.MobileNumber = userDetails.MobileNumber;
			}
			else
			{
				throw new InvalidOperationException("User to Update not found");
			}

			await _dataService.Update(userToUpdate);

		}
	}
}
