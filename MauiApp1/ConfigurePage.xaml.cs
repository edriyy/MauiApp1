using Microsoft.Maui.Controls;
namespace MauiApp1;
public partial class ConfigurePage : ContentPage
{
    public ConfigurePage()
    {
        InitializeComponent();
        this.BindingContext = this;
    }

    public bool IsDarkMode { get; set; }

    private void OnDarkModeToggle(object sender, ToggledEventArgs e)
    {
        App.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}