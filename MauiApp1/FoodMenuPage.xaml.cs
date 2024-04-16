using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class FoodMenuPage : ContentPage
    {
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.db");
        private List<MenuItem> _cartItems = new List<MenuItem>(); // Declaring _cartItems here

        public FoodMenuPage(List<MenuItem> cartItems)
        {
            InitializeComponent();
            _cartItems = cartItems;
            FoodListView.ItemsSource = GetFoodItemsFromDatabase();
        }

        private List<MenuItem> GetFoodItemsFromDatabase()
        {
            using (var db = new AppDbContext(_dbPath))
            {
                // Retrieve food items from the database
                return db.MenuItems.Where(item => item.ItemType == "Food").ToList();
            }
        }

        private void AddToCartButton_Clicked(object sender, EventArgs e)
        {
            var menuItem = (sender as Button)?.BindingContext as MenuItem;
            if (menuItem != null)
            {
                // Add your logic to add the selected item to the cart
                // For demonstration, we'll just display an alert
                DisplayAlert("Success", $"{menuItem.Name} added to cart.", "OK");

                _cartItems.Add(menuItem); // Add the selected item to the cart
            }
        }

        private async void ViewCartButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage(_cartItems));
        }
    }
}