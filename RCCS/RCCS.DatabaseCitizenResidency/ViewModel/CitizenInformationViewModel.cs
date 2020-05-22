using System;
using System.Collections.Generic;
using RCCS.DatabaseCitizenResidency.Model;

namespace RCCS.DatabaseCitizenResidency.ViewModel
{
    public class CitizenInformationViewModel
    {
        //Person info (Citizen.cs)
        public string CPR { get; set; }
        public string CitizenName { get; set; }
        public string Age { get; set; }

        //Care info (RespiteCareHome.cs)
        public string RespiteCareHome { get; set; } //Name of care center
        public string CareHomeAddress { get; set; }

        //Room type (RespiteCareRoom.cs)
        public string CareHomeType { get; set; }    //Dementia or regular

        //Residency info (ResidenceInformation.cs) 
        public DateTime DateOfAdmission { get; set; } //Opholds start dato
        public DateTime EvaluationDate { get; set; }
        public string TimeUntilDischarge { get; set; } //Calculated
        public string ProspectiveSituationStatusForCitizen { get; set; }

        //Care overview (CitizenOverview.cs)
        public string CareSituation { get; set; }
        public int AmountOfEvaluations { get; set; }
        public string CareNeed { get; set; }
        public string PurposeOfStay { get; set; }

        //ProgressReport (ProgressReport.cs)
        public List<ProgressReport> ProgressReports { get; set; }
        //public DateTime DateOfStatus { get; set; }
        //public string StatusOverview { get; set; }

        //Next of kin (Relative.cs)
        public List<Relative> Relatives { get; set; }

        //public bool NokRole { get; set; } //primary = true, secondary = false etc
        //public string NokName { get; set; }
        //public string NokRelation { get; set; } //Son, daughter, friend etc
        //public int NokPhone { get; set; }
    }
}
