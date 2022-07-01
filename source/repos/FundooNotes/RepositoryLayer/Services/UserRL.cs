using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MSMQ.Messaging;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IMongoCollection<RegisterModel> User;

        private readonly IConfiguration configuration;

        public UserRL(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            User = database.GetCollection<RegisterModel>("User");
        }

        public async Task<RegisterModel> AddUser(RegisterModel addUser)
        {
            try
            {
                var check = this.User.AsQueryable().Where(x => x.Email == addUser.Email).SingleOrDefault();
                if (check == null)
                {
                    await this.User.InsertOneAsync(addUser);
                    return addUser;
                }
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RegisterModel> LoginUser(LoginModel login)
        {
            try
            {
                var result = this.User.AsQueryable().Where(x => x.Email == login.Email).FirstOrDefault();
                if (result != null)
                {
                    result = this.User.AsQueryable().Where(x => x.Password == login.Password).FirstOrDefault();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
                }
                return null;
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ForgotPassword(string email)
        {
            try
            {
                var result = this.User.AsQueryable().Where(u => u.Email == email).FirstOrDefault();
                if (result == null)
                {
                    return false;
                }
                MessageQueue Book;
                if (MessageQueue.Exists(@".\Private$\FundooNotes"))
                {
                    Book = new MessageQueue(@".\Private$\FundooNotes");
                }
                else
                {
                    Book = MessageQueue.Create(@".\Private$\FundooNotes");
                }


                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = GetJWTToken(email);
                message.Label = "Forgot Password BookStore";
                Book.Send(message);
                Message msg = Book.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendMail(email, msg.Body.ToString());
                Book.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                Book.BeginReceive();
                Book.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message message = queue.EndReceive(e.AsyncResult);
                EmailService.SendMail(e.Message.ToString(), GetJWTToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
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


        public async Task<RegisterModel> ResetPassword(ResetModel resetPassword)
        {
            try
            {
                var result = this.User.AsQueryable().Where(x => x.Email == resetPassword.Email).FirstOrDefault();
                if (result != null)
                {
                    await this.User.UpdateOneAsync(x => x.Email == resetPassword.Email,
                         Builders<RegisterModel>.Update.Set(x => x.Password, resetPassword.Password));
                    return result;

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
