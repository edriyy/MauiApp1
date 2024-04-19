namespace MauiApp1;

public partial class HomePage : ContentPage
{
    private bool _isButton2Clicked = false;
    
    public HomePage()
	{
		InitializeComponent();
	}
    public HomePage(bool isButton2Clicked) : this()
    {
        _isButton2Clicked = isButton2Clicked;
        UpdateButtonsVisibility();
    }

    private void UpdateButtonsVisibility()
    {
        // Hide buttons 4, 5, 6, and 7 if Button 2 is clicked
        Button4.IsVisible = !_isButton2Clicked;
        Button5.IsVisible = !_isButton2Clicked;
        Button6.IsVisible = !_isButton2Clicked;
        Button7.IsVisible = !_isButton2Clicked;
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void ItemManagementButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ItemManagementPage());
    }

    private void FoodButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new FoodMenuPage());
    }

    private void DrinksButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new DrinkMenuPage());
    }

    private async void ViewCartButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CartPage());
    }
}