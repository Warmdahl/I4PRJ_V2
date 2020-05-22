using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RCCS.DatabaseCitizenResidency.Model
{
    public class Relative
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PhoneNumber { get; set; }
        public string Relation { get; set; }
        public bool IsPrimary { get; set; }

        [JsonIgnore]
        public Citizen Citizen { get; set; }
        public long CitizenCPR { get; set; }
    }
}
