using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleConnector.ViewModel
{
    public class RectanglePane
    {
        public ObservableCollection<Rectangle> Rectangles { get; set; }

        public RectanglePane() : this(Enumerable.Empty<Rectangle>()) { }

        public RectanglePane(IEnumerable<Rectangle> rs)
        {
            Rectangles = new ObservableCollection<Rectangle>(rs);
        }
    }
}
