using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ArtistSimulator.Data.Services
{
    class Loader
    {
        public Loader()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dlg.ShowDialog() == true)
            {
                _path = dlg.FileName;
                if (_path != null)
                    IsFileChoosen = true;
            }
        }

        private string _path;

        public bool IsFileChoosen { get; private set; }

        public Bitmap LoadWritableBitmap() => new Bitmap(_path);

        public BitmapImage LoadImage()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(_path);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
