using Application.Features.Tournaments;
using Application.Features.Users;
using Application.Features.Users.GetById;
using Application.Features.Users.Login;
using Application.Features.Users.Register;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly GetUserByIdHandler _getUserByIdHandler;
        private readonly LoginHandler _loginHandler;
        private readonly RegisterHandler _registerHandler;
        private readonly IMapper _mapper;

        public AuthenticationController(GetUserByIdHandler getUserByIdHandler, LoginHandler loginHandler, RegisterHandler registerHandler, IMapper mapper)
        {
            _getUserByIdHandler = getUserByIdHandler;
            _loginHandler = loginHandler;
            _registerHandler = registerHandler;
            _mapper = mapper;
        }

        [HttpGet("Get/{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _getUserByIdHandler.ExecuteAsync(new GetUserByIdQuery { Id = id }, cancellationToken);

                if (user is null)
                    return NotFound();

                return Ok(_mapper.Map<UserResponse>(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _registerHandler.ExecuteAsync(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _loginHandler.ExecuteAsync(request, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
