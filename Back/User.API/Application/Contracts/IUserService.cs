using Domain.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.API.Application.Model.Request;

namespace User.API.Application.Contracts
{
    /// <summary>
    /// Service to manager users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="addUserRequest"></param>
        Task<IActionResult> AddUser(AddUserRequest addUserRequest);

        /// <summary>
        /// Get user
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetUser(string username);

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> GetUsers();

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> Authenticate(UserInfoRequest userInforequest);

        /// <summary>
        /// Update user by user name
        /// </summary>
        /// <returns></returns>
        Task<IActionResult> UpdateUser(string userName, List<PatchDto> patchDtos);
    }
}
