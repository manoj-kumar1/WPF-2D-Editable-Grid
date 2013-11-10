using WpfGenericGrid.Utility;

namespace WpfGenericGrid.Message
{
    public class GridDataUpdateMessage
    {
        public object CellData { get; set; }

        public object RowHeader { get; set; }

        public GridAction ActionType { get; set; }
    }
}
