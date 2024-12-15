using StudentRecordPDC50.ViewModel;

namespace StudentRecordPDC50.View;

public partial class AcadHistoryStudentPage : ContentPage
{
    public AcadHistoryStudentPage(string studentId)
    {
        InitializeComponent();

        // Initialize and set the BindingContext
        var viewModel = new AcademicHistoryViewModel
        {
            SelectedStudentID = studentId // Pass the student ID to the ViewModel
        };

        BindingContext = viewModel;

        // Load the academic history data
        viewModel.LoadAcademicHistory();
    }
}