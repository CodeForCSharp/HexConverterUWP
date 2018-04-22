using HexConverter.SliderControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace HexConverter.SlideControl
{
    public sealed partial class HexThumb : UserControl
    {
        private readonly Rectangle _leftRect;
        private readonly Rectangle _rightRect;
        private readonly HexSlider _parentSlider;
        public HexThumb(Rectangle leftRect, Rectangle rightRect, HexSlider parentSlider)
        {
            this.InitializeComponent();
            _leftRect = leftRect;
            _rightRect = rightRect;
            _parentSlider = parentSlider;
            Thumb.DragDelta += ThumbEx_DragDelta;
            Thumb.DragStarted += ThumbEx_DragStarted;
            Thumb.DragCompleted += ThumbEx_DragCompleted;
        }

        private void ThumbEx_DragStarted(object sender, DragStartedEventArgs e)
        {

        }

        private void ThumbEx_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if ((_leftRect.Width + e.HorizontalChange) >= 0 && (_rightRect.Width - e.HorizontalChange) >= 0)
            {
                _leftRect.Width += e.HorizontalChange;
                _rightRect.Width -= e.HorizontalChange;

                var sliderLeft = Canvas.GetLeft(Thumb);
                sliderLeft += e.HorizontalChange;
                Canvas.SetLeft(Thumb, sliderLeft);
            }
        }

        private void ThumbEx_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (_leftRect.Parent is StackPanel ancestor)
            {
                var newLeft = _leftRect.Width / _parentSlider.ContentWidth * _parentSlider.Maxium;
                var newRight = _rightRect.Width / _parentSlider.ContentWidth * _parentSlider.Maxium;

                var roundedLeft = (int)Math.Round(newLeft);
                var roundedRight = (int)Math.Round(newRight);
                if (roundedLeft < newLeft && roundedRight < newRight)
                {
                    roundedRight++;
                }

                if (roundedLeft > newLeft && roundedRight > newRight)
                {
                    roundedRight--;
                }

                _leftRect.Width = roundedLeft * _parentSlider.ContentWidth / _parentSlider.Maxium;
                _rightRect.Width = roundedRight * _parentSlider.ContentWidth / _parentSlider.Maxium;
                var point = _leftRect.TransformToVisual(ancestor).TransformPoint(new Point(0, 0));
                Canvas.SetLeft(this, point.X + _leftRect.Width);
                _parentSlider.DragCompleted(this, roundedLeft, roundedRight);
            }
        }
    }
}
