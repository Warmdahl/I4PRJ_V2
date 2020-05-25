namespace RCCS.DatabaseCitizenResidency.ViewModel
{
    public class CitizenListViewModel
    {
        //Citizen
        public string CPR { get; set; }
        public string CitizenName { get; set; }
        //RespiteCareHome
        public string RespiteCareHome { get; set; }
        //ResidenceInformation
        public string TimeUntilDischarge { get; set; } //Calculated
        public string ProspectiveSituationStatusForCitizen { get; set; }
    }
}
