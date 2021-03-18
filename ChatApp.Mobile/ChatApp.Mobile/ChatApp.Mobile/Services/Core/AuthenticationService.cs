using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Core
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        public AuthenticationService(ISessionService sessionService, INavigationService navigationService)
            : base(sessionService, navigationService)
        {
        }

        /// <summary>
        /// Makes login for user.
        /// </summary>
        /// <param name="loginDto">The login dto.</param>
        /// <returns>The authenticated user.Null if not.</returns>
        public async Task<UserModel> Login(LoginModel loginDto)
        {
            return await Post<UserModel, LoginModel>("Users/login", loginDto);
        }

        /// <summary>
        /// Logout user.
        /// </summary>
        /// <returns>This methode returns nothing.</returns>
        public async Task LogOut()
        {
            await SessionService.LogOut();
        }

        public async Task<UserModel> RefreshToken(string token)
        {
            return await PostRefresh<UserModel, string>("Users/refresh", token);
        }
    }
}
