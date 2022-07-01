using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        Task<RegisterModel> AddUser(RegisterModel addUser);
        Task<RegisterModel> LoginUser(LoginModel login);
        Task<bool> ForgotPassword(string email);
        Task<RegisterModel> ResetPassword(ResetModel resetPassword);
    }
}
