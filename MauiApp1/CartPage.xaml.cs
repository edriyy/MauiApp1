using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class CartPage : ContentPage
    {
        private readonly string _orderDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "order_cart3.db");

        public CartPage()
        {
            InitializeComponent();
            
        }

        private void LoadOrder_Clicked(object sender, EventArgs e)
        {
            // Get the table number from the entry field
            if (!int.TryParse(TableNumberEntry.Text, out int tableNumber))
            {
                DisplayAlert("Error", "Please enter a valid table number.", "OK");
                return;
            }

            LoadOrderCartItems(tableNumber);
        }

        private void LoadOrderCartItems(int tableNumber)
        {
            using (var db = new AppDbContext(_orderDbPath))
            {
                var orderCartItems = db.OrderCartItems.Where(item => item.TableNumber == tableNumber).ToList();
                OrderCartListView.ItemsSource = orderCartItems;

                // Calculate and display total price
                decimal totalPrice = orderCartItems.Sum(item => item.Price * item.Quantity);
                TotalPriceLabel.Text = $"Total Price: RM{totalPrice:N2}";
            }
        }
        private void IncreaseQuantity_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var item = (OrderCartItem)button.BindingContext;

            item.Quantity++; // Increment the quantity
            UpdateQuantity(item); // Update the quantity in the database
        }

        private void DecreaseQuantity_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var item = (OrderCartItem)button.BindingContext;

            if (item.Quantity > 0)
            {
                item.Quantity--; // Decrement the quantity
                UpdateQuantity(item); // Update the quantity in the database
            }
        }

        private void UpdateQuantity(OrderCartItem item)
        {
            using (var db = new AppDbContext(_orderDbPath))
            {
                var existingCartItem = db.OrderCartItems.FirstOrDefault(i => i.Id == item.Id);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity = item.Quantity;
                    db.SaveChanges();
                }
            }

            // Get the table number from the entry field
            if (!int.TryParse(TableNumberEntry.Text, out int tableNumber))
            {
                DisplayAlert("Error", "Please enter a valid table number.", "OK");
                return;
            }

            LoadOrderCartItems(tableNumber);
        }




        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}