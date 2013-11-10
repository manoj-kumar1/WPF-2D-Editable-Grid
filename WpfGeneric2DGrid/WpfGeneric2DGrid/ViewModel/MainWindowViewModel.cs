using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WpfGenericGrid.ViewModel;
using WpfGeneric2DGrid.Model;
using WpfGenericGrid.Utility;
using WpfGenericGrid.Message;

namespace GenericGrid.ViewModel
{
    class AppMeta
    {
        public int Id { get; set; }
    }
    class MainWindowViewModel : ViewModelBase
    {
        private GenericGridViewModel _gridViewModel;
        
        public MainWindowViewModel()
        {
            List<Product> products = CreateSampleData();

            var colHeaders = products.GroupBy(p => p.City).FirstOrDefault().Select(p => new ColumnHeader {Name = p.BrandName, Description = p.BrandName});
            
            var rowHeaders = products.GroupBy(p => p.BrandName).FirstOrDefault().Select(p => new RowHeader {Name = p.City, Description = p.City});

            var rowByYAxis = products.GroupBy(p => p.City).ToDictionary(p => p.Key, p => p.ToList());
            var observRowByYAxis = new ObservableDictionary<object, ObservableCollection<object>>();

            rowByYAxis.Keys.ToList().ForEach(key => observRowByYAxis.Add(rowHeaders.FirstOrDefault(r => r.Name == key), new ObservableCollection<object>(rowByYAxis[key])));

            GridViewModel = new GenericGridViewModel("productGrid")
            {
                RowHeight = 30,
                XAxisColumnWidth = 100,
                YAxisWidth = 120,
                GridHeight = 200,
                GridWidth = 700,
                Columns = colHeaders,
                RowsByYAxis = observRowByYAxis,
                CanDelete = true,
                HorizontalScrollBarVisible = true,
                VerticalScrollBarVisible = true,
                YAxisHeaderText = "City"
            };

            //2nd grid
            products = CreateSampleData();

            colHeaders = products.GroupBy(p => p.City).FirstOrDefault().Select(p => new ColumnHeader { Name = p.BrandName, Description = p.BrandName });

            rowHeaders = products.GroupBy(p => p.BrandName).FirstOrDefault().Select(p => new RowHeader { Name = p.City, Description = p.City });

            rowByYAxis = products.GroupBy(p => p.City).ToDictionary(p => p.Key, p => p.ToList());
            observRowByYAxis = new ObservableDictionary<object, ObservableCollection<object>>();

            rowByYAxis.Keys.ToList().ForEach(key => observRowByYAxis.Add(rowHeaders.FirstOrDefault(r => r.Name == key), new ObservableCollection<object>(rowByYAxis[key])));

            GridViewModel2 = new GenericGridViewModel("productGridDetail")
            {
                RowHeight = 30,
                XAxisColumnWidth = 135,
                YAxisWidth = 120,
                Columns = colHeaders,
                RowsByYAxis = observRowByYAxis,
                CanDelete = true,
                HorizontalScrollBarVisible = true,
                VerticalScrollBarVisible = true,
                YAxisHeaderText = "City"
            };

            Messenger.Default.Register<GenericMessage<GridDataUpdateMessage>>(this, (a) =>
            {
                //Updated data
                var message = a.Content;
                if (message.ActionType == GridAction.CellUpdate)
                {
                    //Cell value change
                }
                else if (message.ActionType == GridAction.RowDelete) { 
                    //Row deleted
                }
            });
            Load = new RelayCommand(() =>
                {
                    //Run some logic
                });
        }

        public RelayCommand Load { get; set; }

        public GenericGridViewModel GridViewModel
        {
            get { return _gridViewModel; }
            private set 
            { 
                _gridViewModel = value;
            }
        }

        public GenericGridViewModel GridViewModel2
        {
            get;
            set;
        }

        private List<Product> CreateSampleData()
        {
            return new List<Product>
            {
                new Product { ProductType = "Notebook", BrandName = "LG",
                    City = "Hyderabad",
                    Value = "50.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Samsung",
                    City = "Hyderabad",
                    Value = "50",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Lenovo",
                    City = "Hyderabad",
                    Value = "49",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Dell",
                    City = "Hyderabad",
                    Value = "51.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Toshiba",
                    City = "Hyderabad",
                    Value = "55",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "LG",
                    City = "Bangalore",
                    Value = "50.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Samsung",
                    City = "Bangalore",
                    Value = "50",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Lenovo",
                    City = "Bangalore",
                    Value = "49",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Dell",
                    City = "Bangalore",
                    Value = "51.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Toshiba",
                    City = "Bangalore",
                    Value = "55",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "LG",
                    City = "Mumbai",
                    Value = "50.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Samsung",
                    City = "Mumbai",
                    Value = "50",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Lenovo",
                    City = "Mumbai",
                    Value = "49",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Dell",
                    City = "Mumbai",
                    Value = "51.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Toshiba",
                    City = "Mumbai",
                    Value = "55",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "LG",
                    City = "Delhi",
                    Value = "50.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Samsung",
                    City = "Delhi",
                    Value = "50",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Lenovo",
                    City = "Delhi",
                    Value = "49",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Dell",
                    City = "Delhi",
                    Value = "51.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Toshiba",
                    City = "Delhi",
                    Value = "55",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "LG",
                    City = "Kolkata",
                    Value = "50.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Samsung",
                    City = "Kolkata",
                    Value = "50",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Lenovo",
                    City = "Kolkata",
                    Value = "49",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Dell",
                    City = "Kolkata",
                    Value = "51.5",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                },
                new Product {
                    ProductType = "Notebook",
                    BrandName = "Toshiba",
                    City = "Kolkata",
                    Value = "55",
                    Config = new Configuration {
                        Processor = "2.5 Ghz",
                        RAM = "8 GB",
                        ScreenSize = "15\""
                    }
                }
            };
        } 
    }
}
