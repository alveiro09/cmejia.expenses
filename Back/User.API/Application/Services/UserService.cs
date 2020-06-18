using Domain.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.API.Application.Contracts;
using User.API.Application.Model.Request;
using User.API.Application.Model.Response;
using UserManagement.Domain.Repositories;

namespace User.API.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Contructor with the dependencies required
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userRepository"></param>
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public AddUserResponse AddUser(AddUserRequest addUserRequest)
        {
            AddUserResponse result = new AddUserResponse()
            {
                Added = false,
                FirstName = addUserRequest.FirstName,
                SecondName = addUserRequest.SecondName,
                UserName = addUserRequest.UserName
            };
            var newUser = new UserManagement.Domain.Model.User()
            {
                Id = Guid.NewGuid(),
                Age = addUserRequest.Age,
                Email = addUserRequest.Email,
                FirstName = addUserRequest.FirstName,
                IdentityDocument = addUserRequest.IdentityDocument,
                IdentityNumber = addUserRequest.IdentityNumber,
                Password = addUserRequest.Password,
                SecondEmail = addUserRequest.SecondEmail,
                SecondName = addUserRequest.SecondName,
                UserName = addUserRequest.UserName
            };
            try
            {
                _userRepository.Add(newUser);
                result.Added = true;
            }
            catch (System.Exception exception)
            {
                result.Message = exception.Message;
            }
            return result;
        }

        public async Task<UserResponse> GetUser(Guid id)
        {
            UserResponse result = new UserResponse();
            //var user = (await _userRepository.GetAsync(user => user.Id.Equals(id))).FirstOrDefault();
            var user = (await _userRepository.GetAsync()).FirstOrDefault();
            if (user != null)
            {
                result.FirstName = user.FirstName;
                result.UserName = user.UserName;
            }
            return result;
        }

        public async Task<List<UserResponse>> GetUsers()
        {
            List<UserResponse> result = new List<UserResponse>();
            var users = await _userRepository.GetAsync();
            if (users != null)
            {
                foreach (UserManagement.Domain.Model.User user in users)
                {
                    result.Add(new UserResponse
                    {
                        FirstName = user.FirstName,
                        UserName = user.UserName
                    });
                }
            }
            return result;
        }
    }
}
