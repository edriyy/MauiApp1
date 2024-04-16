using Microsoft.Maui.Controls;

namespace MauiApp1;

public partial class ItemManagementPage : ContentPage
{
	public ItemManagementPage()
	{
		InitializeComponent();
	}
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void AddItemButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateMenuPage());
    }

    private void EditItemButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EditMenuPage());
    }

    private void ItemListsButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ItemListsPage());
    }
}