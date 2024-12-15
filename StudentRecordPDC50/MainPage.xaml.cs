using StudentRecordPDC50.Services; // Make sure to import the necessary namespace
using System.Net.Http.Json;
using StudentRecordPDC50.Model;
using StudentRecordPDC50.View;

namespace StudentRecordPDC50
{
    public partial class MainPage : ContentPage
    {
        private readonly UserService _userService; // Declare the _userService field

        public MainPage()
        {
            InitializeComponent();
            _userService = new UserService(); // Initialize the _userService
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Input Error", "Please enter both username and password.", "OK");
                return;
            }

            const string adminUsername = "admin";
            const string adminPassword = "password123";

            if (username == adminUsername && password == adminPassword)
            {
                await Shell.Current.GoToAsync("//DashboardPage");
                return;
            }

            try
            {
                var loginResponse = await _userService.LoginAsync(username, password);

                if (loginResponse?.Message == "Login successful")
                {
                    // Get the logged-in student's data
                    var user = loginResponse.User;

                    // Redirect to the StudentPage and pass the student data
                    var studentPage = new StudentPage();

                    // Pass the student details dynamically
                    studentPage.SetStudentData(user.Idno, user.Name, user.Gender, user.Contactno);

                    await Navigation.PushAsync(studentPage);
                }
                else
                {
                    await DisplayAlert("Login Failed", loginResponse?.Message ?? "Unknown error", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}