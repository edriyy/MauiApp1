using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class DrinkMenuPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.db");
    
    public DrinkMenuPage()
	{
		InitializeComponent();
        LoadDrinkItems();
    }
    private void LoadDrinkItems()
    {
        // Assuming you have a method to retrieve drink items from your database
        List<MenuItem> drinkItems = GetDrinkItemsFromDatabase();
        DrinkListView.ItemsSource = drinkItems;
    }

    private List<MenuItem> GetDrinkItemsFromDatabase()
    {
        // Connect to your database and retrieve drink items
        using (var db = new AppDbContext(_dbPath))
        {
            return db.MenuItems.Where(item => item.ItemType == "Drink").ToList();
        }
    }
    
    

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}