using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRL user;
        public UserManager(IUserRL user)
        {
            this.user = user;

        }
        public async Task<RegisterModel> AddUser(RegisterModel addUser)
        {
            try
            {
                return await this.user.AddUser(addUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RegisterModel> LoginUser(LoginModel login)
        {
            try
            {
                return await this.user.LoginUser(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                return await this.user.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task<RegisterModel> ResetPassword(ResetModel resetPassword)
        {
            try
            {
                return await this.user.ResetPassword(resetPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetJWTToken(string email)
        {
            if (email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

