namespace RCCS.DatabaseCitizenResidency.ViewModel
{
    public class RespiteCareHomeListViewModel
    {
        
        public string RespiteCareHome { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
        public string RespiteCareRoomsTotal { get; set; }

        //Calculated

        public string AvailableRespiteCareRooms { get; set; }
        public string NextAvailableRespiteCareRoom { get; set; }
    }
}
