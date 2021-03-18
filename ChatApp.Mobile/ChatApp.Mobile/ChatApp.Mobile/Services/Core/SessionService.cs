using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ChatApp.Mobile.Services.Core
{
    public class SessionService : ISessionService
    {
        /// <summary>
        /// Gets connecetd user From SecuredStorage.
        /// </summary>
        /// <returns>The current user Model.</returns>
        public async Task<UserModel> GetConnectedUser()
        {
            string content = string.Empty;
            try
            {
                content = await SecureStorage.GetAsync("ConnectedUser");
            }
            catch (Exception exp)
            {

            }
            return string.IsNullOrEmpty(content) ? null : JsonConvert.DeserializeObject<UserModel>(content);
        }

        /// <summary>
        /// Gets Token From SecuredStorage.
        /// </summary>
        /// <returns>The current user Model.</returns>
        public async Task<TokenModel> GetToken()
        {
            string content = string.Empty;
            try
            {
                content = await SecureStorage.GetAsync("Token");
            }
            catch (Exception exp)
            {

            }
            return string.IsNullOrEmpty(content) ? null : JsonConvert.DeserializeObject<TokenModel>(content);
        }

        /// <summary>
        /// Save the conneceted user in the secured storage.
        /// </summary>
        /// <param name="userModel">The connecetd userModel.</param>
        /// <returns>This method returns nothing.</returns>
        public async Task SetConnectedUser(UserModel userModel)
        {
            string content = JsonConvert.SerializeObject(userModel);
            await SecureStorage.SetAsync("ConnectedUser", content);
        }

        /// <summary>
        /// Sets user authentication and refresh tokens.
        /// </summary>
        /// <param name="tokenModel">The token model.</param>
        /// <returns>This method returns nothing</returns>
        public async Task SetToken(TokenModel tokenModel)
        {
            string content = JsonConvert.SerializeObject(tokenModel);
            await SecureStorage.SetAsync("Token", content);
        }

        /// <summary>
        /// Delete the user data from the secured storage.
        /// </summary>
        /// <returns>This methode returns nothing.</returns>
        public async Task LogOut()
        {
            await SecureStorage.SetAsync("ConnectedUser", string.Empty);
            await SecureStorage.SetAsync("Token", string.Empty);
        }

        /// <summary>
        /// Gets the key value from data
        /// </summary>
        /// <typeparam name="T">The data with class type.</typeparam>
        /// <param name="key">The target key.</param>
        /// <returns>The key value.</returns>
        public T GetValue<T>(string key) where T : class
        {
            var content = Preferences.Get(key, null);
            return string.IsNullOrEmpty(content) ? null : JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Gets the key value from data
        /// </summary>
        /// <typeparam name="T">The data with struct type.</typeparam>
        /// <param name="key">The target key.</param>
        /// <returns>The key value.</returns>
        public T GetStructValue<T>(string key) where T : struct
        {
            var content = Preferences.Get(key, null);
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Sets the value for the target key.
        /// </summary>
        /// <typeparam name="T">The data type.</typeparam>
        /// <param name="key">The target key.</param>
        /// <param name="value">Thge value to save.</param>
        public void SetValue<T>(string key, T value)
        {
            string content = JsonConvert.SerializeObject(value);
            Preferences.Set(key, content);
        }
    }
}
