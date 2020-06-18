using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using User.API.Application.Contracts;
using User.API.Application.Model.Request;
using User.API.Application.Model.Response;

namespace User.API.Controllers
{
    /// <summary>
    ///  controller to manage user 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        ///  Constructor with the environment and repository dependencies
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="logger"></param>
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new user. 
        /// </summary>
        /// <remarks>Endpoint to create a new tenant</remarks>
        /// <param name="addUserRequest">information about the user</param>
        [HttpPost()]
        [Consumes("application/json")]
        public AddUserResponse AddUser([FromBody]AddUserRequest addUserRequest)
        {
            return _userService.AddUser(addUserRequest);
        }

        /// <summary>
        /// Get a user info. 
        /// </summary>
        /// <remarks>Endpoint to create a new tenant</remarks>
        /// <param name="id">User id</param>
        [HttpGet("id")]
        [Consumes("application/json")]
        public Task<UserResponse> GetUser(Guid id)
        {
            return _userService.GetUser(id);
        }

        /// <summary>
        /// Get list of user info. 
        /// </summary>
        /// <remarks>Endpoint to create a new tenant</remarks>
        /// <param name="id">User id</param>
        [HttpGet("id")]
        [Consumes("application/json")]
        public Task<List<UserResponse>> GetUsers()
        {
            return _userService.GetUsers();
        }
    }
}
