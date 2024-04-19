using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class DrinkMenuPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.db");
    private readonly string _orderDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "order_cart3.db");
    public DrinkMenuPage()
    {
        InitializeComponent();
        LoadDrinkItems();
    }
    private void LoadDrinkItems()
    {
        // Assuming you have a method to retrieve drink items from your database
        List<MenuItem> drinkItems = GetDrinkItemsFromDatabase();
        DrinkListView.ItemsSource = drinkItems;
    }

    private List<MenuItem> GetDrinkItemsFromDatabase()
    {
        // Connect to your database and retrieve drink items
        using (var db = new AppDbContext(_dbPath))
        {
            return db.MenuItems.Where(item => item.ItemType == "Drink").ToList();
        }
    }
    private void AddToCart_Clicked(object sender, EventArgs e)
    {
        var menuItem = (MenuItem)((Button)sender).CommandParameter;

        // Get the table number from the entry field
        if (!int.TryParse(TableNumberEntry.Text, out int tableNumber))
        {
            DisplayAlert("Error", "Please enter a valid table number.", "OK");
            return;
        }

        using (var db = new AppDbContext(_orderDbPath))
        {
            var existingCartItem = db.OrderCartItems.FirstOrDefault(item => item.Name == menuItem.Name && item.TableNumber == tableNumber);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++; // Increment quantity if item already exists in the cart
            }
            else
            {
                db.OrderCartItems.Add(new OrderCartItem { Name = menuItem.Name, Price = menuItem.Price, ItemType = menuItem.ItemType, Quantity = 1, TableNumber = tableNumber });
            }

            db.SaveChanges();
        }

        DisplayAlert("Success", $"{menuItem.Name} added to cart for table {tableNumber}!", "OK");
    }


    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}