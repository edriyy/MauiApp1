using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class DrinkMenuPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.db");
    private List<MenuItem> _cartItems = new List<MenuItem>();

    public DrinkMenuPage(List<MenuItem> cartItems)
	{
		InitializeComponent();
        _cartItems = cartItems;
        DrinkListView.ItemsSource = GetDrinkItemsFromDatabase();
    }

    private List<MenuItem> GetDrinkItemsFromDatabase()
    {
        using (var db = new AppDbContext(_dbPath))
        {
            // Retrieve food items from the database
            return db.MenuItems.Where(item => item.ItemType == "Drink").ToList();
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
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}