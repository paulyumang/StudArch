using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using StudentRecordPDC50.Model;
using StudentRecordPDC50.Services;

namespace StudentRecordPDC50.ViewModel
{
    public class StudentListViewModel : INotifyPropertyChanged
    {
        private readonly UserService _userService;
        public ObservableCollection<StudentDetails> Students { get; set; } = new();
        public ObservableCollection<StudentDetails> FilteredStudents { get; set; } = new();

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterStudents();
            }
        }

        public StudentListViewModel()
        {
            _userService = new UserService();
            LoadStudentsAsync();
        }

        private async void LoadStudentsAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var studentDetails = await _userService.GetStudentDetailsAsync();
                Students.Clear();
                foreach (var student in studentDetails)
                {
                    Students.Add(student);
                }

                // Initialize the FilteredStudents with all data
                FilterStudents();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching students: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void FilterStudents()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredStudents.Clear();
                foreach (var student in Students)
                {
                    FilteredStudents.Add(student);
                }
            }
            else
            {
                var filtered = Students.Where(s =>
                    (s.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.Idno?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.Course?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.College?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.Yearlevel?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (s.SchoolYear?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();

                FilteredStudents.Clear();
                foreach (var student in filtered)
                {
                    FilteredStudents.Add(student);
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}