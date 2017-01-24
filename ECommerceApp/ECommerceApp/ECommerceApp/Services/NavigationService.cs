using ECommerceApp.Pages;
using System.Threading.Tasks;
using System;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;

namespace ECommerceApp.Services
{
    public class NavigationService
    {
        private DataService dataService;

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false; //Cerrar el menu cuando elija un item

            switch (pageName)
            {
                case "CustomersPage":
                    await App.Navigator.PushAsync(new CustomersPage());
                    break;
                case "DliverysPage":
                    await App.Navigator.PushAsync(new DliverysPage());
                    break;
                case "OrdersPage":
                    await App.Navigator.PushAsync(new OrdersPage());
                    break;
                case "ProductsPage":
                    await App.Navigator.PushAsync(new ProducsPage());
                    break;
                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;
                case "SyncPage":
                    await App.Navigator.PushAsync(new SyncPage());
                    break;
                case "UserPage":
                    await App.Navigator.PushAsync(new UserPage());
                    break;
                case "LogutPage":
                    LogOut();
                    break;
                default:
                    break;
            }
        }

        internal User GetCurrentUser()
        {
            return App.CurrentUser;
        }

        public NavigationService()
        {
            dataService = new DataService();
        }


        private void LogOut()
        {
            App.CurrentUser.IsRemembered = false;
            dataService.UpdateUser(App.CurrentUser);

            App.Current.MainPage = new LoginPage();
        }

        internal void SetMainPage(User user)
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.LoadUser(user);

            App.CurrentUser = user;
            App.Current.MainPage = new MasterPage();
        }
    }
}
