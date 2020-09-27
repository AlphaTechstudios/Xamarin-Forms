using CollectionView.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CollectionView.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserModel>> GetUsers(); 
    }
}
