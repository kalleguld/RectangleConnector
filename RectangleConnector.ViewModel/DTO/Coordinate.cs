using System;

namespace RectangleConnector.ViewModel.DTO
{
    public struct Coordinate
    {
        public readonly int X;
        public readonly int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Coordinate(Tuple<int, int> t)
        {
            return new Coordinate(t.Item1, t.Item2);
        }
    }
}
