namespace MauiApp1;

public partial class CreateMenuPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.db");
    public CreateMenuPage()
	{
		InitializeComponent();
	}
    private void OnAddFoodClicked(object sender, EventArgs e)
    {
        string foodName = FoodNameEntry.Text;
        string foodPriceText = FoodPriceEntry.Text;

        if (string.IsNullOrWhiteSpace(foodName) || string.IsNullOrWhiteSpace(foodPriceText))
        {
            DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        if (!decimal.TryParse(foodPriceText, out decimal foodPrice))
        {
            DisplayAlert("Error", "Invalid price format.", "OK");
            return;
        }

        using (var db = new AppDbContext(_dbPath))
        {
            if (db.MenuItems.Any(item => item.Name == foodName && item.ItemType == "Food"))
            {
                DisplayAlert("Error", "Food already exists.", "OK");
                return;
            }

            db.MenuItems.Add(new MenuItem { Name = foodName, Price = foodPrice, ItemType = "Food" });
            db.SaveChanges();
        }

        DisplayAlert("Success", "Food item added successfully!", "OK");
        FoodNameEntry.Text = string.Empty;
        FoodPriceEntry.Text = string.Empty;
    }
    private void OnAddDrinkClicked(object sender, EventArgs e)
    {
        string drinkName = DrinkNameEntry.Text;
        string drinkPriceText = DrinkPriceEntry.Text;

        if (string.IsNullOrWhiteSpace(drinkName) || string.IsNullOrWhiteSpace(drinkPriceText))
        {
            DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        if (!decimal.TryParse(drinkPriceText, out decimal drinkPrice))
        {
            DisplayAlert("Error", "Invalid price format.", "OK");
            return;
        }

        using (var db = new AppDbContext(_dbPath))
        {
            if (db.MenuItems.Any(item => item.Name == drinkName && item.ItemType == "Drink"))
            {
                DisplayAlert("Error", "Drink already exists.", "OK");
                return;
            }

            db.MenuItems.Add(new MenuItem { Name = drinkName, Price = drinkPrice, ItemType = "Drink" });
            db.SaveChanges();
        }

        DisplayAlert("Success", "Drink item added successfully!", "OK");
        DrinkNameEntry.Text = string.Empty;
        DrinkPriceEntry.Text = string.Empty;
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}