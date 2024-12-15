    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace StudentRecordPDC50.Model
    {
        public class AcademicHistory
        {
            public int ID { get; set; }
            public string StudentID { get; set; }  // Foreign key referencing student ID
            public string College { get; set; }
            public string Course { get; set; }
            public string Yearlevel { get; set; }
            public string Status { get; set; }
            public string Semester { get; set; }
            public string SchoolYear { get; set; }
        }
    }

