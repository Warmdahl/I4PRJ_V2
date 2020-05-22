using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCCS.DatabaseUsers.Model.Entities
{
    public class EfCoordinator
    {
        public long EfCoordinatorId { get; set; }
        public long EfUserId { get; set; }
        public EfUser User { get; set; }
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(254)]
        public string PersonaleId { get; set; }
        [MaxLength(12)]
        public string PhoneNo { get; set; }
    }
}
