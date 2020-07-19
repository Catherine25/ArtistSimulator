using ArtistSimulator.Data.Models;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Diagnostics.Debug;

namespace ArtistSimulator.Data.Services
{
    class PaintAndCutModeController
    {
        private Analyzer _analyzer;
        private Bitmap _bitmap;

        public PaintAndCutModeController(Bitmap bitmap)
        {
            _analyzer = new Analyzer(bitmap);
            _bitmap = bitmap;
        }

        public Area SetupArea(Grid grid, MyColor color)
        {
            WriteLine("[Analyzer] SetupArea()");

            Area area = new Area(grid, color, new Rectangle(0, 0, _bitmap.Width, _bitmap.Height));

            area.Interacted += Interacted;

            return area;
        }

        private int FindSplitAreaCount(int primary)
        {
            for (int i = 2; i < primary; i++)
                if (primary % i == 0)
                    return i;

            return primary;
        }

        private void Interacted(Area area)
        {
            if (Tool.CurrentTool == ToolEnum.Brush)
                Paint(ref area);
            else if(!area.IsMinimal)
                Cut(ref area);
        }

        public void Paint(ref Area area)
        {
            WriteLine("[Analyzer] Paint()");
            if (area.IsPainted)
                return;
            else
                area.Color = _analyzer.FindLightest(area.ReferencedBitmapRectangle.Location, area.ReferencedBitmapRectangle.Size, area.Color);
        }

        public void Cut(ref Area area)
        {
            WriteLine("[Analyzer] Cut()");

            WriteLine($"[Analyzer] Cutting area at {area.ReferencedBitmapRectangle.Location.X},{area.ReferencedBitmapRectangle.Location.Y}");
            WriteLine($"[Analyzer]              with size = {area.ReferencedBitmapRectangle.Width},{area.ReferencedBitmapRectangle.Height}");
            WriteLine($"[Analyzer]              with color = {area.Color.WColor}");
            WriteLine($"[Analyzer] Area's name? {area.Grid.Name}");

            int horizontalCount = FindSplitAreaCount(area.ReferencedBitmapRectangle.Width);
            int vericalCount = FindSplitAreaCount(area.ReferencedBitmapRectangle.Height);

            WriteLine($"[Analyzer] Count: {horizontalCount}/{vericalCount}");

            for (int x = 0; x < horizontalCount; x++)
                area.Grid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int y = 0; y < vericalCount; y++)
                area.Grid.RowDefinitions.Add(new RowDefinition());

            //new
            int w = area.ReferencedBitmapRectangle.Width / horizontalCount;
            int h = area.ReferencedBitmapRectangle.Height / vericalCount;

            for (int x = 0; x < horizontalCount; x++)
                for (int y = 0; y < vericalCount; y++)
                    area = CreateArea(area, x, y, w, h);

            area.StopInteraction();
        }

        private Area CreateArea(Area area, int x, int y, int w, int h)
        {
            var rect = new Rectangle
            {
                X = area.ReferencedBitmapRectangle.X + w * x,
                Y = area.ReferencedBitmapRectangle.Y + h * y,
                Width = w,
                Height = h
            };

            Area newArea = new Area(null, area.Color, rect);
            newArea.Interacted += Interacted;

            Grid.SetColumn(newArea.Grid, x);
            Grid.SetRow(newArea.Grid, y);
            area.Grid.Children.Add(newArea.Grid);

            return area;
        }
    }
}
