using ListView.Models;
using ListView.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ListView.Services
{
    public class ProductsService : IProductsService
    {
        public IEnumerable<ProductModel> GetAll()
        {
            return new List<ProductModel>
            {
                new ProductModel{ ID = 1, Name = "Product 1", Price = 80.99f, ImagePath = "https://images.pexels.com/photos/291762/pexels-photo-291762.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                new ProductModel{ ID = 2, Name = "Product 2", Price = 20.99f, ImagePath = "https://images.pexels.com/photos/794062/pexels-photo-794062.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                new ProductModel{ ID = 3, Name = "Product 3", Price = 10.99f, ImagePath = "https://images.pexels.com/photos/35188/child-childrens-baby-children-s.jpg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                new ProductModel{ ID = 4, Name = "Product 4", Price = 50.99f, ImagePath = "https://images.pexels.com/photos/45055/pexels-photo-45055.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                new ProductModel{ ID = 5, Name = "Product 5", Price = 45.99f, ImagePath = "https://images.pexels.com/photos/1078973/pexels-photo-1078973.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                new ProductModel{ ID = 6, Name = "Product 6", Price = 33.99f, ImagePath = "https://images.pexels.com/photos/2994951/pexels-photo-2994951.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                new ProductModel{ ID = 7, Name = "Product 7", Price = 38.99f, ImagePath = "https://images.pexels.com/photos/297367/pexels-photo-297367.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                new ProductModel{ ID = 8, Name = "Product 8", Price = 200.99f, ImagePath = "https://images.pexels.com/photos/54203/pexels-photo-54203.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                new ProductModel{ ID = 9, Name = "Product 9", Price = 80.99f, ImagePath = "https://images.pexels.com/photos/744790/pexels-photo-744790.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"}
            };
        }

        public IEnumerable<ProductsVM> GetAllGrouppedByCategory()
        {
            return new List<ProductsVM>
            {
                new ProductsVM
                { CategoryName = "Category 1", Products = new ObservableCollection<ProductModel>( new List<ProductModel>
                    {
                        new ProductModel{ ID = 1, Name = "Product 1", Price = 80.99f, ImagePath = "https://images.pexels.com/photos/291762/pexels-photo-291762.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                        new ProductModel{ ID = 2, Name = "Product 2", Price = 20.99f, ImagePath = "https://images.pexels.com/photos/794062/pexels-photo-794062.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                        new ProductModel{ ID = 3, Name = "Product 3", Price = 10.99f, ImagePath = "https://images.pexels.com/photos/35188/child-childrens-baby-children-s.jpg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                    }),  Expanded = true,
                },
                new ProductsVM{ CategoryName = "Category 2", Products = new ObservableCollection<ProductModel>( new List<ProductModel>
                    {

                    new ProductModel{ ID = 4, Name = "Product 4", Price = 50.99f, ImagePath = "https://images.pexels.com/photos/45055/pexels-photo-45055.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                    new ProductModel{ ID = 5, Name = "Product 5", Price = 45.99f, ImagePath = "https://images.pexels.com/photos/1078973/pexels-photo-1078973.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                    new ProductModel{ ID = 6, Name = "Product 6", Price = 33.99f, ImagePath = "https://images.pexels.com/photos/2994951/pexels-photo-2994951.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                    new ProductModel{ ID = 7, Name = "Product 7", Price = 38.99f, ImagePath = "https://images.pexels.com/photos/297367/pexels-photo-297367.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},

                    })
                },
                new ProductsVM{ CategoryName = "Category 3", Products = new ObservableCollection<ProductModel>( new List<ProductModel>
                {
                    new ProductModel{ ID = 8, Name = "Product 8", Price = 200.99f, ImagePath = "https://images.pexels.com/photos/54203/pexels-photo-54203.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"},
                    new ProductModel{ ID = 9, Name = "Product 9", Price = 80.99f, ImagePath = "https://images.pexels.com/photos/744790/pexels-photo-744790.jpeg?auto=compress&cs=tinysrgb&h=750&w=1260"}
                })
               }
        };
        }
    }
}
