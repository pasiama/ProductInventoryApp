using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductInventoryApp.Configurations;
using ProductInventoryApp.DTO;
using ProductInventoryApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductInventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        // private readonly JwtConfig _jwtConfig;

        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            // _jwtConfig = jwtConfig;
            _userManager = userManager;
            _configuration = configuration;

        }


       

        // POST api/<AuthenticationController>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        { 
            if (ModelState.IsValid)
            {

                var user_exist = await _userManager.FindByEmailAsync(requestDto.UserEmail);
                if (user_exist != null)
                {
                    return BadRequest(
                        new AuthResult()
                        {
                            Result = false,
                            Error = new List<string>()
                            {
                                "Email already exist"
                            }
                        });
                }

                //create a user
                var new_user = new IdentityUser
                {

                    Email = requestDto.UserEmail,
                    UserName = requestDto.UserName,

                };

                var is_created = await _userManager.CreateAsync(new_user, requestDto.Password);
                if (is_created.Succeeded)
                {
                    // generate the token
                    var token = GenerateJwtToken(new_user);

                    return Ok(new AuthResult()
                    {
                        Token = token,
                        Result = true
                    });
                }
                return BadRequest(
                       new AuthResult()
                       {
                           Result = false,
                           Error = new List<string>()
                           {
                                "Server Error"
                           }
                       });
            };
            return BadRequest();
        }

        private string GenerateJwtToken(IdentityUser user)
        {
           
             var Signingkey = _configuration.GetSection("JwtConfig:Secret").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Signingkey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                };
            var identity = new ClaimsIdentity(claims);

            var token = new JwtSecurityToken(
                issuer: "http://hubtel.com",
                audience: "http://hubtel.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

            
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequest)
        {
            if (ModelState.IsValid)
            {
                //check for user email
                var existing_user = await _userManager.FindByEmailAsync(loginRequest.Email);
                if(existing_user == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Error = new List<string>()
                     {
                         "Invalid Payload"
                     },
                        Result = false
                    });
                }

                var isCorrectPassword = await _userManager.CheckPasswordAsync(existing_user, loginRequest.Password);
                if (!isCorrectPassword) {
                    return BadRequest(new AuthResult()
                    {
                        Error = new List<string>()
                    {
                        "Invaild Credentials"
                    },
                        Result = false
                    });
                }

                var jwtToken = GenerateJwtToken(existing_user);

                return Ok(new AuthResult()
                {
                    Token = jwtToken,
                    Result = true
                });

            }
            return BadRequest(new AuthResult()
            {
                Error = new List<string>()
                {
                    "Invalid Payload"
                },
                Result = false
            });
        }

       
    }
}
