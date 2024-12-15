using StudentRecordPDC50.View;

namespace StudentRecordPDC50
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("StudentPage", typeof(StudentPage));
        }
    }
}