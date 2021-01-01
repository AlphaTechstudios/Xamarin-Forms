using Prism.Navigation;
using System.Collections.Generic;

namespace CarouselView.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private IEnumerable<string> imagesList;
        public IEnumerable<string> ImagesList 
        { 
            get => imagesList;
            set => SetProperty(ref imagesList, value);
        }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Carousel View";
        }

        public override void Initialize(INavigationParameters parameters)
        {
            ImagesList = new List<string>
            {
                "https://images.pexels.com/photos/162520/farmer-man-shepherd-dog-162520.jpeg?auto=compress&cs=tinysrgb&h=650&w=940",
                "https://images.pexels.com/photos/1069712/pexels-photo-1069712.jpeg?auto=compress&cs=tinysrgb&h=650&w=940",
                "https://images.pexels.com/photos/3551498/pexels-photo-3551498.jpeg?auto=compress&cs=tinysrgb&h=650&w=940",
                "https://images.pexels.com/photos/3651618/pexels-photo-3651618.jpeg?auto=compress&cs=tinysrgb&h=650&w=940"
            };
        }


    }
}
