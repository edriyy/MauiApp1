namespace MauiApp1;

public partial class CreateAccountPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db");
    public CreateAccountPage()
	{
		InitializeComponent();
	}
    private void OnCreateAccountClicked(object sender, EventArgs e)
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
            // Check if the username already exists
            if (db.Users.Any(u => u.Username == username))
            {
                DisplayAlert("Error", "Username already exists. Please choose a different username.", "OK");
                return;
            }

            // Add new user
            db.Users.Add(new UserAccount { Username = username, Password = password });
            db.SaveChanges();
        }

        DisplayAlert("Success", "Account created successfully!", "OK");
    }

}