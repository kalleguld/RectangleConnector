using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using RectangleConnector.ViewModel.DTO;
using RectangleConnector.ViewModel.VM;
using Rectangle = RectangleConnector.ViewModel.VM.Rectangle;

namespace RectangleConnector.ViewModel
{
    public class RectanglePane
    {
        public ObservableCollection<Rectangle> Rectangles { get; }

        public RectanglePane() : this(Enumerable.Empty<DTO.Rectangle>()) { }

        public RectanglePane(IEnumerable<DTO.Rectangle> rs)
        {
            Rectangles = new ObservableCollection<Rectangle>(rs.Select(r => new Rectangle(r)));
        }

    }
}
