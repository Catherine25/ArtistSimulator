using ArtistSimulator.Data.Models;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Diagnostics.Debug;

namespace ArtistSimulator.Data.Services
{
    class Analyzer
    {
        private Bitmap _bitmap;

        public Analyzer(Bitmap bitmap)
        {
            WriteLine("[Analyzer] Analyzer()");

            _bitmap = bitmap;
        }

        public MyColor FindLightest(Point point, Size size, MyColor lightest = null)
        {
            WriteLine($"[Analyzer] FindLightest() on area ({point}),({size})");

            if (lightest == null)
                lightest = new MyColor(Colors.White);

            MyColor pixel = new MyColor(Colors.Black);
            float brigtness = pixel.DColor.GetBrightness();

            int w = point.X + size.Width;
            int h = point.Y + size.Height;

            for (int x = point.X; x < w; x++)
                for (int y = point.Y; y < h; y++)
                {
                    MyColor p = new MyColor(_bitmap.GetPixel(x, y));
                    float b = p.DColor.GetBrightness();

                    if (brigtness < b)
                    {
                        pixel = p;
                        brigtness = b;
                        WriteLine($"Current brightness = {brigtness}");
                    }

                    if (pixel == lightest)
                        return pixel;
                }

            WriteLine($"[Analyzer] Lightest = {pixel.WColor}");

            return pixel;
        }

        public MyColor FindLightest_2(Point point, Size size, MyColor lightest = null)
        {
            WriteLine($"[Analyzer] FindLightest() on area ({point}),({size})");

            if (lightest == null)
                lightest = new MyColor(Colors.White);

            MyColor pixel = new MyColor(Colors.Black);
            float brigtness = pixel.DColor.GetBrightness();

            int w = point.X + size.Width;
            int h = point.Y + size.Height;

            for (int x = point.X; x < w; x++)
                for (int y = point.Y; y < h; y++)
                {
                    MyColor p = new MyColor(_bitmap.GetPixel(x, y));
                    float b = p.DColor.GetBrightness();

                    if (brigtness < b)
                    {
                        pixel = p;
                        brigtness = b;
                        WriteLine($"Current brightness = {brigtness}");
                    }

                    if (pixel == lightest)
                        return pixel;
                }

            WriteLine($"[Analyzer] Lightest = {pixel.WColor}");

            return pixel;
        }

        //public Area SetupArea(Grid grid, MyColor color, Bitmap bitmap)
        //{
        //    WriteLine("[Analyzer] SetupArea()");

        //    Area area = new Area(grid, color, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

        //    area.Interacted += Interacted;

        //    return area;
        //}

        //private int FindSplitAreaCount(int primary)
        //{
        //    for (int i = 2; i < primary; i++)
        //        if (primary % i == 0)
        //            return i;

        //    return primary;
        //}

        //private void Interacted(Area area)
        //{
        //    if (IsPainted(area))
        //        Cut(ref area);
        //    else
        //        Paint(area);
        //    //Thread.Sleep(100);
        //}

        //public bool IsPainted(Area area)
        //{
        //    bool painted = area.Color == FindLightest(area.ReferencedBitmapRectangle.Location, area.ReferencedBitmapRectangle.Size);

        //    WriteLineIf(painted, "[Analyzer] The area is painted");
        //    WriteLineIf(!painted, "[Analyzer] The area is NOT painted");

        //    return painted;
        //}

        //public void Paint(Area area)
        //{
        //    WriteLine("[Analyzer] Paint()");

        //    area.Color = FindLightest(area.ReferencedBitmapRectangle.Location, area.ReferencedBitmapRectangle.Size);
        //}

        //public void Cut(ref Area area)
        //{
        //    WriteLine("[Analyzer] Cut()");

        //    WriteLine($"[Analyzer] Cutting area at {area.ReferencedBitmapRectangle.Location.X},{area.ReferencedBitmapRectangle.Location.Y}");
        //    WriteLine($"[Analyzer]              with size = {area.ReferencedBitmapRectangle.Width},{area.ReferencedBitmapRectangle.Height}");
        //    WriteLine($"[Analyzer]              with color = {area.Color.WColor}");
        //    WriteLine($"[Analyzer] Area's name? {area.Grid.Name}");

        //    int horizontalCount = FindSplitAreaCount(area.ReferencedBitmapRectangle.Width);
        //    int vericalCount = FindSplitAreaCount(area.ReferencedBitmapRectangle.Height);

        //    WriteLine($"[Analyzer] Count: {horizontalCount}/{vericalCount}");

        //    for (int x = 0; x < horizontalCount; x++)
        //        area.Grid.ColumnDefinitions.Add(new ColumnDefinition());

        //    for (int y = 0; y < vericalCount; y++)
        //        area.Grid.RowDefinitions.Add(new RowDefinition());

        //    //new
        //    int w = area.ReferencedBitmapRectangle.Width / horizontalCount;
        //    int h = area.ReferencedBitmapRectangle.Height / vericalCount;

        //    for (int x = 0; x < horizontalCount; x++)
        //        for (int y = 0; y < vericalCount; y++)
        //        {
        //            var rect = new Rectangle
        //            {
        //                X = area.ReferencedBitmapRectangle.X + w * x,
        //                Y = area.ReferencedBitmapRectangle.Y + h * y,
        //                Width = w,
        //                Height = h
        //            };

        //            Area newArea = new Area(null, area.Color, rect);
        //            newArea.Interacted += Interacted;

        //            Grid.SetColumn(newArea.Grid, x);
        //            Grid.SetRow(newArea.Grid, y);
        //            area.Grid.Children.Add(newArea.Grid);
        //        }

        //    area.StopInteraction();
        //}
    }
}
