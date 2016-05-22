using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace RectangleConnector.ViewModel.VM
{
    public class Rectangle : ObservableObject
    {

        private readonly IEnumerable<DTO.Rectangle> _unconnectableRectangles;
        
        public Rect Geometry { get; }
        public Brush Brush { get; private set; }

        public DTO.Rectangle RectangleDto { get; }

        public Rectangle(DTO.Rectangle r, IEnumerable<DTO.Rectangle> unconnectableRectangles)
        {
            RectangleDto = r;
            _unconnectableRectangles = unconnectableRectangles;
            var topLeft = new Point(r.TopLeft.X, r.TopLeft.Y);
            var bottomRight = new Point(r.BottomRight.X, r.BottomRight.Y);
            Geometry = new Rect(topLeft, bottomRight);
            Brush = GetBrush();
        }

        private static Color ConvertColor(DTO.Color c)
        {
            return Color.FromRgb(c.R, c.G, c.B);
        }

        public bool IsConnectable(VM.Rectangle otherRectange)
        {
            if (otherRectange == null) return false;
            return !_unconnectableRectangles.Contains(otherRectange.RectangleDto);

        }

        public void Connect(VM.Rectangle r)
        {
            if (!IsConnectable(r)) return;
            if (!r.IsConnectable(this)) return;

            if (RectangleDto.ConnectedRectangles.Add(r.RectangleDto))
            {
                r.Connect(this);
                OnConnectionsUpdated();
            }
        }

        private void OnConnectionsUpdated()
        {
            Brush = GetBrush();
            RaisePropertyChanged(() => Brush);
        }

        private Brush GetBrush()
        {
            var thisColor = ConvertColor(RectangleDto.Color);
            var result = new LinearGradientBrush(thisColor, thisColor,0);

            double offsetStep = 1.0/RectangleDto.ConnectedRectangles.Count/2;
            double offset = 0;
            foreach (var rectangle in RectangleDto.ConnectedRectangles)
            {
                offset += offsetStep;
                result.GradientStops.Add(new GradientStop(ConvertColor(rectangle.Color), offset));
                offset += offsetStep;
                result.GradientStops.Add(new GradientStop(thisColor, offset));
            }
            return result;
        }
    }
}
