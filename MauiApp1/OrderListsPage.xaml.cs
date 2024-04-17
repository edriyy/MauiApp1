

namespace MauiApp1;

public partial class OrderListsPage : ContentPage
{
    
    public OrderListsPage()
	{
		InitializeComponent();
        
    }
    

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}