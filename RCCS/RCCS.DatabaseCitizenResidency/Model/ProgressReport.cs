using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RCCS.DatabaseCitizenResidency.Model
{
    public class ProgressReport
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Report { get; set; }
        public string ResponsibleCaretaker { get; set; }

        [JsonIgnore]
        public Citizen Citizen { get; set; }
        public long CitizenCPR { get; set; }
    }
}
