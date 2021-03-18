using ChatApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ChatApp.Managers.Interfaces
{
    public interface IUsersManager
    {
        UserModel GetUserById(long userId);
        UserModel GetUserByEmail(string email);
        void AddUserConnections(ConnectionModel conversationModel);
        void UpdateUserConnectionsStatus(long userId, bool status, string connectionID);
        UserModel Login(LoginModel loginModel, HttpContext httpContext);
        UserModel RefreshToken(string refreshToken, HttpContext httpContext);
        long InsertUser(UserModel userModel);
        IEnumerable<UserModel> GetMyFriends(long userID);
        UserModel GetUserByConnectionId(string connectionId);
    }
}
