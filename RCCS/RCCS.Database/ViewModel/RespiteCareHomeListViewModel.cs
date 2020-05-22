using System;
using System.Collections.Generic;
using System.Text;
using RCCS.Database.Model;

namespace RCCS.Database.ViewModel
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
