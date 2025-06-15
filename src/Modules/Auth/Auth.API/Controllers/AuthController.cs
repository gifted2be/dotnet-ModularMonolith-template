using Auth.Application.DTOs.RequestModel;
using Auth.Application.DTOs.ResponseModel;
using Auth.Application.Exceptions;
using Auth.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModularMonolith.Template.SharedKernel.Helpers;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(LoginDto requestModel)
        {
            LoginResposneDto? response = await _authService.Login(requestModel);

            if (response == null)
            {
                throw AuthException.UserNotFound(requestModel.Email);
            }

            return Ok(ResponseHelper.Success(response));
        }

        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(RegisterDto requestModel)
        {
            RegisterResponseDto? response = await _authService.Register(requestModel);

            if (response == null)
            {
                return BadRequest(ResponseHelper.BadRequest("User Register Failed"));
            }

            return Ok(ResponseHelper.Success(response));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto requestModel)
        {
            RefreshTokenResponseDto response = await _authService.RefreshAccessToken(requestModel);
            return Ok(ResponseHelper.Success(response));
        }
    }
}
