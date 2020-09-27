using ListView.Models;
using ListView.ViewModels;
using System.Collections.Generic;

namespace ListView.Services
{
    public interface IProductsService
    {
        IEnumerable<ProductModel> GetAll();
        IEnumerable<ProductsVM> GetAllGrouppedByCategory();
    }
}
