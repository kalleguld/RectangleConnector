using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleConnector.ViewModel
{
    public struct Coordinate
    {
        public readonly double X;
        public readonly double Y;

        public Coordinate(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Coordinate(Tuple<double, double> t)
        {
            return new Coordinate(t.Item1, t.Item2);
        }
    }
}
