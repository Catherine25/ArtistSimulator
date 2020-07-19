using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Diagnostics.Debug;

namespace ArtistSimulator.Data.Models
{
    class Area
    {
        public Grid Grid;
        public MyColor Color;
        public Rectangle ReferencedBitmapRectangle;
        public bool IsPainted = false;
        public bool IsMinimal { get => ReferencedBitmapRectangle.Width < MinimalSize && ReferencedBitmapRectangle.Height < MinimalSize; }
        public static int MinimalSize = 10;
        private static bool MouseDown = false;

        public Area(Grid grid, MyColor color, Rectangle rect)
        {
            Grid = grid;
            Color = color;
            ReferencedBitmapRectangle = rect;

            if (Grid == null)
                Grid = new Grid()
                {
                    Background = new SolidColorBrush(Color.WColor),
                };

            WriteLine($"[Area] Created area with rect = {rect}");
            WriteLine($"[Area]              with color = {color.WColor}");
            WriteLine($"[Area] Actual size of grid {Grid.ActualWidth},{Grid.ActualHeight}");
            WriteLine($"[Area] Size of grid {Grid.Width},{Grid.Height}");

            Grid.ShowGridLines = true;
            Grid.UseLayoutRounding = true;

            Grid.MouseDown += HandleMouseDownButton;
            Grid.MouseUp += HandleMouseUpButton;

            //Grid.StylusUp += (object sender, StylusEventArgs e) => Handle((int)e.GetPosition(Grid).X, (int)e.GetPosition(Grid).Y);
            //Grid.StylusDown += (object sender, StylusDownEventArgs e) => Handle((int)e.GetPosition(Grid).X, (int)e.GetPosition(Grid).Y);

            Grid.MouseEnter += HandleMouseEnter;
            Grid.MouseLeave += HandleMouseLeave;
        }

        private void HandleMouseUpButton(object sender, MouseEventArgs e)
        {
            MouseDown = false;
            Handle((int)e.GetPosition(Grid).X, (int)e.GetPosition(Grid).Y);
        }

        private void HandleMouseDownButton(object sender, MouseEventArgs e)
        {
            MouseDown = true;
            //Handle((int)e.GetPosition(Grid).X, (int)e.GetPosition(Grid).Y);
        }

        private void HandleMouseEnter(object sender, MouseEventArgs e)
        {
            if (MouseDown)
                Handle((int)e.GetPosition(Grid).X, (int)e.GetPosition(Grid).Y);
            else
                (sender as Grid).Background = new SolidColorBrush(Color.WColor);
        }

        private void HandleMouseLeave(object sender, MouseEventArgs e)
        {
            //(sender as Grid).Background = new SolidColorBrush(Color.Darker((int)Color.DColor.GetBrightness() * 50).WColor);
            (sender as Grid).Background = new SolidColorBrush(Color.Darker(25).WColor);
        }

        public void StopInteraction()
        {
            Grid.MouseDown -= HandleMouseDownButton;
            Grid.MouseUp -= HandleMouseUpButton;
            Grid.MouseEnter -= HandleMouseEnter;
            Grid.MouseLeave -= HandleMouseLeave;
        }

        private void Handle(int x, int y)
        {
            WriteLine(x + " " + y);
            WriteLine($"[Area] Interacted with area with color = {Color.WColor}");

            //Task.Run(() => Interacted(this));
            Interacted(this);
        }

        public Action<Area> Interacted;
    }
}
