using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Models;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LIADbContext _context;
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public UsersController(LIADbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        // REGISTER
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterModel model)
        {
            if (!_context.Users.Any(x => x.Email == model.Email))
            {
                try
                {
                    var user = new User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PersonalIdentityNumber = model.PersonalIdentityNumber,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email
                    };
                    user.CreatePasswordHash(model.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return Ok("User successfully created!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return BadRequest();
        }

        // LOGIN
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == model.Email);

            if (user != null)
            {
                if (user.ValidatePasswordHash(model.Password))
                {
                    var secretKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecretKey"));
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("DisplayName", ($"{user.FirstName} {user.LastName}")),
                        new Claim("UserId", user.Id.ToString()),
                        }),
                        IssuedAt = DateTime.UtcNow,
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var accessToken = _tokenHandler.WriteToken(_tokenHandler.CreateToken(tokenDescriptor));

                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                        return new OkObjectResult(new
                        {
                            AccessToken = accessToken,
                            Id = user.Id
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unhandled error. {ex.Message}\n{ex}");
                    }
                }
            }
            return BadRequest();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
