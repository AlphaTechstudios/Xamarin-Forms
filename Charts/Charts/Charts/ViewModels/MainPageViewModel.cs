using Prism.Navigation;
using System.Collections.Generic;
using SkiaSharp;
using Microcharts;

namespace Charts.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private LineChart lineChart;
        public LineChart LineChart
        {
            get => lineChart;
            set => SetProperty(ref lineChart, value);
        }

        private BarChart barChart;
        public BarChart BarChart
        {
            get => barChart;
            set => SetProperty(ref barChart, value);
        }


        private DonutChart donutChart;
        public DonutChart DonutChart
        {
            get => donutChart;
            set => SetProperty(ref donutChart, value);
        }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Charts";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            InitData();
        }

        private void InitData()
        {
            var blueColor = SKColor.Parse("#09C");
            var chartEntries = new List<ChartEntry>
            {
                new ChartEntry(200)
                {
                    Label = "France",
                    ValueLabel = "200",
                    Color = blueColor
                },
                new ChartEntry(450)
                {
                    Label = "USA",
                    ValueLabel = "450",
                    Color = blueColor
                },
                new ChartEntry(800)
                {
                    Label = "India",
                    ValueLabel = "800",
                    Color = blueColor
                },
                new ChartEntry(100)
                {
                    Label = "Italy",
                    ValueLabel = "100",
                    Color = blueColor
                },
            };

            var chartEntriesDonut = new List<ChartEntry>
            {
                new ChartEntry(200)
                {
                    Label = "France",
                    ValueLabel = "200",
                    Color = blueColor
                },
                new ChartEntry(450)
                {
                    Label = "USA",
                    ValueLabel = "450",
                    Color = SKColor.Parse("#FFF")
        },
                new ChartEntry(800)
                {
                    Label = "India",
                    ValueLabel = "800",
                    Color = SKColor.Parse("#000")
                },
                new ChartEntry(100)
                {
                    Label = "Italy",
                    ValueLabel = "100",
                    Color = SKColor.Parse("#E32")
                },
            };

            LineChart = new LineChart { Entries = chartEntries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal };
            BarChart = new BarChart { Entries = chartEntries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal };
            DonutChart = new DonutChart { Entries = chartEntriesDonut, LabelTextSize = 30f };
        }
    }
}
