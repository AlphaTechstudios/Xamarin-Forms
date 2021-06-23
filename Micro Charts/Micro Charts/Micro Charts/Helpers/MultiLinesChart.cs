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
    public class MultiLinesChart: LineChart
    {
        public IEnumerable<IEnumerable<ChartEntry>> MultiLineEntires { get; set; }
        public List<string> LegendNames { get; set; }
        private bool initiated = false;
        private float multiLineMax;
        private float multiLineMin;
        private List<SKPoint> points = new List<SKPoint>();

        private void Init()
        {
            if (initiated)
            {
                return;
            }


            initiated = true;
            foreach (List<ChartEntry> line in MultiLineEntires)
            {
                foreach (ChartEntry entry in line)
                {
                    if(entry.Value > multiLineMax)
                    {
                        multiLineMax = entry.Value;
                    }
                    if (entry.Value < multiLineMin)
                    {
                        multiLineMin = entry.Value;
                    }
                }
            }
        }

        public override void DrawContent(SKCanvas canvas, int width, int height)
        {
            Init();

            Entries = MultiLineEntires.ElementAt(0);
            var valueLableSizes = MeasureLabels(MultiLineEntires.ElementAt(0).Select(x => x.Label).ToArray());
            var footerHeight = CalculateFooterHeaderHeight(valueLableSizes, Orientation.Horizontal);
            var itemSize = CalculateItemSize(width, height, footerHeight, 100);
            var origin = CalculateYOrigin(itemSize.Height, 100);

            foreach (IEnumerable<ChartEntry> l in MultiLineEntires)
            {
                Entries = l;
                var tempPoints = CalculateMultiLinePoints(itemSize, origin, 100);
                points.AddRange(tempPoints);
                DrawLine(canvas, tempPoints, itemSize);
                DrawPoints(canvas, tempPoints);

                //if(MultiLineEntires.IndexOf(l) == 0)
                //{
                    DrawArea(canvas, tempPoints, itemSize, origin);
                    DrawFooter(canvas, l.Select(x => x.Label).ToArray(), valueLableSizes, tempPoints, itemSize, height, footerHeight);
                //}
            }

            DrawLegends(canvas, width, height);
        }

        private void DrawLegends(SKCanvas canvas, int width, int height)
        {
            if (!LegendNames.Any()) { return; }

            List<SKColor> colors = new List<SKColor> { };

            foreach (List<ChartEntry> l in MultiLineEntires)
            {
                colors.Add(l[0].Color);
            }

            int radius_size = 20;

            using (var paint = new SKPaint())
            {
                paint.TextSize = this.LabelTextSize;
                paint.IsAntialias = true;
                paint.IsStroke = false;

                float x = 200 + radius_size * 2;
                float y = 50;

                foreach (string legend in LegendNames)
                {
                    paint.Color = SKColor.Parse("#000000");
                    canvas.DrawText(legend, x + radius_size + 10, y, paint);

                    paint.Color = colors.ElementAt(LegendNames.IndexOf(legend));
                    canvas.DrawCircle(x, y - radius_size / 2 - radius_size / 4, radius_size, paint);

                    x += radius_size * 2 + this.LabelTextSize * (legend.Length / 2 + 2);
                }

                var minPoint = points.Min(p => p.Y);
                var maxPoint = points.Max(p => p.Y);

                paint.Color = SKColor.Parse("#000000");
                paint.TextSize = 20;
                canvas.DrawCircle(12, minPoint, 5, paint);
                canvas.DrawText(NumbersTools.GetNember(multiLineMax), 0, minPoint - 20, paint);
                canvas.DrawCircle(12, maxPoint, 5, paint);
                canvas.DrawText(NumbersTools.GetNember(multiLineMin), 0, maxPoint - 20, paint);

                var step = maxPoint / 8;
                var valueStep = multiLineMax / 6;
                for (int i = 1; i < 6; i++)
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

        private SKPoint[] CalculateMultiLinePoints(SKSize itemSize, float origin, int headerHeight)
        {
            var result = new List<SKPoint>();

            for (int i = 0; i < this.Entries.Count(); i++)
            {
                var entry = this.Entries.ElementAt(i);

                var x = this.Margin + (itemSize.Width / 2) + (i * (itemSize.Width + this.Margin));
                var y = headerHeight + (((multiLineMax - entry.Value) / (multiLineMax - multiLineMin)) * itemSize.Height);
                var point = new SKPoint(x, y);
                result.Add(point);
            }

            return result.ToArray();
        }
    }
}
