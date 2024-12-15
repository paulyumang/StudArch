using StudentRecordPDC50.ViewModel;
namespace StudentRecordPDC50.View;


public partial class UserPage : ContentPage
{
	public UserPage()
	{
		InitializeComponent();
        BindingContext = new UserViewModel();
    }
  
}