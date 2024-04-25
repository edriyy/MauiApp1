namespace MauiApp1;

public partial class KitchenDisplayPage : ContentPage
{
    private readonly string _orderDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "order_cart4.db");
    private List<OrderCartItem> _originalOrderCartItems;
    public KitchenDisplayPage()
	{
		InitializeComponent();
        LoadOrderCartItems();
    }
    private void LoadOrderCartItems()
    {
        _originalOrderCartItems = GetOrderCartItemsFromDatabase();
        OrderCartListView.ItemsSource = _originalOrderCartItems;
    }

    private List<OrderCartItem> GetOrderCartItemsFromDatabase()
    {
        using (var db = new AppDbContext(_orderDbPath))
        {
            return db.OrderCartItems.ToList();
        }
    }
    private void UndoButton_Clicked(object sender, EventArgs e)
    {
        // Reset the items to the original list
        OrderCartListView.ItemsSource = null; // Clear the ItemsSource
        OrderCartListView.ItemsSource = _originalOrderCartItems; // Reset the ItemsSource
        LoadOrderCartItems();
    }

    private void DoneButton_Clicked(object sender, EventArgs e)
    {
        var orderCartItem = (OrderCartItem)((Button)sender).CommandParameter;

        // Remove the item from the display
        if (OrderCartListView.ItemsSource is List<OrderCartItem> orderCartItems)
        {
            orderCartItems.Remove(orderCartItem);
            OrderCartListView.ItemsSource = null; // Clear the ItemsSource
            OrderCartListView.ItemsSource = orderCartItems; // Reset the ItemsSource
        }
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}