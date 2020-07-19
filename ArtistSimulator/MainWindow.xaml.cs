using ArtistSimulator.Data.Models;
using ArtistSimulator.Data.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DBitmap = System.Drawing.Bitmap;

namespace ArtistSimulator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Area.MinimalSize = (int)PixelSizeSlider.Value;
            LoadFromPcButton.Click += (object sender, RoutedEventArgs e) => LoadFromFile();
            StartButton.Click += (object sender, RoutedEventArgs e) => Start();
            ToolButton.Click += (object sender, RoutedEventArgs e) => SwitchTool();
            CurrentModeButton.Click += (object sender, RoutedEventArgs e) => ModeSwitched();
            PixelSizeSlider.ValueChanged += (object sender, RoutedPropertyChangedEventArgs<double> e) => MinimalPixelSizeChanged((int)e.NewValue);
            LoadFromPcButton.IsEnabled = true;
            CurrentModeButton.Content = $"Mode: {Mode.CurrentMode.ToString()}";
        }

        private void MinimalPixelSizeChanged(int size) => Area.MinimalSize = size;

        private void ModeSwitched()
        {
            Mode.SwitchToNext();
            CurrentModeButton.Content = $"Mode: {Mode.CurrentMode.ToString()}";
        }

        private void SwitchTool()
        {
            Tool.CurrentTool = Tool.CurrentTool == ToolEnum.Brush ? ToolEnum.Scissors : ToolEnum.Brush;
            ToolButton.Content = $"Tool: {Tool.CurrentTool}";
        }

        private DBitmap _writableBitmap;
        private BitmapImage _image;

        private void LoadFromFile()
        {
            Loader loader = new Loader();
            
            _writableBitmap = loader.LoadWritableBitmap();
            _image = loader.LoadImage();
            
            CurrentImage.Source = _image;
            LoadFromPcButton.IsEnabled = false;
        }

        private void Start()
        {
            Workspace.Children.Clear();

            Analyzer analyzer = new Analyzer(_writableBitmap);
            MyColor lightest = analyzer.FindLightest(new System.Drawing.Point((int)Workspace.Margin.Left, (int)Workspace.Margin.Top), _writableBitmap.Size);
            MyColor color = lightest;
            Workspace.Background = new SolidColorBrush(color.WColor);
            PaintAndCutModeController controller = new PaintAndCutModeController(_writableBitmap);
            Area area = controller.SetupArea(Workspace, color);
            
            Workspace = area.Grid;
        }
    }
}
