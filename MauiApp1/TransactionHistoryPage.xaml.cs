namespace MauiApp1;

public partial class TransactionHistoryPage : ContentPage
{
    private readonly string _receiptDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "receipt.db");
    public TransactionHistoryPage()
    {
        InitializeComponent();
        LoadReceiptData();
    }
    private void LoadReceiptData()
    {
        using (var db = new AppDbContext(_receiptDbPath))
        {
            // Retrieve receipt data from the database
            var receipts = db.Receipts.ToList();

            // Set the retrieved data as the item source for the ListView
            ReceiptListView.ItemsSource = receipts;
        }
    }
    private void RemoveReceiptClicked(object sender, EventArgs e)
    {
        // Get the ID of the receipt to remove
        var button = sender as Button;
        if (button != null && button.CommandParameter != null && button.CommandParameter is int receiptId)
        {
            // Remove the receipt from the database
            using (var db = new AppDbContext(_receiptDbPath))
            {
                var receiptToRemove = db.Receipts.FirstOrDefault(r => r.Id == receiptId);
                if (receiptToRemove != null)
                {
                    db.Receipts.Remove(receiptToRemove);
                    db.SaveChanges();

                    // Reload the receipt data after removal
                    LoadReceiptData();
                }
            }
        }
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}