using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HexConverter.RangeControl
{
    /// <summary>
    /// RangeToolButton.xaml 的交互逻辑
    /// </summary>
    public partial class RangeToolButton : UserControl
    {
        public RangeToolButton()
        {
            InitializeComponent();
        }


        public event EventHandler<RoutedEventArgs> AddClick;
        public event EventHandler<RoutedEventArgs> RemoveClick;
        
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddClick?.Invoke(this,e);
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            RemoveClick?.Invoke(this, e);
        }
    }
}
