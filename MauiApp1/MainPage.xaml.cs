namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private async void AdminButton_Clicked(object sender, EventArgs e)
        {
            loadingFrame.IsVisible = true;
            loadingIndicator.IsRunning = true;

            await Task.Delay(1000);

            Navigation.PushAsync(new LoginPage());

            loadingFrame.IsVisible = false;
            loadingIndicator.IsRunning = false;
        }

        private async void EmployeeButton_Clicked(object sender, EventArgs e)
        {
            loadingFrame.IsVisible = true;
            loadingIndicator.IsRunning = true;

            await Task.Delay(1000);

            await Navigation.PushAsync(new HomePage(true));

            loadingFrame.IsVisible = false;
            loadingIndicator.IsRunning = false;
        }
    }

}
