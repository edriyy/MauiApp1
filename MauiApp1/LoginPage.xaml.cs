namespace MauiApp1;

public partial class LoginPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db");
    public LoginPage()
	{
		InitializeComponent();
	}
    private void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        // Check if username and password are not empty
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        using (var db = new AppDbContext(_dbPath))
        {
            // Check if user with given username and password exists
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                DisplayAlert("Error", "Invalid username or password.", "OK");
                return;
            }

            // Successfully logged in
            //DisplayAlert("Success", "Logged in successfully!", "OK");
            // Navigate to next page or perform other actions as needed
            // Clear entry fields
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            Navigation.PushAsync(new HomePage());
        }
    }
    private async void ChangePasswordLabel_Tapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChangePasswordPage());
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}