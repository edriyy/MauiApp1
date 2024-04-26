namespace MauiApp1;

public partial class DataHistoryPage : ContentPage
{
	public DataHistoryPage()
	{
		InitializeComponent();
	}
    private void TransactionHistoryButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TransactionHistoryPage());
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}