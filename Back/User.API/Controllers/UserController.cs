using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Web.Http;
using User.API.Application.Contracts;
using User.API.Application.Model.Request;

namespace User.API.Controllers
{
    /// <summary>
    ///  controller to manage user 
    /// </summary>
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : Controller
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
        /// <remarks>Endpoint to create a new user</remarks>
        /// <param name="addUserRequest">information about the user</param>
        [Microsoft.AspNetCore.Mvc.HttpPost()]
        [Consumes("application/json")]
        [Produces("application/json")]
        public Task<IActionResult> AddUser([Microsoft.AspNetCore.Mvc.FromBody]AddUserRequest addUserRequest)
        {
            return _userService.AddUser(addUserRequest);
        }

        /// <summary>
        /// Get a user info. 
        /// </summary>
        /// <remarks>Endpoint to get an user</remarks>
        /// <param name="username">User id</param>
        [Microsoft.AspNetCore.Mvc.HttpGet("username")]
        [Produces("application/json")]
        public Task<IActionResult> GetUser(string username)
        {
            return _userService.GetUser(username);
        }

        /// <summary>
        /// Get list of user info. 
        /// </summary>
        /// <remarks>Endpoint to get all the users</remarks>
        [Microsoft.AspNetCore.Mvc.HttpGet()]
        [Produces("application/json")]
        public Task<IActionResult> GetUsers()
        {
            return _userService.GetUsers();
        }
        /// <summary>
        /// Get list of user info. 
        /// </summary>
        /// <remarks>Endpoint to get all the users</remarks>
        [Microsoft.AspNetCore.Mvc.HttpPost("authenticate")]
        [Produces("application/json")]
        [AllowAnonymous]
        public Task<IActionResult> Autenticate([Microsoft.AspNetCore.Mvc.FromBody]UserInfoRequest userInfoRequest)
        {
            return _userService.Authenticate(userInfoRequest);
        }
    }
}
