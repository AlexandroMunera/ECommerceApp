using ECommerceApp.Models;
using ECommerceApp.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerceApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {

        #region Attributes

        private NavigationService navigationService;

        private DialogService dialogService;

        private ApiService apiService;

        private DataService dataService;

        public bool isRunning { get; set; }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public string User { get; set; }

        public string Password { get; set; }

        public bool IsRemembered { get; set; }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        #endregion

        #region Constructors

        public LoginViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            apiService = new ApiService();
            dataService = new DataService();

            IsRemembered = true; //Por defecto poner el recordar en true
        } 

        #endregion

        #region Commands

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            if (string.IsNullOrEmpty(User))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar un usuario");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar una contraseña");
                return;
            }

            IsRunning = true;
            var response = await apiService.Login(User, Password);
            IsRunning = false;

            if (! response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var user = (User)response.Result;

            user.IsRemembered = IsRemembered;
            user.Password = Password;

            dataService.InsertUser(user);
            navigationService.SetMainPage(user);
        } 

        #endregion
    }
}
