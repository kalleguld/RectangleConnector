using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RectangleConnector.ViewModel.VM
{
    public class RectangleConnection
    {
        public VM.Rectangle R1 { get; }
        public VM.Rectangle R2 { get; }

        public RectangleConnection(Rectangle r1, Rectangle r2)
        {
            R1 = r1;
            R2 = r2;
        }

        protected bool Equals(RectangleConnection other)
        {
            return Equals(R1, other.R1) && Equals(R2, other.R2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((R1?.GetHashCode() ?? 0)*397) ^ (R2?.GetHashCode() ?? 0);
            }
        }
    }
}
