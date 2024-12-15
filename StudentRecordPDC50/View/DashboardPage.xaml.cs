namespace StudentRecordPDC50.View;

public partial class DashboardPage : ContentPage
{
	public DashboardPage()
	{
		InitializeComponent();
	}
    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        // Navigate to the main page (replace MainPage with your actual main page name)
        await Navigation.PushAsync(new UserPage());
    }

    private async void OnAddHistoryClicked(object sender, EventArgs e)
    {
        // Navigate to the main page (replace MainPage with your actual main page name)
        await Navigation.PushAsync(new AcademicHistoryPage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Navigate to the main page (replace MainPage with your actual main page name)
        await Navigation.PushAsync(new MainPage());
    }
    private async void OnStudentListClicked(object sender, EventArgs e)
    {
        // Navigate to the main page (replace MainPage with your actual main page name)
        await Navigation.PushAsync(new StudentListPage());
    }
}