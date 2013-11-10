using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfGenericGrid.Utility;

namespace WpfGeneric2DGrid.UserControls
{
    /// <summary>
    /// Interaction logic for Generic2DGrid.xaml
    /// </summary>
    public partial class CustomProductGrid : UserControl
    {
        public CustomProductGrid()
        {
            InitializeComponent();
        }

        private void SvGridCells_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0.0f)
            {
                try
                {
                    SvColHeaders.ScrollToHorizontalOffset(e.HorizontalOffset);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                SvRowYAxis.ScrollToVerticalOffset(e.VerticalOffset);
            }
        }

        private void CellTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textCellValue_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var textBox = (GridCellTextBox)sender;
            if (textBox != null)
            {
                var id = textBox.UniqueName;
                if (id == null) //For enter to work, we need to have unique id specified in the model
                    return;
                var nameParts = id.Split('_');
                if (nameParts.Count() == 3)
                {
                    var row = Convert.ToInt32(nameParts[1]) + 1;
                    var col = Convert.ToInt32(nameParts[2]);

                    //find next row item
                    var found = false;
                    dynamic nextRowItem = null;
                    foreach (var item in RowsByYAxis.Items)
                    {
                        var key = ((KeyValuePair<object, ObservableCollection<object>>)(item)).Key;
                        if (found)
                        {
                            nextRowItem = item;
                            break;
                        }
                        if ((key as RowHeader).Name == textBox.RowHeaderName)
                        {
                            found = true;
                        }
                    }
                    var cp = RowsByYAxis.ItemContainerGenerator.ContainerFromItem(nextRowItem) as ContentPresenter;
                    var textBoxes = FindVisualChildren<GridCellTextBox>(cp);
                    var nextRowTextBox = textBoxes.FirstOrDefault(t => t.UniqueName == string.Format("{0}_{1}_{2}", nameParts[0], row, col));
                    if (nextRowTextBox != null)
                    {
                        nextRowTextBox.Focus();
                        return;
                    }
                }
            }
            SvGridCells.Focus();
        }

        public static List<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            var list = new List<T>();
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        list.Add((T)child);
                    }

                    List<T> childItems = FindVisualChildren<T>(child);
                    if (childItems != null && childItems.Any())
                    {
                        list.AddRange(childItems);
                    }
                }
            }
            return list;
        }
    }
}
