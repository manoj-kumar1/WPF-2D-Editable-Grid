using System;
using System.Windows;
using System.Windows.Controls;
using GenericGrid.ViewModel;

namespace WpfGeneric2DGrid.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }
    }
}
