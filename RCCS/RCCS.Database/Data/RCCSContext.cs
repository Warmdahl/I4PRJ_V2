using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using RCCS.Database.Model;

namespace RCCS.Database.Data
{
    public class RCCSContext : DbContext
    {
        public RCCSContext(DbContextOptions<RCCSContext> options) : base(options) { }

        //Entities
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<CitizenOverview> CitizenOverviews { get; set; }
        public DbSet<ProgressReport> ProgressReports { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<ResidenceInformation> ResidenceInformations { get; set; }
        public DbSet<RespiteCareRoom> RespiteCareRooms { get; set; }
        public DbSet<RespiteCareHome> RespiteCareHomes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region modelbuilding

            //Keys for entities
            modelBuilder.Entity<Citizen>().HasKey(c => c.CPR);
            modelBuilder.Entity<RespiteCareHome>().HasKey(rch => rch.Name);

            modelBuilder.Entity<RespiteCareRoom>()
                .Property(rcr => rcr.RoomNumber)
                .ValueGeneratedNever();

            modelBuilder.Entity<Citizen>()
                .Property(c => c.CPR)
                .ValueGeneratedNever();

            //Relative One-to-Many
            modelBuilder.Entity<Relative>()
                .HasOne(r => r.Citizen)
                .WithMany(c => c.Relatives)
                .HasForeignKey(r => r.CitizenCPR);

            //ProgressReport One-to-Many
            modelBuilder.Entity<ProgressReport>()
                .HasOne(p => p.Citizen)
                .WithMany(c => c.ProgressReports)
                .HasForeignKey(p => p.CitizenCPR);

            //CitizenOverview One-to-One
            modelBuilder.Entity<CitizenOverview>()
                .HasOne(co => co.Citizen)
                .WithOne(c => c.CitizenOverview)
                .HasForeignKey<CitizenOverview>(co => co.CitizenCPR);

            //ResidenceInformation One-to-One
            modelBuilder.Entity<ResidenceInformation>()
                .HasOne(ri => ri.Citizen)
                .WithOne(c => c.ResidenceInformation)
                .HasForeignKey<ResidenceInformation>(ri => ri.CitizenCPR);

            //RespiteCareRoom One-to-One
            modelBuilder.Entity<RespiteCareRoom>()
                .HasOne(rcr => rcr.Citizen)
                .WithOne(c => c.RespiteCareRoom)
                .HasForeignKey<RespiteCareRoom>(rcr => rcr.CitizenCPR)
                .IsRequired(false);

            //RespiteCareRoom One-to-Many
            modelBuilder.Entity<RespiteCareRoom>()
                .HasOne(rcr => rcr.RespiteCareHome)
                .WithMany(rch => rch.RespiteCareRooms)
                .HasForeignKey(rcr => rcr.RespiteCareHomeName);

            #endregion
        }
    }
}
