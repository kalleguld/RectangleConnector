using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using RectangleConnector.ViewModel.VM;

namespace RectangleConnector.ViewModel
{
    public class MainViewModel
    {
        public RectanglePane LeftPane { get; }
        public RectanglePane RightPane { get; }

        public ObservableCollection<VM.Rectangle> Rectangles { get; private set; }

        public MainViewModel() : this(Enumerable.Empty<DTO.Rectangle>(), Enumerable.Empty<DTO.Rectangle>())
        {
        }
        

        public MainViewModel(IEnumerable<DTO.Rectangle> leftRectangles, IEnumerable<DTO.Rectangle> rightRectangles )
        {
            var origin = new Vector();
            LeftPane = new RectanglePane(leftRectangles, origin);

            var rightOffset = (Vector) LeftPane.Geometry.TopRight;
            RightPane = new RectanglePane(rightRectangles, rightOffset);

            LeftPane.PropertyChanged += LeftPane_PropertyChanged;

            LeftPane.Rectangles.CollectionChanged += Rectangles_CollectionChanged;
            RightPane.Rectangles.CollectionChanged += Rectangles_CollectionChanged;
            Rectangles_CollectionChanged(null, null);
        }

        private void Rectangles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Rectangles = new ObservableCollection<Rectangle>(LeftPane.Rectangles.Union(RightPane.Rectangles));
        }

        private void LeftPane_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LeftPane.Geometry))
            {
                RightPane.Offset = (Vector) LeftPane.Geometry.TopRight;
            }
        }


    }
}
