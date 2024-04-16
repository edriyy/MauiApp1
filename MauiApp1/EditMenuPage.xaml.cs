namespace MauiApp1;

public partial class EditMenuPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.db");
    public EditMenuPage()
	{
		InitializeComponent();
        
    }
    private void SearchButton_Clicked(object sender, EventArgs e)
    {
        string searchTerm = SearchEntry.Text;

        using (var db = new AppDbContext(_dbPath))
        {
            var menuItem = db.MenuItems.FirstOrDefault(item => item.Name == searchTerm);

            if (menuItem != null)
            {
                EditItemLayout.IsVisible = true;
                ItemNameLabel.Text = menuItem.Name;
                ItemPriceLabel.Text = menuItem.Price.ToString();
                ItemTypeLabel.Text = menuItem.ItemType;
            }
            else
            {
                DisplayAlert("Error", "Item not found.", "OK");
                EditItemLayout.IsVisible = false;
            }
        }
    }
    private void SaveChangesButton_Clicked(object sender, EventArgs e)
    {
        string newName = NewNameEntry.Text;
        string newPriceText = NewPriceEntry.Text;

        if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newPriceText))
        {
            DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        if (!decimal.TryParse(newPriceText, out decimal newPrice))
        {
            DisplayAlert("Error", "Invalid price format.", "OK");
            return;
        }

        using (var db = new AppDbContext(_dbPath))
        {
            var menuItem = db.MenuItems.FirstOrDefault(item => item.Name == ItemNameLabel.Text);

            if (menuItem != null)
            {
                menuItem.Name = newName;
                menuItem.Price = newPrice;
                db.SaveChanges();
                DisplayAlert("Success", "Item updated successfully!", "OK");
                // Clear fields and hide layout
                NewNameEntry.Text = string.Empty;
                NewPriceEntry.Text = string.Empty;
                EditItemLayout.IsVisible = false;
            }
            else
            {
                DisplayAlert("Error", "Item not found.", "OK");
            }
        }
    }
    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        // Clear fields and hide layout
        NewNameEntry.Text = string.Empty;
        NewPriceEntry.Text = string.Empty;
        EditItemLayout.IsVisible = false;
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}