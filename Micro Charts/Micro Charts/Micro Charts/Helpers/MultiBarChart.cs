using Micro_Charts.Tools;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Internals;

namespace Micro_Charts.Helpers
{
    public class MultiBarChart: BarChart
    {
        public IEnumerable<IEnumerable<ChartEntry>> MultiBarEntries { get; set; }
        public List<string> LegendNames { get; set; }
        private bool initiated = false;
        private float multilineMin;
        private float multilineMax;
        private List<SKPoint> points = new List<SKPoint>();
        private void init()
        {
            if (initiated)
            {
                return;
            }

            initiated = true;

            foreach (List<ChartEntry> l in MultiBarEntries)
            {
                foreach (ChartEntry e in l)
                {
                    if (e.Value > multilineMax)
                    {
                        multilineMax = e.Value;
                    }
                    if (e.Value < multilineMin)
                    {
                        multilineMin = e.Value;
                    }
                }
            }
        }
        
        public override void DrawContent(SKCanvas canvas, int width, int height)
        {
            init();
            int i = 0;
            Entries = MultiBarEntries.ElementAt(0);
            var headerHeight = 100;
            var valueLableSizes = MeasureLabels(MultiBarEntries.ElementAt(0).Select(x => x.Label).ToArray());
            var footerHeight = CalculateFooterHeaderHeight(valueLableSizes, Orientation.Horizontal);
            var itemSize = CalculateItemSize(width, height, footerHeight, headerHeight);
            var origin = CalculateYOrigin(itemSize.Height, headerHeight);


            foreach (IEnumerable<ChartEntry> entry in MultiBarEntries)
            {
                Entries = entry;
                var points = this.CalculateMultilinePoints(itemSize, origin, headerHeight, i);
                this.points.AddRange(points);
                this.DrawBars(canvas, points, itemSize, origin, headerHeight);
                this.DrawPoints(canvas, points);

                if (MultiBarEntries.IndexOf(entry) == 0)
                {
                    this.DrawBarAreas(canvas, points, itemSize, headerHeight);
                    this.DrawFooter(canvas, entry.Select(x => x.Label).ToArray(), valueLableSizes, points, itemSize, height, footerHeight);
                    //DrawValueLabel(canvas, yPoints, itemSize, height, valueLabelSizes);
                }
                i++;
            }
            DrawLegend(canvas, width, height);
        }

        private SKPoint[] CalculateMultilinePoints(SKSize itemSize, float origin, float headerHeight, int i)
        {
            var result = new List<SKPoint>();

            for (int j = 0; j < this.Entries.Count(); j++)
            {
                var entry = this.Entries.ElementAt(j);

                var x = this.Margin + (itemSize.Width / 2) + (j * (itemSize.Width + this.Margin)) + i * 20;
                var y = headerHeight + (((multilineMax - entry.Value) / (multilineMax - multilineMin)) * itemSize.Height);
                var point = new SKPoint(x, y);
                result.Add(point);
            }

            return result.ToArray();
        }

        protected void DrawBars(SKCanvas canvas, SKPoint[] points, SKSize itemSize, float origin, float headerHeight)
        {
            const float MinBarHeight = 1;
            if (points.Length > 0)
            {
                for (int i = 0; i < this.Entries.Count(); i++)
                {
                    var entry = this.Entries.ElementAt(i);
                    var point = points[i];

                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = entry.Color,
                    })
                    {
                        var x = point.X - (itemSize.Width / 6);
                        var y = Math.Min(origin, point.Y);
                        var height = Math.Max(MinBarHeight, Math.Abs(origin - point.Y));
                        if (height < MinBarHeight)
                        {
                            height = MinBarHeight;
                            if (y + height > this.Margin + itemSize.Height)
                            {
                                y = headerHeight + itemSize.Height - height;
                            }
                        }

                        var rect = SKRect.Create(x, y, itemSize.Width / 4, height);
                        canvas.DrawRect(rect, paint);
                    }
                }
            }
        }

        private void DrawLegend(SKCanvas canvas, int width, int height)
        {
            if (!LegendNames.Any()) { return; }

            List<SKColor> colors = new List<SKColor> { };

            foreach (List<ChartEntry> l in MultiBarEntries)
            {
                colors.Add(l[0].Color);
            }

            int rectWidth = 20;

            using (var paint = new SKPaint())
            {
                paint.TextSize = this.LabelTextSize;
                paint.IsAntialias = true;
                paint.IsStroke = false;

                float x = 200 + rectWidth * 2;
                float y = 50;

                foreach (string legend in LegendNames)
                {
                    paint.Color = SKColor.Parse("#000000");
                    canvas.DrawText(legend, x + rectWidth + 10, y, paint);

                    paint.Color = colors.ElementAt(LegendNames.IndexOf(legend));
                    var rect = SKRect.Create(x, y - rectWidth, rectWidth, rectWidth);
                    canvas.DrawRect(rect, paint);

                    x += rectWidth * 2 + this.LabelTextSize * (legend.Length / 2 + 2);
                }

                var minPoint = points.Min(p => p.Y);
                var maxPoint = points.Max(p => p.Y);

                paint.Color = SKColor.Parse("#000000");
                paint.TextSize = 20;
                canvas.DrawCircle(12, minPoint, 5, paint);
                canvas.DrawText(NumbersTools.GetNember(multilineMax), 0, minPoint - 20, paint);
                canvas.DrawCircle(12, maxPoint, 5, paint);
                canvas.DrawText(NumbersTools.GetNember(multilineMin), 0, maxPoint - 20, paint);

                var step = maxPoint / 4;
                var valueStep = multilineMax / 4;
                for (int i = 1; i < 4; i++)
                {
                    var tt = (maxPoint - step * i);
                    if (minPoint < (maxPoint - step * i) && Math.Abs(minPoint - (maxPoint - step * i)) >= step)
                    {
                        canvas.DrawCircle(12, (maxPoint - step * i), 5, paint);
                        canvas.DrawText(NumbersTools.GetNember(valueStep * i), 0, (maxPoint - step * i) - 20, paint);
                    }
                }
            }
        }


    }
}
