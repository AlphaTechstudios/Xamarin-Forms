using ChatApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Interfaces
{
    public interface ISessionService
    {
        /// <summary>
        /// Gets connecetd user From SecuredStorage.
        /// </summary>
        /// <returns>The current user Model.</returns>
        Task<UserModel> GetConnectedUser();

        /// <summary>
        /// Gets Token From SecuredStorage.
        /// </summary>
        /// <returns>The current user Model.</returns>
        Task<TokenModel> GetToken();

        /// <summary>
        /// Save the conneceted user in the secured storage.
        /// </summary>
        /// <param name="userModel">The connecetd userModel.</param>
        /// <returns>This method return nothing.</returns>
        Task SetConnectedUser(UserModel userModel);

        /// <summary>
        /// Sets user authentication and refresh tokens.
        /// </summary>
        /// <param name="tokenModel">The token model.</param>
        /// <returns>This method returns nothing</returns>
        Task SetToken(TokenModel tokenModel);

        /// <summary>
        /// Delete the user data from the secured storage.
        /// </summary>
        /// <returns>This methode returns nothing.</returns>
        Task LogOut();

        /// <summary>
        /// Sets the value for the target key.
        /// </summary>
        /// <typeparam name="T">The data type.</typeparam>
        /// <param name="key">The target key.</param>
        /// <param name="value">Thge value to save.</param>
        void SetValue<T>(string key, T value);

        T GetStructValue<T>(string key) where T : struct;

        /// <summary>
        /// Gets the key value from data
        /// </summary>
        /// <typeparam name="T">The data type.</typeparam>
        /// <param name="key">The target key.</param>
        /// <returns>The key value.</returns>
        T GetValue<T>(string key) where T : class;
    }
}
