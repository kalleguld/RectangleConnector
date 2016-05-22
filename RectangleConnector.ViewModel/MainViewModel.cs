using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RectangleConnector.ViewModel
{
    public class MainViewModel
    {
        public RectanglePane LeftPane { get; set; }
        public RectanglePane RightPane { get; set; }

        public MainViewModel() : this(Enumerable.Empty<DTO.Rectangle>(), Enumerable.Empty<DTO.Rectangle>())
        {
        }
        

        public MainViewModel(IEnumerable<DTO.Rectangle> leftRectangles, IEnumerable<DTO.Rectangle> rightRectangles )
        {
            LeftPane = new RectanglePane(leftRectangles);
            RightPane = new RectanglePane(rightRectangles);
        }
    }
}
