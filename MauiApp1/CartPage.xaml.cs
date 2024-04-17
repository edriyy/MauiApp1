using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class CartPage : ContentPage
    {
        private List<MenuItem> _cartItems;

        public CartPage(List<MenuItem> cartItems)
        {
            InitializeComponent();
            _cartItems = cartItems;
            CartListView.ItemsSource = _cartItems;
        }
        private void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}