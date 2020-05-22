using System.ComponentModel.DataAnnotations;

namespace RCCS.DatabaseUsers.Model.Entities
{
    public class EfAdmin
    {
        public long EfUserId { get; set; }
        public long EfAdminId { get; set; }
        public EfUser User { get; set; }
        [MaxLength(64)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        [MaxLength(254)]
        public string PersonaleId { get; set; }
    }
}
