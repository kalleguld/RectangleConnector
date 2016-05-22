using System.Collections.Generic;
using System.Linq;

namespace RectangleConnector.ViewModel.DTO
{
    public class Rectangle
    {

        public Coordinate TopLeft { get; }
        public Coordinate BottomRight { get; }
        public Color Color { get; set; }
        public HashSet<Rectangle> ConnectedRectangles { get; }

        public Rectangle(Coordinate topLeft, Coordinate bottomRight, Color color, IEnumerable<Rectangle> connectedRectangles = null )
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
            Color = color;
            connectedRectangles = connectedRectangles ?? Enumerable.Empty<Rectangle>();
            ConnectedRectangles = new HashSet<Rectangle>(connectedRectangles);
        }

        public int Left => TopLeft.X;
        public int Top => TopLeft.Y;
        public int Width => BottomRight.X - TopLeft.X;
        public int Height => BottomRight.Y - TopLeft.Y;
    }
}
