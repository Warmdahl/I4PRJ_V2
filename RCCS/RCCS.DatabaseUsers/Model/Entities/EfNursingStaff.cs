using System.ComponentModel.DataAnnotations;

namespace RCCS.DatabaseUsers.Model.Entities
{
    public class EfNursingStaff
    {
        public long EfNursingStaffId { get; set; }
        public long EfUserId { get; set; }
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(254)]
        public string PersonaleId { get; set; }
    }
}
