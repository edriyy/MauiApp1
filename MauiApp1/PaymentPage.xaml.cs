using System.Text;

namespace MauiApp1;

public partial class PaymentPage : ContentPage
{
    private readonly string _orderDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "order_cart4.db");
    private readonly string _receiptDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "receipt.db");
    private List<OrderCartItem> selectedItems = new List<OrderCartItem>();
    public PaymentPage()
    {
        InitializeComponent();
    }
    private async void LoadOrder_Clicked(object sender, EventArgs e)
    {

        // Get the table number from the entry field
        if (!int.TryParse(TableNumberEntry.Text, out int tableNumber))
        {
            DisplayAlert("Error", "Please enter a valid table number.", "OK");
            return;
        }

        loadingFrame.IsVisible = true;
        loadingIndicator.IsRunning = true;

        await Task.Delay(2000);

        LoadOrderCartItems(tableNumber);

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }
    private async void LoadOrderCartItems(int tableNumber)
    {

        using (var db = new AppDbContext(_orderDbPath))
        {
            var orderCartItems = db.OrderCartItems.Where(item => item.TableNumber == tableNumber).ToList();
            List<OrderCartItem> expandedOrderCartItems = ExpandAndNormalizeOrderCartItems(orderCartItems);
            OrderCartListView.ItemsSource = expandedOrderCartItems;

            // Calculate and display total price
            decimal totalPrice = orderCartItems.Sum(item => item.Price * item.Quantity);
            TotalPriceLabel.Text = $"Total Price: RM{totalPrice:N2}";
        }

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }


    private List<OrderCartItem> ExpandAndNormalizeOrderCartItems(List<OrderCartItem> orderCartItems)
    {
        List<OrderCartItem> expandedAndNormalizedList = new List<OrderCartItem>();
        foreach (var item in orderCartItems)
        {
            for (int i = 0; i < item.Quantity; i++)
            {
                // Create a new instance of the item with quantity set to one
                OrderCartItem newItem = new OrderCartItem
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    TableNumber = item.TableNumber,
                    Quantity = 1 // Set quantity to one
                };
                expandedAndNormalizedList.Add(newItem);
            }
        }
        return expandedAndNormalizedList;
    }
    private void CalculateChangeButton_Clicked(object sender, EventArgs e)
    {
        if (!decimal.TryParse(AmountPaidEntry.Text, out decimal amountPaid))
        {
            DisplayAlert("Error", "Please enter a valid amount paid.", "OK");
            return;
        }

        decimal totalPrice = selectedItems.Sum(item => item.Price * item.Quantity);
        decimal change = amountPaid - totalPrice;

        ChangeLabel.Text = $"Change: RM{change:N2}";
    }
    private void ItemCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = sender as CheckBox;
        var item = checkbox?.BindingContext as OrderCartItem; // Assuming OrderCartItem is the type of items in your list
        if (checkbox.IsChecked && !selectedItems.Contains(item))
        {
            selectedItems.Add(item);
        }
        else if (!checkbox.IsChecked && selectedItems.Contains(item))
        {
            selectedItems.Remove(item);
        }
    }
    private void SelectAllCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        bool isChecked = SelectAllCheckBox.IsChecked;

        // Iterate through each item in the ListView
        foreach (var item in OrderCartListView.ItemsSource)
        {
            // Find the checkbox for each item in the ListView
            var viewCell = OrderCartListView.TemplatedItems.First(x => x.BindingContext == item);
            var checkbox = viewCell.FindByName<CheckBox>("ItemCheckBox");

            // Update the state of the checkbox
            checkbox.IsChecked = isChecked;

            // Update the selectedItems list accordingly
            var orderItem = item as OrderCartItem;
            if (isChecked && !selectedItems.Contains(orderItem))
            {
                selectedItems.Add(orderItem);
            }
            else if (!isChecked && selectedItems.Contains(orderItem))
            {
                selectedItems.Remove(orderItem);
            }
        }
    }

    private void CalculateTotalPriceButton_Clicked(object sender, EventArgs e)
    {
        decimal totalPrice = selectedItems.Sum(item => item.Price * item.Quantity);
        CalculationResultLabel.Text = $"Total Price of Selected Items: RM{totalPrice:N2}";
    }

    /*private void RemoveCheckedItemsButton_Clicked(object sender, EventArgs e)
    {
        using (var db = new AppDbContext(_orderDbPath))
        {
            foreach (var item in selectedItems)
            {
                var orderCartItemToRemove = db.OrderCartItems.SingleOrDefault(i => i.Id == item.Id);
                if (orderCartItemToRemove != null)
                {
                    if (orderCartItemToRemove.Quantity > 1)
                    {
                        // If quantity is greater than 1, decrement the quantity
                        orderCartItemToRemove.Quantity--;
                    }
                    else
                    {
                        // If quantity is 1, remove the item from the database
                        db.OrderCartItems.Remove(orderCartItemToRemove);
                    }
                }
            }
            db.SaveChanges();

            // Get the table number from the entry field
            if (!int.TryParse(TableNumberEntry.Text, out int tableNumber))
            {
                DisplayAlert("Error", "Please enter a valid table number.", "OK");
                return;
            }

            LoadOrderCartItems(tableNumber);
        }

        // Clear the selected items list and uncheck select all checkbox
        selectedItems.Clear();
        //SelectAllCheckBox.IsChecked = false;
    }*/
    private void RemoveCheckedItemsButton_Clicked(object sender, EventArgs e)
    {
        foreach (var item in selectedItems.ToList())
        {
            OrderCartListView.ItemsSource = OrderCartListView.ItemsSource.Cast<OrderCartItem>().Where(x => x != item).ToList();
            selectedItems.Remove(item);
        }
    }
    private void StoreReceiptInDatabase(int tableNumber, string itemName, decimal itemPrice, int quantity, decimal totalPrice, decimal amountPaid, decimal change)
    {
        using (var db = new AppDbContext(_receiptDbPath))
        {
            var receipt = new Receipt
            {
                TableNumber = tableNumber,
                Name = itemName,
                Price = itemPrice,
                Quantity = quantity,
                TotalPrice = totalPrice,
                AmountPaid = amountPaid,
                Change = change
            };

            db.Receipts.Add(receipt);
            db.SaveChanges();

            DisplayAlert("Receipt", "Receipt saved successfully.", "OK");
        }
    }
    private void PrintReceiptButton_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(TableNumberEntry.Text, out int tableNumber))
        {
            DisplayAlert("Error", "Please enter a valid table number.", "OK");
            return;
        }

        if (!decimal.TryParse(AmountPaidEntry.Text, out decimal amountPaid))
        {
            DisplayAlert("Error", "Please enter a valid amount paid.", "OK");
            return;
        }

        string receiptText = GenerateReceiptForTable(tableNumber, amountPaid);

        if (string.IsNullOrEmpty(receiptText))
        {
            DisplayAlert("Receipt", "No items found for the specified table number.", "OK");
        }
        else
        {
            ShareReceipt(receiptText);
        }
    }

    private async void ShareReceipt(string receiptText)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Text = receiptText,
            Title = "Share Receipt"
        });
    }


    private string GenerateReceiptForTable(int tableNumber, decimal amountPaid)
    {
        StringBuilder receiptBuilder = new StringBuilder();
        decimal totalPrice = 0;

        using (var db = new AppDbContext(_orderDbPath))
        {
            var orderCartItems = db.OrderCartItems.Where(item => item.TableNumber == tableNumber).ToList();

            if (orderCartItems.Count == 0)
            {
                return string.Empty; // No items found for the specified table number
            }

            receiptBuilder.AppendLine($"Receipt for Table {tableNumber}:");
            receiptBuilder.AppendLine();

            foreach (var item in orderCartItems)
            {
                receiptBuilder.AppendLine($"{item.Name} - Price: {item.Price:C} - Quantity: {item.Quantity}");
                totalPrice += item.Price * item.Quantity;

                // Store receipt entry for each item
                StoreReceiptInDatabase(tableNumber, item.Name, item.Price, item.Quantity, item.Price * item.Quantity, amountPaid, amountPaid - totalPrice);
            }

            receiptBuilder.AppendLine();
            receiptBuilder.AppendLine($"Total Price: {totalPrice:C}");
            receiptBuilder.AppendLine($"Amount Paid: {amountPaid:C}");

            // Calculate change
            decimal change = amountPaid - totalPrice;
            receiptBuilder.AppendLine($"Change: {change:C}");
        }

        return receiptBuilder.ToString();
    }



    private void CompleteButton_Clicked(object sender, EventArgs e)
    {
        // Get the table number from the entry field
        if (!int.TryParse(TableNumberEntry.Text, out int tableNumber))
        {
            DisplayAlert("Error", "Please enter a valid table number.", "OK");
            return;
        }

        RemoveOrderCartItems(tableNumber);
    }

    private void RemoveOrderCartItems(int tableNumber)
    {
        using (var db = new AppDbContext(_orderDbPath))
        {
            var orderCartItems = db.OrderCartItems.Where(item => item.TableNumber == tableNumber).ToList();
            db.OrderCartItems.RemoveRange(orderCartItems); // Remove all items for the given table number
            db.SaveChanges();

            // Clear the list view and total price label
            OrderCartListView.ItemsSource = null;
            TotalPriceLabel.Text = "Total Price: ";

            DisplayAlert("Success", "Order cart items removed successfully.", "OK");
        }
    }


    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}