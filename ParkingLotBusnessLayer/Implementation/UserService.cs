using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingLotBusnessLayer.Interface;
using ParkingLotModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotBusnessLayer.Implementation
{
    public class UserService:IUserService
    {
        private IConfiguration _config;

        public IUserRepository userRepository;

        public UserService(IConfiguration config,IUserRepository userRepository)
        {
            _config = config;
            this.userRepository = userRepository;
        }

        public string Login(User user)
        {
            try
            {
                string RoleName=userRepository.Login(user);
                return GenerateJSONWebToken(user, RoleName);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Boolean AddUser(UserDetails userDetails)
        {
            try
            {                
                return userRepository.AddUser(userDetails);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string GenerateJSONWebToken(User userInfo,string Role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claim = new[] {                
                new Claim(JwtRegisteredClaimNames.Email,userInfo.Email),
                new Claim(ClaimTypes.Role,Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claim,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }      
    }
}
