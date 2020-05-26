using System;

namespace RCCS.DatabaseCitizenResidency.ViewModel
{
    public class CreateCitizenViewModel
    {
        //Citizen
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? CPR { get; set; }

        //Relative
        public string RelativeFirstName { get; set; }
        public string RelativeLastName { get; set; }
        public int PhoneNumber { get; set; }
        public string Relation { get; set; }
        public bool IsPrimary { get; set; }

        //Residence Information
        public DateTime StartDate { get; set; }
        public DateTime ReevaluationDate { get; set; }
        public DateTime PlannedDischargeDate { get; set; }
        public string ProspectiveSituationStatusForCitizen { get; set; }

        //Citizen Overview 
        public string CareNeed { get; set; }
        public string PurposeOfStay { get; set; }

        //RespiteCareRoom
        public string RespiteCareHomeName { get; set; }
        public int Type { get; set; }
    }
}
