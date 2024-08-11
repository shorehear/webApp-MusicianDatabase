//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace MusiciansAPI.Authorization
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class AuthorizationController : ControllerBase
//    {
//        private readonly IConfiguration configuration;
//        private readonly UserManager<IdentityUser> userManager;
//        private readonly SignInManager<IdentityUser> signInManager;


//        public AuthorizationController(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
//        {
//            this.configuration = configuration;
//            this.userManager = userManager;
//            this.signInManager = signInManager;
//        }

//        [HttpPost("token")]
//        public async Task<IActionResult> GenerateToken([FromBody] UserLogin userLogin)
//        {
//            var user = await userManager.FindByNameAsync(userLogin.Username);
//            if (user != null && await userManager.CheckPasswordAsync(user, userLogin.Password))
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var key = Encoding.UTF8.GetBytes(configuration["Jwt:key"]);
//                var tokenDescriptor = new SecurityTokenDescriptor
//                {
//                    Subject = new ClaimsIdentity(new[] { new Claim("sub", userLogin.Username) }),
//                    Expires = DateTime.UtcNow.AddHours(1),
//                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//                };

//                var token = tokenHandler.CreateToken(tokenDescriptor);
//                var tokenString = tokenHandler.WriteToken(token);
//                return Ok(new { Token = tokenString });
//            }
//            return Unauthorized();
//        }
//    }

//    public class UserLogin 
//    {
//        public string Username { get; set; }
//        public string Password { get; set; }
//    }
//}
