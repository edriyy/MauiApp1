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
        
        List<MenuItem> foodItems = GetFoodItemsFromDatabase();
        FoodListView.ItemsSource = foodItems;
    }

    private List<MenuItem> GetFoodItemsFromDatabase()
    {
        
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