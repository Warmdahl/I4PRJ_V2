using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RCCS.Database.Model
{
    public class RespiteCareHome
    {
        public int AvailableRespiteCareRooms { get; set; }
        public int RespiteCareRoomsTotal { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public List<RespiteCareRoom> RespiteCareRooms { get; set; }
    }
}
