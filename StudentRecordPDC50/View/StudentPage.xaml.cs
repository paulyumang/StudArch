using StudentRecordPDC50.ViewModel;
using StudentRecordPDC50.Services;
using StudentRecordPDC50.Model;

namespace StudentRecordPDC50.View
{
    public partial class StudentPage : ContentPage
    {
        private readonly UserService _userService;
        private readonly AttendanceViewModel _attendanceViewModel;
        private readonly AcademicHistoryViewModel _academicHistoryViewModel;

        public string Studentidno { get; set; }

        public StudentPage()
        {
            InitializeComponent();
            _userService = new UserService();

            // Initialize the ViewModels
            _attendanceViewModel = new AttendanceViewModel();
            _academicHistoryViewModel = new AcademicHistoryViewModel();

            // Bind ViewModels to ListViews
            AttendanceListView.BindingContext = _attendanceViewModel; // Bind to AttendanceViewModel
            //AcademicHistoryListView.BindingContext = _academicHistoryViewModel; // Bind to AcademicHistoryViewModel
        }

        public void SetStudentData(string idno, string name, string gender, string contact)
        {
            // Set Student ID and Bind the Student Data
            BindingContext = new StudentViewModel
            {
                Idno = idno,
                Name = name,
                Gender = gender,
                Contactno = contact
            };

            // Set the Student ID in the Attendance and Academic History ViewModels
            Studentidno = idno;
            _attendanceViewModel.Studentidno = idno;
            _academicHistoryViewModel.SelectedStudentID = idno;

            // Load the data (attendance and academic history)
            _attendanceViewModel.LoadAttendanceCommand.Execute(null);
            _academicHistoryViewModel.LoadAcademicHistory(); // Fetch academic history
        }

        private async void OnInButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Studentidno))
            {
                await DisplayAlert("Error", "Student ID not found. Please log in again.", "OK");
                return;
            }

            var response = await _userService.RecordEntryAsync(Studentidno);
            if (response != null)
            {
                await DisplayAlert("Attendance", response.Message ?? "Success", "OK");
                _attendanceViewModel.LoadAttendanceCommand.Execute(null); // Refresh attendance
            }
            else
            {
                await DisplayAlert("Attendance", "Failed to connect to server", "OK");
            }
        }

        private async void OnOutButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Studentidno))
            {
                await DisplayAlert("Error", "Student ID not found. Please log in again.", "OK");
                return;
            }

            var response = await _userService.RecordExitAsync(Studentidno);
            if (response != null)
            {
                await DisplayAlert("Attendance", response.Message ?? "Success", "OK");
                _attendanceViewModel.LoadAttendanceCommand.Execute(null); // Refresh attendance
            }
            else
            {
                await DisplayAlert("Attendance", "Failed to connect to server", "OK");
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private async void OnAcademicHistoryClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Studentidno))
            {
                await DisplayAlert("Error", "Student ID not found. Please log in again.", "OK");
                return;
            }

            // Pass the Studentidno to the constructor of AcadHistoryStudentPage
            await Navigation.PushAsync(new AcadHistoryStudentPage(Studentidno));
        }


    }
}