using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WpfGenericGrid.Message;
using WpfGenericGrid.Utility;

namespace WpfGenericGrid.ViewModel
{
    public class GenericGridViewModel : ViewModelBase
    {
        private ObservableDictionary<object, ObservableCollection<object>> _rowsByYAxis;
        private double _xAxisColumnWidth = 100;
        private double _yAxisWidth = 150;
        private double _xAxisHeight = 30;
        private double _rowHeight = 30;
        private double _gridWidth = 600;
        private double _gridHeight = 170;
        private string _gridName = string.Empty;
        private bool _isVerticalScrollVisible;
        private bool _isHorizontalScrollVisible;
        private double _gridXAxisHeaderWidth;
        private double _gridYAxisHeaderHeight;
        private double _gridBodyScrollHeight;
        private double _gridBodyScrollWidth;
        private ObservableCollection<object> _rowsHeader;
        private string _gridYAxisHeaderMargin;

        public GenericGridViewModel(string gridName)
        {
            _gridName = gridName;
            HorizontalScrollBarVisible = true;
            VerticalScrollBarVisible = true;
            DeleteRow = new RelayCommand<object>(rowIdentifier =>
            {
                if (!RowsByYAxis.ContainsKey(rowIdentifier)) return;
                var rowHeaderToRemove = RowsByYAxis[rowIdentifier];
                _rowsByYAxis.Remove(rowIdentifier);
                _rowsHeader.Remove(rowIdentifier);
                Messenger.Default.Send(
                    new GenericMessage<GridDataUpdateMessage>(new GridDataUpdateMessage()
                        {
                            RowHeader = rowHeaderToRemove,
                            ActionType = GridAction.RowDelete
                        }));
                RowsByYAxis = _rowsByYAxis;
            });
            Columns = new ObservableCollection<object>();
            RowsByYAxis = new ObservableDictionary<object, ObservableCollection<object>>();
        }

        protected void ComputeGridLayout(int colCount, int rowCount)
        {
            _isVerticalScrollVisible = GridHeight - RowHeight < rowCount * RowHeight;
            _isHorizontalScrollVisible = GridWidth - YAxisWidth < colCount * CellWidth;

            GridXAxisHeaderWidth = HorizontalScrollBarVisible ? _isHorizontalScrollVisible ? GridWidth - YAxisWidth : colCount * CellWidth : GridWidth - YAxisWidth;
            GridYAxisHeaderHeight = VerticalScrollBarVisible ? _isVerticalScrollVisible ? GridHeight - RowHeight : rowCount * RowHeight : GridHeight - RowHeight;

            GridBodyScrollWidth = _isVerticalScrollVisible ? GridXAxisHeaderWidth + 18 : GridXAxisHeaderWidth;
            GridBodyScrollHeight = _isHorizontalScrollVisible ? GridYAxisHeaderHeight + 18 : GridYAxisHeaderHeight;

            GridYAxisHeaderMargin = HorizontalScrollBarVisible || VerticalScrollBarVisible ? _isVerticalScrollVisible || _isHorizontalScrollVisible ? "0 -20 0 0" : "0" : "0";
        }

        public double XAxisColumnWidth
        {
            get { return _xAxisColumnWidth; }
            set { _xAxisColumnWidth = value; }
        }

        public double YAxisWidth
        {
            get { return _yAxisWidth; }
            set { _yAxisWidth = value; }
        }

        public double RowHeight
        {
            get { return _rowHeight; }
            set { _rowHeight = value; }
        }

        public double XAxisHeight
        {
            get { return _xAxisHeight; }
            set { _xAxisHeight = value; }
        }

        public double CellWidth
        {
            get { return XAxisColumnWidth + 1; }
        }
        public double GridWidth
        {
            get { return _gridWidth; }
            set { _gridWidth = value; }
        }

        public double GridHeight
        {
            get { return _gridHeight; }
            set { _gridHeight = value; }
        }

        public double GridXAxisHeaderWidth
        {
            get { return _gridXAxisHeaderWidth; }
            private set { _gridXAxisHeaderWidth = value; RaisePropertyChanged("GridXAxisHeaderWidth"); }
        }

        public double GridYAxisHeaderHeight
        {
            get { return _gridYAxisHeaderHeight; }
            private set { _gridYAxisHeaderHeight = value; RaisePropertyChanged("GridYAxisHeaderHeight"); }
        }

        public double GridBodyScrollHeight
        {
            get { return _gridBodyScrollHeight; }
            private set { _gridBodyScrollHeight = value; RaisePropertyChanged("GridBodyScrollHeight"); }
        }

        public double GridBodyScrollWidth
        {
            get { return _gridBodyScrollWidth; }
            private set { _gridBodyScrollWidth = value; RaisePropertyChanged("GridBodyScrollWidth"); }
        }

        public string YAxisHeaderText { get; set; }
        public bool IsReadOnly { get; set; }
        public bool CanDelete { get; set; }
        public bool HorizontalScrollBarVisible { get; set; }
        public bool VerticalScrollBarVisible { get; set; }
        public RelayCommand<object> DeleteRow { get; protected set; }
        public IEnumerable<object> Columns { get; set; }
        public string GridYAxisHeaderMargin
        {
            get { return _gridYAxisHeaderMargin; }
            private set { _gridYAxisHeaderMargin = value; RaisePropertyChanged("GridYAxisHeaderMargin"); }
        }
        public ObservableCollection<object> RowsHeader
        {
            get { return _rowsHeader; }
            private set { _rowsHeader = value; RaisePropertyChanged("RowsHeader"); }
        }
        public ObservableDictionary<object, ObservableCollection<object>> RowsByYAxis
        {
            get { return _rowsByYAxis; }
            set
            {
                _rowsByYAxis = value;
                if (_rowsByYAxis.Any())
                {
                    ComputeGridLayout(Columns.Count(), _rowsByYAxis.Count);
                    RowsHeader = new ObservableCollection<object>(_rowsByYAxis.Keys);
                }
                RaisePropertyChanged("RowsByYAxis");
            }
        }
    }
}
