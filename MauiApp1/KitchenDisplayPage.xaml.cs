namespace MauiApp1;

public partial class KitchenDisplayPage : ContentPage
{
    private readonly string _orderDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "order_cart4.db");
    private List<OrderCartItem> _originalOrderCartItems;

    public KitchenDisplayPage()
    {
        InitializeComponent();
        LoadOrderCartItems();
        StartAutoRefresh();
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

    private async void StartAutoRefresh()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromSeconds(30)); // Refresh interval (adjust as needed)
            RefreshOrderCartItems();
        }
    }

    private void RefreshOrderCartItems()
    {
        _originalOrderCartItems = GetOrderCartItemsFromDatabase();
        OrderCartListView.ItemsSource = null;
        OrderCartListView.ItemsSource = _originalOrderCartItems;
    }

    private void UndoButton_Clicked(object sender, EventArgs e)
    {
        LoadOrderCartItems();
    }

    private void DoneButton_Clicked(object sender, EventArgs e)
    {
        var orderCartItem = (OrderCartItem)((Button)sender).CommandParameter;

        // Perform the "Done" action here
        // For example, you can mark the order as completed in the database

        // Now remove the item from the display
        if (_originalOrderCartItems.Contains(orderCartItem))
        {
            _originalOrderCartItems.Remove(orderCartItem);
            OrderCartListView.ItemsSource = null;
            OrderCartListView.ItemsSource = _originalOrderCartItems;
        }
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}