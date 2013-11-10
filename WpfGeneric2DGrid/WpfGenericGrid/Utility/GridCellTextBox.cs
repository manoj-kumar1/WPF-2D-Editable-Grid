using System.Windows;
using System.Windows.Controls;

namespace WpfGenericGrid.Utility
{
    /// <summary>
    /// Custom text box for more fine-grained control of cell
    /// </summary>
    public class GridCellTextBox : TextBox
    {
        public static readonly DependencyProperty UniqueNameProperty =
            DependencyProperty.Register("UniqueName", typeof(string), typeof(GridCellTextBox));
        public static readonly DependencyProperty RowHeaderNameProperty =
            DependencyProperty.Register("RowHeaderName", typeof(string), typeof(GridCellTextBox));

        public string UniqueName
        {
            get { return (string)GetValue(UniqueNameProperty); }
            set { SetValue(UniqueNameProperty, value); }
        }

        public string RowHeaderName
        {
            get { return (string)GetValue(RowHeaderNameProperty); }
            set { SetValue(RowHeaderNameProperty, value); }
        }
    }
}
