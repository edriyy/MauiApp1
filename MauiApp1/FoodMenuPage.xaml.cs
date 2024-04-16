using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class FoodMenuPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.db");
    
    public FoodMenuPage()
	{
		InitializeComponent();
        LoadFoodItems();
    }
    private void LoadFoodItems()
    {
        // Assuming you have a method to retrieve food items from your database
        List<MenuItem> foodItems = GetFoodItemsFromDatabase();
        FoodListView.ItemsSource = foodItems;
    }

    private List<MenuItem> GetFoodItemsFromDatabase()
    {
        // Connect to your database and retrieve food items
        using (var db = new AppDbContext(_dbPath))
        {
            return db.MenuItems.Where(item => item.ItemType == "Food").ToList();
        }
    }
    




    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}