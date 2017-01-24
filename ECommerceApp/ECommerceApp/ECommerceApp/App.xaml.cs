﻿using ECommerceApp.Pages;
using ECommerceApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ECommerceApp.Models;
using ECommerceApp.ViewModels;

namespace ECommerceApp
{
    public partial class App : Application
    {
        #region Attributes

        private DataService dataService;

        #endregion

        #region Properties
        public static NavigationPage Navigator { get; internal set; }

        public static MasterPage Master { get; internal set; }

        public static User CurrentUser { get; internal set; }
        #endregion

        #region Contructors
        public App()
        {
            InitializeComponent();

            dataService = new DataService();

            var user = dataService.GetUser();

            if (user != null && user.IsRemembered)
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.LoadUser(user);

                App.CurrentUser = user;
                MainPage = new MasterPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
            
        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

            #endregion}

        }
    }
}
