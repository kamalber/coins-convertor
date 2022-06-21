
using Microsoft.AspNetCore.Mvc;
using CoinConvertor.DTO.Requests;
using CoinConvertor.DTO.Responses;
using CoinConvertor.Services.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoinConvertor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new ResponseData
                {
                    ErrorMessage = "Missing login details",
                    ErrorCode = "107"
                });
            }

            var loginResponse = await userService.Login(loginRequest);

            if (!loginResponse.Success)
            {
                return Unauthorized(new
                {
                    loginResponse.ErrorCode,
                    loginResponse.ErrorMessage
                });
            }

            return Ok(loginResponse);
        }


        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(SignupRequest signupRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                if (errors.Any())
                {
                    return BadRequest(new ResponseData
                    {
                        ErrorMessage = $"{string.Join(",", errors)}",
                        ErrorCode = "108"
                    });
                }
            }

            var response = await userService.SignupAsync(signupRequest);

            if (!response.Success)
            {
                return UnprocessableEntity(response);
            }

            return Ok(response);
        }

    }
}
