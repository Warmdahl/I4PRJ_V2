using System.ComponentModel.DataAnnotations;

namespace RCCS.DatabaseUsers.Model.Entities
{
    public enum Role
    {
        Admin,
        Coordinator,
        NursingStaff
    }
    public class EfUser
    {
        [Key]
        public long EfUserId { get; set; }
        [MaxLength(254)]
        public string PersonaleId { get; set; }
        [MaxLength(60)]
        public string PwHash { get; set; }
        public Role Role { get; set; }
    }
}
