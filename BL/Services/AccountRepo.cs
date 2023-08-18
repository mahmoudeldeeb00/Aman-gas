using BL.DTOS;
using BL.Helpers;
using BL.IServices;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    
    public class AccountRepo : IAccountRepo
    {
        public UserManager<AppUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;
        private readonly DbContainer _db;
        public JWT _jwt;

        public AccountRepo(UserManager<AppUser> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> rmanager , DbContainer db)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = rmanager;
            _db = db;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Isseur,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.ExpireInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task<string> AddNewRoleAsync(string role)
        {
            var result =  await _roleManager.CreateAsync(new IdentityRole(role));
            return result.Succeeded.ToString();
        }

        public async Task<AuthenticationModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthenticationModel { Message = "this Email is already registered " };

            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthenticationModel { Message = "this User Name is already registered " };

            List<Char> CarChars = model.CarChars.ToList();  
            if (model.CarChars?.Length == 2)
                CarChars.Add('-');
           Car car = _db.Cars.FirstOrDefault(f => f.FirstChar == CarChars[0] && f.SecondChar == CarChars[1] && f.ThirdChar == CarChars[2]&&f.CarNumbers==model.CarNumbers );
            if(car is not null)
                return new AuthenticationModel { Message = "this Car is already registered " };


            var NewCar = new Car()
            {
                CarNumbers = model.CarNumbers.Replace(" ", ""),
                CarTypeId = model.CarType,
                FirstChar = CarChars[0],
                SecondChar = CarChars[1],
                ThirdChar = CarChars[2],
                Name = $"{model.FirstName}  {model.LastName}  vecile ",
                User = model.UserName
            };
            await _db.Cars.AddAsync(NewCar);
            var result = await _db.SaveChangesAsync();
            if(result > 0)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    Gmail = model.Gmail,
                    
                };
                var result1 = await _userManager.CreateAsync(user, model.Pasword);
                if (!result1.Succeeded)
                {
                    var errors = string.Empty;

                    foreach (var error in result1.Errors)
                    {
                        errors += error.Description + " ";
                    }
                    return new AuthenticationModel { Message = errors };
                }
                await _userManager.AddToRoleAsync(user, "User");
                var JwtSecurityToken = await CreateJwtToken(user);
                return new AuthenticationModel
                {
                    Email = user.Email,
                    ExpiredOn = JwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { "User" },
                    Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                    UserName = user.UserName
                };
            }

            return new AuthenticationModel { Message = " Error Ocuured when Try To Save New User  " };


        }

        public async Task<AuthenticationModel> TokenAsync(getTokenModel model)
        {
            List<char> CarChars = model.CarChars.ToList();
            if (model.CarChars?.Length == 2)
                CarChars.Add('-');
            AuthenticationModel TModel = new();
            AppUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                var UserCar = _db.Cars.FirstOrDefault(f => f.FirstChar == CarChars[0] && f.SecondChar == CarChars[1] && f.ThirdChar == CarChars[2] && f.CarNumbers == model.CarNumbers);
                if(UserCar is not null)
                {
                    user = await _userManager.FindByNameAsync(UserCar.User);
                }
            }



            if(user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                TModel.Message = "invalid UserName or password ";
                return TModel;
            }
            JwtSecurityToken token = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            TModel.IsAuthenticated = true;
            TModel.Token = new JwtSecurityTokenHandler().WriteToken(token);
            TModel.Email = user.Email;
            TModel.UserName = user.UserName;
            TModel.ExpiredOn = token.ValidTo;
            TModel.Roles = roles.ToList();
            return TModel;

        }
    }
}
