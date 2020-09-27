using CollectionView.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollectionView.Services
{
    public class UsersService : IUsersService
    {
        private string apiUrl = "http://jsonplaceholder.typicode.com/";
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(apiUrl) })
            {
                try
                {
                    var response = await client.GetAsync("users");

                    if (!response.IsSuccessStatusCode)
                    {
                        // TODO Log the exception
                        Console.WriteLine(response.StatusCode);
                        return default;
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<IEnumerable<UserModel>>(result);
                    }
                }
                catch (Exception exp)
                {

                    throw;
                }
            }
        }
    }
}
