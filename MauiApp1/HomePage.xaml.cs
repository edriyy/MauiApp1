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

    private async void ItemManagementButton_Clicked(object sender, EventArgs e)
    {
        loadingFrame.IsVisible = true;
        loadingIndicator.IsRunning = true;

        await Task.Delay(1000);

        await Navigation.PushAsync(new ItemManagementPage());

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }

    private async void FoodButton_Clicked(object sender, EventArgs e)
    {
        loadingFrame.IsVisible = true;
        loadingIndicator.IsRunning = true;

        await Task.Delay(1000);

        await Navigation.PushAsync(new FoodMenuPage());

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }

    private async void DrinksButton_Clicked(object sender, EventArgs e)
    {
        loadingFrame.IsVisible = true;
        loadingIndicator.IsRunning = true;

        await Task.Delay(1000);

        await Navigation.PushAsync(new DrinkMenuPage());

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }

    private async void ViewCartButton_Clicked(object sender, EventArgs e)
    {
        loadingFrame.IsVisible = true;
        loadingIndicator.IsRunning = true;

        await Task.Delay(1000);

        await Navigation.PushAsync(new CartPage());

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }
    private async void KitchenDisplayButton_Clicked(object sender, EventArgs e)
    {
        loadingFrame.IsVisible = true;
        loadingIndicator.IsRunning = true;

        await Task.Delay(1000);

        await Navigation.PushAsync(new KitchenDisplayPage());

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }

    private async void ConfigureButton_Clicked(object sender, EventArgs e)
    {
        loadingFrame.IsVisible = true;
        loadingIndicator.IsRunning = true;

        await Task.Delay(1000);

        await Navigation.PushAsync(new ConfigurePage());

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }
    private async void PaymentButton_Clicked(object sender, EventArgs e)
    {
        loadingFrame.IsVisible = true;
        loadingIndicator.IsRunning = true;

        await Task.Delay(1000);

        await Navigation.PushAsync(new PaymentPage());

        loadingFrame.IsVisible = false;
        loadingIndicator.IsRunning = false;
    }
}