using Microsoft.AspNetCore.Mvc;
using STE.Models;

namespace STE.Service
{
    public interface IUserService
    {
       Task<UserModel> GetUserById(string userId);
       Task<object> CreateUser(UserModel model);
       Task<string> DeleteUser(string userId);
       Task<UserModel> UpdateUser(UserModel model, string userId);

    }
}
