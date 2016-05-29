using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using RectangleConnector.ViewModel.DTO;
using Color = System.Windows.Media.Color;

namespace RectangleConnector.ViewModel.VM
{
    public class Rectangle : ObservableObject
    {

        private readonly IReadOnlyCollection<DTO.Rectangle> _unconnectableRectangles;

        private Vector _offset;
        private Rect _geometry;
        private Brush _brush;

        public Vector Offset
        {
            get { return _offset; }
            set
            {
                _offset = value;
                Geometry = GetGeometry(RectangleDto, _offset);
                RaisePropertyChanged();
            }
        }

        public Rect Geometry
        {
            get { return _geometry; }
            private set
            {
                _geometry = value;
                RaisePropertyChanged();
            }
        }

        public Brush Brush
        {
            get { return _brush; }
            private set
            {
                _brush = value;
                RaisePropertyChanged();
            }
        }

        public DTO.Rectangle RectangleDto { get; }


        public Rectangle(DTO.Rectangle r, Vector offset, IReadOnlyCollection<DTO.Rectangle> unconnectableRectangles)
        {
            RectangleDto = r;
            _unconnectableRectangles = unconnectableRectangles;
            Geometry = GetGeometry(r, offset);
            Brush = GetBrush();
        }

        private static Rect GetGeometry(DTO.Rectangle rectangleDto, Vector offset)
        {
            var topLeft = new Point(rectangleDto.TopLeft.X + offset.X, rectangleDto.TopLeft.Y + offset.Y);
            var bottomRight = new Point(rectangleDto.BottomRight.X + offset.X, rectangleDto.BottomRight.Y + offset.Y);
            var geometry = new Rect(topLeft, bottomRight);
            return geometry;
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
