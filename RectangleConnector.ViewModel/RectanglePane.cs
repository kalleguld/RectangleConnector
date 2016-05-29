using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;
using RectangleConnector.ViewModel.DTO;
using RectangleConnector.ViewModel.VM;
using Rectangle = RectangleConnector.ViewModel.VM.Rectangle;

namespace RectangleConnector.ViewModel
{
    public class RectanglePane : ObservableObject
    {
        private Vector _offset;
        private Rect _geometry;

        public ObservableCollection<Rectangle> Rectangles { get; }

        public Vector Offset
        {
            get { return _offset; }
            set
            {
                _offset = value;
                foreach (var rectangle in Rectangles)
                {
                    rectangle.Offset = Offset;
                }
                RaisePropertyChanged();
                ResetGeometry();
            }
        }

        public Rect Geometry
        {
            get { return _geometry; }
            set
            {
                _geometry = value;
                RaisePropertyChanged();
            }
        }


        public RectanglePane(IEnumerable<DTO.Rectangle> rs, Vector offset)
        {
            var rList = rs.ToList();
            var vmRectangles = new List<VM.Rectangle>();

            foreach (var r in rList)
            {
                var vmR = new VM.Rectangle(r, offset, rList);
                vmRectangles.Add(vmR);
                vmR.PropertyChanged += Rectangle_PropertyChanged;
            }

            Rectangles = new ObservableCollection<Rectangle>(vmRectangles);

            Rectangles.CollectionChanged += Rectangles_CollectionChanged;

            ResetGeometry();
        }

        private void Rectangle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(VM.Rectangle.Geometry)) return;

            var senderRectangle = (VM.Rectangle) sender;
            if (!Geometry.Contains(senderRectangle.Geometry))
            {
                ResetGeometry();
            }
        }

        private void Rectangles_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Move) return;

            foreach (VM.Rectangle rectangle in e.NewItems)
            {
                rectangle.PropertyChanged += Rectangle_PropertyChanged;
            }
            foreach (VM.Rectangle rectangle in e.OldItems)
            {
                rectangle.PropertyChanged -= Rectangle_PropertyChanged;
            }
            ResetGeometry();
        }

        private void ResetGeometry()
        {
            var rsList = Rectangles.ToList();
            var xMin = rsList.Min(r => r.Geometry.X);
            var yMin = rsList.Min(r => r.Geometry.Y);
            var xMax = rsList.Max(r => r.Geometry.X + r.Geometry.Width);
            var yMax = rsList.Max(r => r.Geometry.Y + r.Geometry.Height);
            Geometry = new Rect(new Point(xMin, yMin), new Point(xMax, yMax));
        }
    }
}
