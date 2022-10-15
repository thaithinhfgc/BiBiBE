using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using BiBiBE.Repository;
using System.Linq;
using BiBiBE.Models;

namespace BiBiBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository repositoryAccount;
        private readonly IConfiguration configuration;
        public UserController(IUserRepository _repositoryAccount, IConfiguration configuration)
        {
            repositoryAccount = _repositoryAccount;
            this.configuration = configuration;

        }


        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int page, int pageSize)
        {

            try
            {
                var AccountList = await repositoryAccount.SearchByEmail(search, page, pageSize);
                var Count = AccountList.Count();
                return Ok(new { StatusCode = 200, Message = "Load successful", data = AccountList, Count });

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Getaccount(int id)
        {
            try
            {
                var Result = await repositoryAccount.GetProfile(id);
                return Ok(new { StatusCode = 200, Message = "Load successful", data = Result });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult> GetLogin(User acc)
        {
            try
            {

                User customer = await repositoryAccount.LoginMember(acc.Email, acc.Password);
                if (customer != null)
                {

                    return Ok(new { StatusCode = 200, Message = "Login succedfully", data = GenerateToken(customer) });

                }
                else
                {
                    return Ok(new { StatusCode = 200, Message = "Email or Password is valid" });
                }


            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }
        }
        private string GenerateToken(User acc)
        {
            var secretKey = configuration.GetSection("AppSettings").GetSection("SecretKey").Value;

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]{
                    new Claim(ClaimTypes.Email, acc.Email),

                    new Claim("MemberId", acc.UserId.ToString()),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
        [HttpPost]

        public async Task<IActionResult> Create(User acc)
        {

            try
            {
                var newAcc = new User
                {
                    UserId = acc.UserId,
                    Email = acc.Email,
                    Password = acc.Password,
                    Phone = acc.Phone,
                    Role = acc.Role,
                };
                await repositoryAccount.AddMember(newAcc);
                return Ok(new { StatusCode = 200, Message = "Add successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }

        [HttpPut("{id}")]

        public async Task<IActionResult> update(int id, User acc)
        {
            if (id != acc.UserId)
            {
                return BadRequest();
            }
            try
            {
                var Acc = new User
                {
                    UserId = acc.UserId,
                    Email = acc.Email,
                    Password = acc.Password,
                    Phone = acc.Phone,
                    Role = acc.Role,
                };
                await repositoryAccount.UpdateMember(Acc);
                return Ok(new { StatusCode = 200, Message = "Update successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {

            try
            {

                await repositoryAccount.DeleteMember(id);

                return Ok(new { StatusCode = 200, Message = "Delete successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }
    }
}
