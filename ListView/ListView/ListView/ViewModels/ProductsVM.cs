using ListView.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ListView.ViewModels
{
    public class ProductsVM : ObservableCollection<ProductModel>, INotifyPropertyChanged
    {

        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CategoryName"));
            }
        }

        private bool expanded;
        public bool Expanded
        {
            get => expanded;
            set
            {
                expanded = value;
                ClearItems();

                if (!expanded)
                {
                    ClearItems();
                }
                else if (Products != null)
                {
                    foreach (var item in Products)
                    {
                        Add(item);
                    }
                }

                OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
            }
        }

        private ObservableCollection<ProductModel> products;

        public ObservableCollection<ProductModel> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Products"));
            }
        }
    }
}
