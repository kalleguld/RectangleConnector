using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RectangleConnector.View
{
    public partial class RectanglePaneView : System.Windows.Controls.UserControl
    {
        private Point _dragStartPoint;

        public RectanglePaneView()
        {
            InitializeComponent();
        }

        private void Rectangle_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }

        private void Rectangle_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            var diff = _dragStartPoint - e.GetPosition(null);
            if (!(Math.Abs(diff.X) >= SystemParameters.MinimumHorizontalDragDistance) ||
                !(Math.Abs(diff.Y) >= SystemParameters.MinimumVerticalDragDistance)) return;

            var sysRect = sender as Rectangle;
            var vmRect = sysRect?.DataContext;
            if (vmRect == null) return;

            DragDrop.DoDragDrop(this, vmRect, DragDropEffects.Link);
        }

        private DragDropEffects _effectsCache;
        private void Rectangle_OnDragEnter(object sender, DragEventArgs e)
        {
            var targetRect = GetRect(sender);
            var draggedRectangle = e.Data.GetData(typeof(RectangleConnector.ViewModel.VM.Rectangle)) as ViewModel.VM.Rectangle;
            _effectsCache = e.Effects = (targetRect?.IsConnectable(draggedRectangle) == true)
                ? DragDropEffects.Link
                : DragDropEffects.None;
            e.Handled = true;
        }

        private static ViewModel.VM.Rectangle GetRect(object sender)
        {
            var sysRect = sender as Rectangle;
            var targetRect = sysRect?.DataContext as ViewModel.VM.Rectangle;
            return targetRect;
        }

        private void Rectangle_OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = _effectsCache;
            e.Handled = true;
        }

        private void Rectangle_OnDrop(object sender, DragEventArgs e)
        {
            var targetRect = GetRect(sender);
            var draggedRectangle = e.Data.GetData(typeof(ViewModel.VM.Rectangle)) as ViewModel.VM.Rectangle;

            if (targetRect?.IsConnectable(draggedRectangle) == true)
            {
                targetRect.Connect(draggedRectangle);
            }
        }
    }
}
