using StudentRecordPDC50.ViewModel;
using StudentRecordPDC50.Model;


namespace StudentRecordPDC50.View;

public partial class AcademicHistoryPage : ContentPage
{
	public AcademicHistoryPage()
	{
		InitializeComponent();
        BindingContext = new AcademicHistoryViewModel();
    }
    // This method will be triggered when an item in the ListView is tapped
    private void OnAcademicHistoryTapped(object sender, ItemTappedEventArgs e)
    {
        // Get the tapped item from the ListView
        var tappedItem = e.Item as AcademicHistory;

        if (tappedItem != null)
        {
            // Fill the Entry fields with the tapped item's data
            var viewModel = BindingContext as AcademicHistoryViewModel;

            if (viewModel != null)
            {
                // Set the data from the tapped item to the Entry fields
                viewModel.CollegeInput = tappedItem.College;
                viewModel.CourseInput = tappedItem.Course;
                viewModel.YearlevelInput = tappedItem.Yearlevel;
                viewModel.StatusInput = tappedItem.Status;
                viewModel.SemesterInput = tappedItem.Semester;
                viewModel.SchoolYearInput = tappedItem.SchoolYear;
            }
        }
    }
}	