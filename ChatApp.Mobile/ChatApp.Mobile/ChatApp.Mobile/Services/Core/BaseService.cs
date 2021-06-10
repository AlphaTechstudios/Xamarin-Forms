using ChatApp.Mobile.Models;
using ChatApp.Mobile.Services.Interfaces;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.Mobile.Services.Core
{
    public class BaseService
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        protected readonly ISessionService SessionService;
        private readonly INavigationService navigationService;

        /// <summary>
        /// Gets or sets the base address.
        /// </summary>
        protected string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the BaseRoute.
        /// </summary>
        protected string BaseRoute { get; set; }

        /// <summary>
        /// Gets the base address.
        /// </summary>
        protected string BaseUrl
        {
            get => $"{BaseAddress}{BaseRoute}/";
        }


        public BaseService(ISessionService sessionService)
        {
            InitRoutes();
            SessionService = sessionService;
        }

        public BaseService(ISessionService sessionService, INavigationService navigationService)
        {
            InitRoutes();
            SessionService = sessionService;
            this.navigationService = navigationService;
        }


        /// <summary>
        /// Gets httpClient.
        /// </summary>
        /// <returns>The hhtpClient.</returns>
        protected async Task<HttpClient> GetClient(bool withoutToken = false)
        {
            return await GetClient(BaseUrl, withoutToken);
        }

        private void InitRoutes()
        {
            BaseRoute = "/api";
            BaseAddress = "http://192.168.1.36:45455";
        }

        private HttpClient GetClientRefresh()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            return client;

        }

        /// <summary>
        /// Gets httpClient from URL.
        /// </summary>
        /// <param name="baseUrl">The url.</param>
        /// <returns>The httpClient.</returns>
        protected async Task<HttpClient> GetClient(string baseUrl, bool withoutToken = false)
        {
            bool relase = false;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            if (withoutToken)
            {
                return client;
            }
            var user = await SessionService.GetConnectedUser();
            var token = await SessionService.GetToken();

            try
            {
                if (user != null)
                {
                    if (token.TokenExpireTime.AddSeconds(30) <= DateTime.UtcNow)
                    {
                        await semaphoreSlim.WaitAsync();
                        relase = true;
                        user = await SessionService.GetConnectedUser();
                        token = await SessionService.GetToken();

                        if (token.TokenExpireTime.AddSeconds(30) <= DateTime.UtcNow)
                        {
                            user = await PostRefresh<UserModel, string>("Users/refresh", token.RefreshToken);
                            if (user != null)
                            {
                                await SessionService.SetConnectedUser(user);
                                await SessionService.SetToken(new TokenModel
                                {
                                    Token = user.Token,
                                    RefreshToken = user.RefreshToken,
                                    TokenExpireTime = user.TokenExpireTimes
                                });
                                token = await SessionService.GetToken();
                            }
                            else
                            {
                                await SessionService.SetConnectedUser(null);
                                await SessionService.SetToken(null);
                                await navigationService.NavigateAsync("../LoginPage");
                            }
                        }
                        semaphoreSlim.Release();
                        relase = false;
                    }
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                }
            }
            catch (Exception exp)
            {
                if (relase)
                {
                    semaphoreSlim.Release();
                }
            }
            return client;
        }

        /// <summary>
        /// Gets the response from url.
        /// </summary>
        /// <param name="url">The service url.</param>
        /// <returns>The reponse.</returns>
        protected async Task Get(string url)
        {
            using (HttpClient client = await GetClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        // TODO log.
                    }
                }
                catch (HttpRequestException ex)
                {
                    // TODO log.
                }
            }
        }

        /// <summary>
        /// Gets reponse from url.
        /// </summary>
        /// <typeparam name="T">The reponse type.</typeparam>
        /// <param name="url">The service url.</param>
        /// <returns>The service's reponse.</returns>
        protected async Task<T> Get<T>(string url, bool withoutToken = false)
        {
            using (HttpClient client = await GetClient(withoutToken))
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return default;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
                catch (HttpRequestException exp)
                {
                    return default;
                }
            }
        }

        /// <summary>
        /// Posts the data.
        /// </summary>
        /// <typeparam name="T">The response Type</typeparam>
        /// <typeparam name="M">The model Type</typeparam>
        /// <param name="url">The service url.</param>
        /// <param name="model">The model object.</param>
        /// <returns>The response.</returns>
        protected async Task<T> Post<T, M>(string url, M model)
        {
            var content = JsonConvert.SerializeObject(model);
            HttpContent contentPost = new StringContent(content, Encoding.UTF8, "application/json");
            using (HttpClient client = await GetClient())
            {
                try
                {
                    var response = await client.PostAsync(url, contentPost);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        return default;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
                catch (Exception exp)
                {
                    return default;
                }
            }
        }

        protected async Task<bool> Post(string url, MultipartFormDataContent model)
        {
            using (HttpClient client = await this.GetClient())
            {
                try
                {
                    var response = await client.PostAsync(url, model);


                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return false;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsByteArrayAsync();
                        return true;
                    }
                }
                catch (Exception exp)
                {
                    return false;
                }
            }
        }
        protected async Task<T> PostRefresh<T, M>(string url, M model)
        {
            var content = JsonConvert.SerializeObject(model);
            HttpContent contentPost = new StringContent(content, Encoding.UTF8, "application/json");
            using (HttpClient client = this.GetClientRefresh())
            {
                try
                {
                    var response = await client.PostAsync(url, contentPost);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return default(T);
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
                catch (Exception exp)
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// Puts the data.
        /// </summary>
        /// <typeparam name="T">The response Type</typeparam>
        /// <typeparam name="M">The model Type</typeparam>
        /// <param name="url">The service url.</param>
        /// <param name="model">The model object.</param>
        /// <returns>The response.</returns>
        protected async Task<T> PutAsync<T, M>(string url, M model)
        {
            var content = JsonConvert.SerializeObject(model);
            HttpContent contentPost = new StringContent(content, Encoding.UTF8, "application/json");
            using (HttpClient client = await GetClient())
            {
                try
                {
                    var response = await client.PutAsync(url, contentPost);

                    if (!response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return default;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(result);
                    }
                }
                catch (Exception exp)
                {
                    return default;
                }
            }
        }

    }
}
