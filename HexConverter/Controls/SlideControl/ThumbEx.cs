using System;

namespace HexConverter.SliderControl
{
    public class HexSliderRangeChangedEventArgs : EventArgs
    {
        public int RangeIndex { get; set; }
        public int OriginalValue { get; set; }
        public int NewValue { get; set; }
    }
}
