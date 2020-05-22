using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseUsers.Model.Entities;

namespace RCCS.DatabaseUsers.Data
{
    public class RCCSUsersContext : DbContext
    {
        public RCCSUsersContext(DbContextOptions<RCCSUsersContext> options)
            : base(options)
        {
        }

        public DbSet<EfUser> Users { get; set; }
        public DbSet<EfAdmin> Admins { get; set; }
        public DbSet<EfCoordinator> Coordinators { get; set; }
        public DbSet<EfNursingStaff> NursingStaffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure indexes
            modelBuilder.Entity<EfUser>()
                .HasIndex(p => p.PersonaleId)
                .IsUnique();
            modelBuilder.Entity<EfAdmin>()
                .HasIndex(p => p.PersonaleId)
                .IsUnique();
            modelBuilder.Entity<EfCoordinator>()
                .HasIndex(p => p.PersonaleId)
                .IsUnique();
            modelBuilder.Entity<EfNursingStaff>()
                .HasIndex(p => p.PersonaleId)
                .IsUnique();
        }
    }
}
