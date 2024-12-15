using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
        
namespace StudentRecordPDC50.Model
{
    public class AttendanceRecord
    {
        [JsonPropertyName("Studentidno")]
        public string Studentidno { get; set; }  // Matches "Studentidno" in JSON

        [JsonPropertyName("Entrytime")]
        public string Entrytime { get; set; }   // Matches "Entrytime" in JSON

        [JsonPropertyName("Exittime")]
        public string Exittime { get; set; }    // Matches "Exittime" in JSON

        [JsonPropertyName("Status")]
        public string Status { get; set; }      // Matches "Status" in JSON
    }

}
