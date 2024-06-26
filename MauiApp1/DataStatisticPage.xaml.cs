using System.Globalization;

namespace MauiApp1;

public partial class DataStatisticPage : ContentPage
{
    private readonly string _receiptDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "receipt.db");
    public DataStatisticPage()
    {
        InitializeComponent();
    }
    private void GetStatisticsClicked(object sender, EventArgs e)
    {
        string itemName = ItemNameEntry.Text.Trim();
        if (!string.IsNullOrEmpty(itemName))
        {
            using (var db = new AppDbContext(_receiptDbPath))
            {
                var receipts = db.Receipts.Where(r => r.Name.ToLower() == itemName.ToLower()).ToList();

                if (receipts.Any())
                {
                    ItemNameLabel.Text = $"Item Name: {receipts.First().Name}";

                    var totalSales = receipts.Sum(r => r.TotalPrice);
                    var averagePrice = receipts.Average(r => r.Price);
                    var totalQuantity = receipts.Sum(r => r.Quantity);

                    // Set the culture to Malaysian Ringgit (RM)
                    CultureInfo malaysianCulture = new CultureInfo("ms-MY");

                    TotalSalesLabel.Text = $"Total Sales: {totalSales.ToString("C", malaysianCulture)}";
                    AveragePriceLabel.Text = $"Average Price: {averagePrice.ToString("C", malaysianCulture)}";
                    TotalQuantityLabel.Text = $"Total Quantity: {totalQuantity}";
                    StatisticsLayout.IsVisible = true;
                }
                else
                {
                    ItemNameLabel.Text = "Item Name: Not Found";
                    StatisticsLayout.IsVisible = false;
                    DisplayAlert("No Data", "No data found for the specified item.", "OK");
                }
            }
        }
        else
        {
            ItemNameLabel.Text = "Item Name: Please Enter a Name";
            StatisticsLayout.IsVisible = false;
            DisplayAlert("Invalid Input", "Please enter an item name.", "OK");
        }
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}