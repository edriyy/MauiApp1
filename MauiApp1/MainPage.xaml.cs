namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private void AdminButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void EmployeeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage(true));
        }
    }

}
