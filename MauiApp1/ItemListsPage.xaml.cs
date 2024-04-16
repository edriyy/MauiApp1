using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class ItemListsPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "menu.db");
    private List<string> _categories = new List<string> { "Food", "Drink" };
    public ObservableCollection<MenuItem> MenuItems { get; set; }
    public ItemListsPage()
	{
		InitializeComponent();
        CategoryPicker.ItemsSource = _categories;
        MenuItems = new ObservableCollection<MenuItem>();
        ItemListView.ItemsSource = MenuItems;
    }
    private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedCategory = CategoryPicker.SelectedItem as string;

        if (selectedCategory != null)
        {
            using (var db = new AppDbContext(_dbPath))
            {
                var items = db.MenuItems.Where(item => item.ItemType == selectedCategory).ToList();
                MenuItems.Clear();
                foreach (var item in items)
                {
                    MenuItems.Add(item);
                }
            }
        }
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}