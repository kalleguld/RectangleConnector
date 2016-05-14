using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleConnector.ViewModel
{
    public class Rectangle
    {
        public Coordinate TopLeft { get; set; }
        public Coordinate BottomRight { get; set; }
        public Color Color { get; set; }

        public Rectangle(Coordinate topLeft, Coordinate bottomRight, Color color)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
            Color = color;
        }

        public double Left { get { return TopLeft.X; } }
        public double Top { get { return TopLeft.Y; } }
        public double Width  { get { return BottomRight.X - TopLeft.X; } }
        public double Height { get { return BottomRight.Y - TopLeft.Y; } }

    }
}
