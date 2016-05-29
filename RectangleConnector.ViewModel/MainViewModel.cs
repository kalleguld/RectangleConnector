using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using RectangleConnector.ViewModel.VM;

namespace RectangleConnector.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RectanglePane LeftPane { get; }
        public RectanglePane RightPane { get; }

        public ObservableCollection<VM.Rectangle> Rectangles { get; }

        public ObservableCollection<VM.RectangleConnection> Connections { get; } = new ObservableCollection<RectangleConnection>();


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

            Rectangles = new ObservableCollection<Rectangle>(LeftPane.Rectangles.Union(RightPane.Rectangles));
            LeftPane.Rectangles.CollectionChanged += Rectangles_CollectionChanged;
            RightPane.Rectangles.CollectionChanged += Rectangles_CollectionChanged;

            foreach (var rect in LeftPane.Rectangles)
            {
                rect.ConnectedRectangles.CollectionChanged += GetConnectionsChangedHandler(rect);
            }

            LeftPane.Rectangles.CollectionChanged += LeftPane_Rectangles_changed;

        }

        private void LeftPane_Rectangles_changed(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Rectangle rectangle in e.OldItems)
                {
                    rectangle.ConnectedRectangles.CollectionChanged -= GetConnectionsChangedHandler(rectangle);
                }
            }

            if (e.NewItems != null)
            {
                foreach (Rectangle rectangle in e.NewItems)
                {
                    rectangle.ConnectedRectangles.CollectionChanged += GetConnectionsChangedHandler(rectangle);
                }
            }
        }

        private System.Collections.Specialized.NotifyCollectionChangedEventHandler GetConnectionsChangedHandler(
            VM.Rectangle sourceRectangle)
        {
            return (sender, e) =>
            {
                if (e.OldItems != null)
                {
                    foreach (Rectangle targetRectangle in e.OldItems)
                    {
                        Connections.Remove(new RectangleConnection(sourceRectangle, targetRectangle));
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (Rectangle targetRectangle in e.NewItems)
                    {
                        Connections.Add(new RectangleConnection(sourceRectangle, targetRectangle));
                    }
                }
            };
        }

        private void Rectangles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Rectangle rectangle in e.OldItems)
                {
                    Rectangles.Remove(rectangle);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Rectangle rectangle in e.NewItems)
                {
                    Rectangles.Add(rectangle);
                }
            }
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
