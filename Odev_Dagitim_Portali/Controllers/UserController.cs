using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Odev_Dagitim_Portali.Dtos;
using Odev_Dagitim_Portali.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Odev_Dagitim_Portali.Controllers
{
    [Route("api/User/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        ResultDto result = new ResultDto();
        public UserController(UserManager<AppUser> userManager, IMapper mapper, RoleManager<AppRole> roleManager, IConfiguration configuration, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public List<UserDto> List()
        {
            var users = _userManager.Users.ToList();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }
        [Authorize(Roles = "Admin,Teacher,Student")]
        [HttpGet]
        [Route("{id}")] 
        public UserDto GetById(string id)   
        {
            var user = _userManager.Users.Where(s => s.Id == id).SingleOrDefault();
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        [Authorize]
        [HttpPut]
        public async Task<ResultDto> Update(UserDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                result.Status = false;
                result.Message = "Kullanıcı Blunamadı!";
            }

            user.PhoneNumber = dto.PhoneNumber;
            user.Full_name = dto.Full_name;
            user.Email = dto.Email;

            await _userManager.UpdateAsync(user);
            result.Status = true;
            result.Message = "Kullanıcı Güncellendi";

            return result;
        }

        [HttpPost]
        public async Task<ResultDto> RegisterStudent(RegisterDto dto)
        {
            var identityResult = await _userManager.CreateAsync(new() { UserName = dto.UserName, Email = dto.Email, Full_name = dto.Full_name, PhoneNumber = dto.PhoneNumber }, dto.Password);

            if (!identityResult.Succeeded)
            {
                result.Status = false;
                foreach (var item in identityResult.Errors)
                {
                    result.Message += "<p>" + item.Description + "<p>";
                }

                return result;
            }
            var user = await _userManager.FindByNameAsync(dto.UserName);
            var roleExist = await _roleManager.RoleExistsAsync("Student");
            if (!roleExist)
            {
                var role = new AppRole { Name = "Student" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "Student");
            result.Status = true;
            result.Message = "Ogrenci Eklendi";
            return result;
        }


        [HttpPost]
        public async Task<ResultDto> RegisterTeacher(RegisterDto dto)
        {
            if (!dto.Email.Contains("@akdeniz.edu.tr"))
            {

                result.Status = false;
                result.Message = "Hatalı e-mail. Resmi e mail adresinizi kullanmanız gerkli!";
                return result;
            }

            var identityResult = await _userManager.CreateAsync(new() { UserName = dto.UserName, Email = dto.Email, Full_name = dto.Full_name, PhoneNumber = dto.PhoneNumber }, dto.Password);

            if (!identityResult.Succeeded)
            {
                result.Status = false;
                foreach (var item in identityResult.Errors)
                {
                    result.Message += "<p>" + item.Description + "<p>";
                }

                return result;
            }
            var user = await _userManager.FindByNameAsync(dto.UserName);
            var roleExist = await _roleManager.RoleExistsAsync("Teacher");
            if (!roleExist)
            {
                var role = new AppRole { Name = "Teacher" };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, "Teacher");
            result.Status = true;
            result.Message = "Öğretmen Eklendi";
            return result;
        }

        [HttpPost]
        public async Task<ResultDto> SignIn(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user is null)
            {
                result.Status = false;
                result.Message = "Ogrenci Bulunamadı!";
                return result;
            }
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordCorrect)
            {
                result.Status = false;
                result.Message = "Kullanıcı Adı veya Parola Geçersiz!";
                return result;
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("JWTID", Guid.NewGuid().ToString()),

            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GenerateJWT(authClaims);
            
            result.Status = true;
            result.Message = token ;
            return result;

        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public ResultDto Delete(string id)
        {
            var user = _context.Users.Where(s => s.Id == id).SingleOrDefault();
            if (user == null)
            {
                result.Status = false;
                result.Message = "Kişi Bulunamadı!";
                return result;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            result.Status = true;
            result.Message = "Kişi Silindi";
            return result;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ResultDto> GiveRole(AddRoleDto dto)
        {

            var user = await _userManager.FindByNameAsync(dto.UserName);
            var roleExist = await _roleManager.RoleExistsAsync(dto.Role);
            if (!roleExist)
            {
                var role = new AppRole { Name = dto.Role };
                await _roleManager.CreateAsync(role);
            }

            await _userManager.AddToRoleAsync(user, dto.Role);
            result.Message = dto.Role +" rolü Eklendi";
            result.Status = true;
            return result;
        }

        





        private string GenerateJWT(List<Claim> claims)
        {

            var accessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["AccessTokenExpiration"]));


            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: accessTokenExpiration,
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
        

    }
}