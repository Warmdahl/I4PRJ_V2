namespace RCCS.DatabaseCitizenResidency.ViewModel
{
    public class CitizenListViewModel
    {
        public string CPR { get; set; }
        public string CitizenName { get; set; }
        public string RespiteCareHome { get; set; }
        //Calculated
        public string TimeUntilDischarge { get; set; }
        public string ProspectiveSituationStatusForCitizen { get; set; }
    }
}
