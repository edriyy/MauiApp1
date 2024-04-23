namespace MauiApp1;

public partial class PaymentPage : ContentPage
{
    private List<OrderCartItem> _orderCartItems;
    private decimal _totalPrice;
    public PaymentPage(List<OrderCartItem> orderCartItems, decimal totalPrice)
	{
		InitializeComponent();
        _orderCartItems = orderCartItems;
        _totalPrice = totalPrice;

        // Display order cart items and total price
        OrderCartListView.ItemsSource = orderCartItems;
        TotalPriceLabel.Text = $"Total Price: {totalPrice:C}";
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    private void AmountEntry_Completed(object sender, EventArgs e)
    {
        // Calculate change when the amount is entered
        CalculateChange();
    }

    private void CalculateChange()
    {
        if (!string.IsNullOrEmpty(AmountEntry.Text))
        {
            // Parse amount entered
            if (decimal.TryParse(AmountEntry.Text, out decimal amountPaid))
            {
                // Calculate change
                decimal change = amountPaid - _totalPrice;

                // Display change
                ChangeLabel.Text = $"Change: {change:C}";
            }
            else
            {
                // Invalid amount entered
                ChangeLabel.Text = "Invalid amount entered";
            }
        }
    }
}