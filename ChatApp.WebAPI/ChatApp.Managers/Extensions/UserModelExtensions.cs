using ChatApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Managers.Extensions
{
    public static class UserModelExtensions
    {
        public static IEnumerable<UserModel> WithoutPassword(this IEnumerable<UserModel> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static UserModel WithoutPassword(this UserModel userModel)
        {
            if (userModel == null)
            {
                return null;
            }
            userModel.Password = null;
            return userModel;
        }
    }
}
