using StudentRecordPDC50.Model;
using StudentRecordPDC50.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StudentRecordPDC50.ViewModel
{
    public class AcademicHistoryViewModel : BindableObject
    {
        private readonly UserService _userService;

        public ObservableCollection<AcademicHistory> AcademicHistories { get; set; }
        public ObservableCollection<AcademicHistory> FilteredAcademicHistories { get; set; }
        public ObservableCollection<User> Students { get; set; }
        public ObservableCollection<string> Courses { get; set; }
        public ObservableCollection<string> SchoolYears { get; set; }

        private User _selectedStudent;
        public User SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                if (_selectedStudent != null)
                {
                    SelectedStudentID = _selectedStudent.Idno;
                    LoadAcademicHistory();
                }
            }
        }

        private string _selectedStudentID;
        public string SelectedStudentID
        {
            get => _selectedStudentID;
            set
            {
                _selectedStudentID = value;
                OnPropertyChanged();
            }
        }

        private string _collegeInput;
        public string CollegeInput
        {
            get => _collegeInput;
            set
            {
                _collegeInput = value;
                OnPropertyChanged();
            }
        }

        private string _courseInput;
        public string CourseInput
        {
            get => _courseInput;
            set
            {
                _courseInput = value;
                OnPropertyChanged();
            }
        }


        private string _yearlevelInput;
        public string YearlevelInput
        {
            get => _yearlevelInput;
            set
            {
                _yearlevelInput = value;
                OnPropertyChanged();
            }
        }

        private string _statusInput;
        public string StatusInput
        {
            get => _statusInput;
            set
            {
                _statusInput = value;
                OnPropertyChanged();
            }
        }

        private string _semesterInput;
        public string SemesterInput
        {
            get => _semesterInput;
            set
            {
                _semesterInput = value;
                OnPropertyChanged();
            }
        }

        private string _schoolYearInput;
        public string SchoolYearInput
        {
            get => _schoolYearInput;
            set
            {
                _schoolYearInput = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCourse;
        public string SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
                FilterAcademicHistories(); // Trigger filtering
            }
        }

        private string _selectedSchoolYear;
        public string SelectedSchoolYear
        {
            get => _selectedSchoolYear;
            set
            {
                _selectedSchoolYear = value;
                OnPropertyChanged();
                FilterAcademicHistories(); // Trigger filtering
            }
        }

        private string _noRecordsMessage;
        public string NoRecordsMessage
        {
            get => _noRecordsMessage;
            set
            {
                _noRecordsMessage = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddAcademicHistoryCommand { get; }

        public AcademicHistoryViewModel()
        {
            _userService = new UserService();
            AcademicHistories = new ObservableCollection<AcademicHistory>();
            FilteredAcademicHistories = new ObservableCollection<AcademicHistory>();
            Students = new ObservableCollection<User>();
            Courses = new ObservableCollection<string> { "BSCS", "BSIT", "BMMA" }; // Example courses
            SchoolYears = new ObservableCollection<string> { "2020-2021", "2021-2022", "2022-2023", "2023-2024", "2024-2025", "2025-2026" }; // Example school years
            AddAcademicHistoryCommand = new Command(async () => await AddAcademicHistory());
            Task task = LoadStudents();
            LoadStudentCommand = new Command(async () => await LoadStudents());
        }
        public ICommand LoadStudentCommand { get; }
        private async Task LoadStudents()
        {
            var users = await _userService.GetUsersAsync();
            foreach (var user in users)
            {
                Students.Add(user);
            }
        }

        public async Task LoadAcademicHistory()
        {
            if (string.IsNullOrEmpty(SelectedStudentID)) return;

            // Fetch academic history for the selected student
            var histories = await _userService.GetAcademicHistoryAsync(SelectedStudentID);
            AcademicHistories.Clear();

            // If no records found, set a message for no records
            if (histories == null || histories.Count == 0)
            {
                NoRecordsMessage = "No academic record"; // Set the message
            }
            else
            {
                foreach (var history in histories)
                {
                    AcademicHistories.Add(history); // Add each history record
                }
            }

            // Apply the filter after loading the history
            FilterAcademicHistories();
        }


        private void FilterAcademicHistories()
        {
            var filteredList = AcademicHistories.Where(h =>
                (string.IsNullOrEmpty(SelectedCourse) || h.Course == SelectedCourse) &&
                (string.IsNullOrEmpty(SelectedSchoolYear) || h.SchoolYear == SelectedSchoolYear)).ToList();

            FilteredAcademicHistories.Clear();
            foreach (var history in filteredList)
            {
                FilteredAcademicHistories.Add(history);
            }

            // Update the message if no records are found after filtering
            NoRecordsMessage = FilteredAcademicHistories.Count == 0 ? "No records found for the selected filters." : string.Empty;
        }


        private async Task AddAcademicHistory()
        {
            if (!string.IsNullOrWhiteSpace(CollegeInput) && !string.IsNullOrWhiteSpace(CourseInput) && !string.IsNullOrWhiteSpace(YearlevelInput) && !string.IsNullOrWhiteSpace(StatusInput)
                && !string.IsNullOrWhiteSpace(SemesterInput) && !string.IsNullOrWhiteSpace(SchoolYearInput))
            {
                var newHistory = new AcademicHistory
                {
                    StudentID = SelectedStudentID,
                    College = CollegeInput,
                    Course = CourseInput,
                    Yearlevel = YearlevelInput,
                    Status = StatusInput,
                    Semester = SemesterInput,
                    SchoolYear = SchoolYearInput
                };

                var result = await _userService.AddAcademicHistoryAsync(newHistory);

                if (result.Equals("Success", StringComparison.OrdinalIgnoreCase))
                {
                    // Refresh the academic histories list after adding
                    await LoadAcademicHistory();

                    // Clear the input fields after successful addition
                    ClearInputFields();
                }
            }
        }

        private void ClearInputFields()
        {
            CollegeInput = string.Empty;
            CourseInput = string.Empty;
            YearlevelInput = string.Empty;
            StatusInput = string.Empty;
            SemesterInput = string.Empty;
            SchoolYearInput = string.Empty;
        }
    }
}
