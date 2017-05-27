using ECommerceApp.Models;
using ECommerceApp.Services;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.ComponentModel;

namespace ECommerceApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Singleton

        static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        #endregion

        #region Attributes

        private DataService dataService;

        private ApiService apiService;

        private NetServices netService;

        private string productsFilter;

        private string customersFilter;



        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public ObservableCollection<ProductItemViewModel> Products { get; set; }

        public ObservableCollection<CustomerItemViewModel> Customers { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public UserViewModel UserLoged { get; set; }

        public string ProductsFilter
        {
            set
            {
                if (productsFilter != value)
                {
                    productsFilter = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ProductsFilter"));
                    }

                    if (string.IsNullOrEmpty(productsFilter))
                    {
                        LoadLocalProducts();
                    }
                }
            }
            get
            {
                return productsFilter;
            }
        }

        public string CustomersFilter
        {
            set
            {
                if (customersFilter != value)
                {
                    customersFilter = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CustomersFilter"));
                    }

                    if (string.IsNullOrEmpty(customersFilter))
                    {
                        LoadLocalCustomers();
                    }
                }
            }
            get
            {
                return customersFilter;
            }
        }

       


        #endregion

        #region Constructors
        public MainViewModel()
        {
            //Singleton
            instance = this; 

            //Create observable collections
            Menu = new ObservableCollection<MenuItemViewModel>();
            Products = new ObservableCollection<ProductItemViewModel>();
            Customers = new ObservableCollection<CustomerItemViewModel>();

            //Create views
            NewLogin = new LoginViewModel();
            UserLoged = new UserViewModel();

            //Instance services
            dataService = new DataService();
            apiService = new ApiService();
            netService = new NetServices();

            //Load data
            LoadMenu();
            LoadProducts();
            LoadCustomers();
            
        }       


        #endregion

        #region Commands

        public ICommand SearchProductCommand { get { return new RelayCommand(SearchProduct); } }

        private void SearchProduct()
        {
            var products = dataService.GetProducts(ProductsFilter);
            ReloadProducts(products);           

        }

        public ICommand SearchCustomerCommand { get { return new RelayCommand(SearchCustomer); } }

        private void SearchCustomer()
        {
            var customers = dataService.GetCustomers(CustomersFilter);
            ReloadCustomers(customers);
        }


        #endregion

        #region Methods

        public void LoadUser(User user)
        {
                UserLoged.FullName = user.FullName;
                UserLoged.Photo = user.PhotoFullPath;
            
        }

        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_product.png",
                PageName = "ProductsPage",
                Title = "Productos"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_customer.png",
                PageName = "CustomersPage",
                Title = "Clientes"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_order.png",
                PageName = "OrdersPage",
                Title = "Pedidos"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_delivery.png",
                PageName = "DliverysPage",
                Title = "Entregas"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_sync.png",
                PageName = "SyncPage",
                Title = "Sincronizar"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_setup.png",
                PageName = "SetupPage",
                Title = "Configuración"
            });

            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_logout.png",
                PageName = "LogutPage",
                Title = "Cerrar Sesion"
            });
        }

        private async void LoadProducts()
        {
            var products = new List<Product>();

            if (netService.IsConnected())
            {
                products = await apiService.GetProducts();
                dataService.SaveProducts(products);
            }
            else
            {
                products = dataService.GetProducts();
            }

            ReloadProducts(products);
        }

        private void ReloadProducts(List<Product> products)
        {
            Products.Clear();

            foreach (var p in products)
            {
                Products.Add(new ProductItemViewModel
                {
                    BarCode = p.BarCode,
                    Category = p.Category,
                    CategoryId = p.CategoryId,
                    Company = p.Company,
                    CompanyId = p.CompanyId,
                    Description = p.Description,
                    Image = p.Image,
                    Inventories = p.Inventories,
                    Price = p.Price,
                    ProductId = p.ProductId,
                    Remarks = p.Remarks,
                    Stock = p.Stock,
                    Tax = p.Tax,
                    TaxId = p.TaxId
                });
            }
        }

        private void LoadLocalProducts()
        {
            var products = dataService.GetProducts();
            ReloadProducts(products);
        }

        private async void LoadCustomers()
        {
            var customers = new List<Customer>();

            if (netService.IsConnected())
            {
                customers = await apiService.GetCustomers();
                dataService.SaveCustomers(customers);
            }
            else
            {
                customers = dataService.GetCustomers();
            }

            ReloadCustomers(customers);
        }

        private void ReloadCustomers(List<Customer> customers)
        {
            Customers.Clear();

            foreach (var c in customers)
            {
                Customers.Add(new CustomerItemViewModel
                {
                    Address = c.Address,
                    City = c.City,
                    CityId = c.CityId,
                    CompanyCustomers = c.CompanyCustomers,
                    CustomerId = c.CustomerId,
                    Department = c.Department,
                    DepartmentId = c.DepartmentId,
                    FirstName = c.FirstName,
                    IsUpdated = c.IsUpdated,
                    LastName = c.LastName,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Orders = c.Orders,
                    Phone = c.Phone,
                    Photo = c.Photo,
                    Sales = c.Sales,
                    UserName = c.UserName
                });
            }
        }

        private void LoadLocalCustomers()
        {
            var customers = dataService.GetCustomers();
            ReloadCustomers(customers);
        }

        #endregion
    }

}
