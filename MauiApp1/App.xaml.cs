﻿namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            //MainPage = new FoodMenuPage();
        }
    }
}
