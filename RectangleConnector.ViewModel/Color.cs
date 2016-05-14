using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleConnector.ViewModel
{
    public struct Color
    {
        public readonly byte R;
        public readonly byte G;
        public readonly byte B;

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B= b;
        }

        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color White = new Color(255,255,255);
        public static readonly Color Red = new Color(255, 0, 0);
        public static readonly Color Green = new Color(0, 255, 0);
        public static readonly Color Blue = new Color(0, 0, 255);


    }
}
