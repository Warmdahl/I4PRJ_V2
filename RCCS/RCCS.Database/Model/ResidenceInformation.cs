using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using RCCS.Database.Model;

namespace RCCS.Database.Model
{
    public class ResidenceInformation
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ReevaluationDate { get; set; }
        public DateTime PlannedDischargeDate { get; set; }
        public string ProspectiveSituationStatusForCitizen { get; set; }

        [JsonIgnore]
        public Citizen Citizen { get; set; }
        public long CitizenCPR { get; set; }
    }
}
