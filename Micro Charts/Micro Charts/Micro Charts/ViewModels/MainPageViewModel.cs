using Micro_Charts.Helpers;
using Microcharts;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro_Charts.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private DonutChart donutChart;
        public DonutChart DonutChart
        {
            get => donutChart;
            set => SetProperty(ref donutChart, value);
        }

        private LineChart lineChart;
        public LineChart LineChart
        {
            get => lineChart;
            set => SetProperty(ref lineChart, value);
        }

        private MultiLinesChart multiLinesChart;
        public MultiLinesChart MultiLinesChart
        {
            get => multiLinesChart;
            set => SetProperty(ref multiLinesChart, value);
        }

        private MultiBarChart multiBarChart;
        public MultiBarChart MultiBarChart
        {
            get => multiBarChart;
            set => SetProperty(ref multiBarChart, value);
        }


        private string[] months = new string[] { "JAN", "FRB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

        private float[] turnoverData = new float[] { 1000, 5000, 3500, 12000, 9000, 15000, 3000, 0, 0, 0, 0, 0 };
        private float[] chargesData = new float[] { 100, 500, 350, 1200, 900, 1500, 300, 0, 0, 0, 0, 0 };

        private SKColor blueColor = SKColor.Parse("#09C");
        private SKColor redColor = SKColor.Parse("#CC0000");

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            InitData();
        }

        private void InitData()
        {
            var entries = new List<List<ChartEntry>>();
            var turnoverEntries = new List<ChartEntry>();
            var donutChartEntries = new List<ChartEntry>();


            var chargesEntries = new List<ChartEntry>();
            int i = 0;
            foreach (var data in turnoverData)
            {
                turnoverEntries.Add(new ChartEntry(data)
                {
                    Color = blueColor,
                    ValueLabel = $"{data / 1000} k",
                    Label = months[i]
                });
                i++;
            }

            i = 0;
            foreach (var data in chargesData)
            {
                chargesEntries.Add(new ChartEntry(data)
                {
                    Color = redColor,
                    ValueLabel = $"{data / 1000} k",
                    Label = months[i]
                });
                i++;
            }


            donutChartEntries.Add(new ChartEntry(turnoverData.Sum())
            {
                Color = blueColor,
                ValueLabel = $"{turnoverData.Sum() / 1000} k",
                Label = "Turnover",
                ValueLabelColor = blueColor
            });

            donutChartEntries.Add(new ChartEntry(chargesData.Sum())
            {
                Color = redColor,
                ValueLabel = $"{chargesData.Sum() / 1000} k",
                Label = "Charges",
                ValueLabelColor = redColor
            });

            entries.Add(turnoverEntries);
            entries.Add(chargesEntries);

            DonutChart = new DonutChart { Entries = donutChartEntries, LabelTextSize = 30f };
            LineChart = new LineChart { Entries = turnoverEntries, LabelTextSize = 30f, LabelOrientation = Orientation.Horizontal };
            MultiLinesChart = new MultiLinesChart
            {
                MultiLineEntires = entries,
                LabelTextSize = 30f,
                LabelOrientation = Orientation.Horizontal,
                LineAreaAlpha = 50,
                PointAreaAlpha = 50,
                LegendNames = new List<string> { "Turnover Chart", "Charges Chart" },
                IsAnimated = false

            };

            MultiBarChart = new MultiBarChart
            {
                MultiBarEntries = entries,
                LabelTextSize = 30f,
                LabelOrientation = Orientation.Horizontal,
                PointAreaAlpha = 0,
                LegendNames = new List<string> { "Turnover Chart", "Charges Chart" },
                IsAnimated = false
            };
        }
    }
}
