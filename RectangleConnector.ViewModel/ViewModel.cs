using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RectangleConnector.ViewModel
{
    public class ViewModel
    {
        public RectanglePane LeftPane { get; set; }
        public RectanglePane RightPane { get; set; }

        public ViewModel() : this(Enumerable.Empty<DTO.Rectangle>(), Enumerable.Empty<DTO.Rectangle>())
        {
        }
        

        public ViewModel(IEnumerable<DTO.Rectangle> leftRectangles, IEnumerable<DTO.Rectangle> rightRectangles )
        {
            LeftPane = new RectanglePane(leftRectangles);
            RightPane = new RectanglePane(rightRectangles);
        }
    }
}
