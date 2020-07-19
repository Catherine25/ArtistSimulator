using DColor = System.Drawing.Color;
using WColor = System.Windows.Media.Color;

namespace ArtistSimulator.Data.Models
{
    public class MyColor
    {
        public MyColor(DColor dColor) => DColor = dColor;
        public MyColor(WColor wColor) => WColor = wColor;

        public override bool Equals(object obj)
        {
            return obj is MyColor color;
        }

        public static bool operator ==(MyColor x, MyColor y)
        {
            if (ReferenceEquals(x, null))
                return ReferenceEquals(y, null);
            else if (ReferenceEquals(y, null))
                return true;
            else
                return x.DColor == y.DColor;
        }

        public static bool operator !=(MyColor x, MyColor y)
        {
            return !(x == y);
        }

        public DColor DColor
        {
            get => _dColor;
            set
            {
                _dColor = value;
                _wColor = WColor.FromArgb(value.A, value.R, value.G, value.B);
            }
        }
        public DColor _dColor;


        public WColor WColor
        {
            get => _wColor;
            set
            {
                _wColor = value;
                _dColor = DColor.FromArgb(value.A, value.R, value.G, value.B);
            }
        }
        public WColor _wColor;

        public MyColor Darker(int value) =>
            new MyColor(DColor.FromArgb(DColor.A, DColor.R - value, DColor.G - value, DColor.B - value));

        public MyColor Lighter(int value) =>
            new MyColor(DColor.FromArgb(DColor.A, DColor.R + value, DColor.G + value, DColor.B + value));
    }
}
