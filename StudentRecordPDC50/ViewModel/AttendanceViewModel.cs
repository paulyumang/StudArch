using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using StudentRecordPDC50.Model;
using StudentRecordPDC50.Services;

namespace StudentRecordPDC50.ViewModel
{
    public class AttendanceViewModel : BindableObject
    {
        private readonly UserService _userService;

        public ObservableCollection<AttendanceRecord> AttendanceRecords { get; set; }
        public ICommand LoadAttendanceCommand { get; }

        private string _studentidno;
        public string Studentidno
        {
            get => _studentidno;
            set
            {
                _studentidno = value;
                OnPropertyChanged();
            }
        }

        public AttendanceViewModel()
        {
            _userService = new UserService();
            AttendanceRecords = new ObservableCollection<AttendanceRecord>();
            LoadAttendanceCommand = new Command(async () => await LoadAttendance());
        }

        private async Task LoadAttendance()
        {
            if (string.IsNullOrWhiteSpace(Studentidno))
                return;

            var attendanceData = await _userService.GetAttendanceAsync(Studentidno);
            AttendanceRecords.Clear();
            foreach (var record in attendanceData)
            {
                AttendanceRecords.Add(record);
            }
        }

}
}
