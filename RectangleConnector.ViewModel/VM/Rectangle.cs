using System.Windows;
using System.Windows.Media;

namespace RectangleConnector.ViewModel.VM
{
    public class Rectangle
    {

        
        public Rect Geometry { get; }
        public Brush Brush { get; }

        public DTO.Rectangle RectangleDto { get; }

        public Rectangle(DTO.Rectangle r)
        {
            RectangleDto = r;
            var topLeft = new Point(r.TopLeft.X, r.TopLeft.Y);
            var bottomRight = new Point(r.BottomRight.X, r.BottomRight.Y);
            Geometry = new Rect(topLeft, bottomRight);
            Brush = new SolidColorBrush(Color.FromRgb(r.Color.R, r.Color.G, r.Color.B));
        }
    }
}
