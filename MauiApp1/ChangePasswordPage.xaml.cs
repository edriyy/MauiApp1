//using Windows.System;

namespace MauiApp1;

public partial class ChangePasswordPage : ContentPage
{
    private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "users.db");
    public ChangePasswordPage()
	{
		InitializeComponent();
    }
    private void OnChangePasswordClicked(object sender, EventArgs e)
    {
        string oldPassword = OldPasswordEntry.Text;
        string newPassword = NewPasswordEntry.Text;
        string confirmPassword = ConfirmPasswordEntry.Text;

        // Check if old password, new password, and confirm password are not empty
        if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        // Check if new password matches confirm password
        if (newPassword != confirmPassword)
        {
            DisplayAlert("Error", "New password and confirm password do not match.", "OK");
            return;
        }

        // Check if old password is correct
        using (var db = new AppDbContext(_dbPath))
        {
            var user = db.Users.FirstOrDefault(u => u.Password == oldPassword);
            if (user == null)
            {
                DisplayAlert("Error", "Incorrect old password.", "OK");
                return;
            }

            // Update password
            user.Password = newPassword;
            db.SaveChanges();
        }

        DisplayAlert("Success", "Password changed successfully!", "OK");
        Navigation.PopAsync(); // Navigate back to previous page
    }
    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}