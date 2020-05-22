using System.Text.Json.Serialization;

namespace RCCS.DatabaseCitizenResidency.Model
{
    public class RespiteCareRoom
    {
        public int RespiteCareRoomId { get; set; }
        public int RoomNumber { get; set; }
        public string Type { get; set; }
        public bool IsAvailable { get; set; }

        [JsonIgnore]
        public RespiteCareHome RespiteCareHome { get; set; }
        public string RespiteCareHomeName { get; set; }

        [JsonIgnore]
        public Citizen Citizen { get; set; }
        public long? CitizenCPR { get; set; }
    }
}
