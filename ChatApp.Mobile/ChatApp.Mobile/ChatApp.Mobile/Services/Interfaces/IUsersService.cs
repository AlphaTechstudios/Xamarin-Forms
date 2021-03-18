using ChatApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<UserModel>> GetUserFriendsAsync(long userId);
    }
}
