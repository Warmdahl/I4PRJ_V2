using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using RCCS.Database.Model;

namespace RCCS.Database.Model
{
    public class Citizen
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CPR { get; set; }

        [JsonIgnore]
        public List<Relative> Relatives { get; set; }

        [JsonIgnore]
        public List<ProgressReport> ProgressReports { get; set; } //Sta

        [JsonIgnore]
        public CitizenOverview CitizenOverview { get; set; }

        [JsonIgnore]
        public ResidenceInformation ResidenceInformation { get; set; }

        [JsonIgnore]
        public RespiteCareRoom RespiteCareRoom { get; set; }
    }
}
