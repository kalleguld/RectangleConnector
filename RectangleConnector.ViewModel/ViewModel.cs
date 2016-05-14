using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleConnector.ViewModel
{
    public class ViewModel
    {
        public RectanglePane LeftPane { get; set; }
        public RectanglePane RightPane { get; set; }

        public ViewModel() : this(new RectanglePane(), new RectanglePane())
        {
        }

        public ViewModel(RectanglePane leftPane, RectanglePane rightPane)
        {
            LeftPane = leftPane;
            RightPane = rightPane;
        }
    }
}
