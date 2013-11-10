using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using WpfGenericGrid.Message;

namespace WpfGeneric2DGrid.Model
{
    class Product
    {
        private string _price;

        public string ProductType { get; set; }
        
        public string BrandName { get; set; }

        public string City { get; set; }

        public Configuration Config { get; set; }

        public string Value
        {
            get { return _price; }
            set
            {
                if (_price != null)
                {
                    try
                    {
                        var val = Math.Round(Convert.ToDouble(value), 2);
                        _price = val.ToString();
                    }
                    catch (Exception)
                    {
                        _price = string.Empty;
                        throw;
                    }

                    Messenger.Default.Send(new GenericMessage<GridDataUpdateMessage>(new GridDataUpdateMessage() { CellData = this, ActionType = WpfGenericGrid.Utility.GridAction.CellUpdate }));
                }
                else
                    _price = value;
            }
        }
    }
}
