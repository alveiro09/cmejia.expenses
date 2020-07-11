using Domain.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRepository _userRepositoryGeneric;
        private readonly ITokenAuthentication _tokenAuthentication;
        private readonly IRepository<UserManagement.Domain.Model.User> _userRepository;

        /// <summary>
        /// Contructor with the dependencies required
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="userRepositoryGeneric"></param>
        /// <param name="tokenAuthentication"></param>
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepositoryGeneric, ITokenAuthentication tokenAuthentication)
        {
            _unitOfWork = unitOfWork;
            _tokenAuthentication = tokenAuthentication;
            _userRepositoryGeneric = userRepositoryGeneric;
            _userRepository = _unitOfWork.GetRepository<UserManagement.Domain.Model.User>();
        }
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            AddUserResponse result = new AddUserResponse()
            {
                Added = false,
                FirstName = addUserRequest.FirstName,
                SecondName = addUserRequest.SecondName,
                LastName = addUserRequest.LastName,
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
                UserName = addUserRequest.UserName,
                LastName = addUserRequest.LastName
            };
            try
            {
                _userRepository.Add(newUser);
                var commit = _unitOfWork.Commit();
                result.Added = commit > 0;
            }
            catch (System.Exception exception)
            {
                result.Message = exception.Message;
            }
            return new OkObjectResult(result);
        }

        public async Task<IActionResult> GetUser(string username)
        {
            UserResponse result = new UserResponse();
            var userToFind = (await _userRepository.GetAsync(user => user.UserName.Equals(username))).FirstOrDefault();
            if (userToFind != null)
            {
                result.FirstName = userToFind.FirstName;
                result.UserName = userToFind.UserName;
                return new OkObjectResult(result);
            }
            else return new NotFoundResult();
        }

        public async Task<IActionResult> GetUsers()
        {
            List<UserResponse> result = new List<UserResponse>();
            try
            {
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
                    return new OkObjectResult(result);
                }
                else return new NotFoundResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(result);
            }
        }

        public async Task<IActionResult> Authenticate(UserInfoRequest userInforequest)
        {
            UserResponse result = new UserResponse();
            try
            {
                var userToFind = (await _userRepository.GetAsync(user => user.Email.ToLower().Equals(userInforequest.Mail.ToLower()))).FirstOrDefault();
                if (userToFind != null)
                {
                    var tokenInfo = new TokenResponse() { FirsName = userToFind.FirstName, LastName = userToFind.LastName, Mail = userToFind.Email, UserName = userToFind.UserName };
                    var token = _tokenAuthentication.BuildToken(tokenInfo);
                    return new OkObjectResult(token);
                }
                else return new NotFoundResult();
            }
            catch (Exception exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
