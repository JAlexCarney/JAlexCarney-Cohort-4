using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL
{
    public class FieldAgentDbContext : DbContext
    {
        // Data Tables
        public DbSet<Agency> Agency { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Alias> Alias { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<SecurityClearance> SecurityClearance { get; set; }
        
        // Bridge Tables
        public DbSet<MissionAgent> MissionAgent { get; set; }
        public DbSet<AgencyAgent> AgencyAgent { get; set; }

        public FieldAgentDbContext(DbContextOptions options) : base(options)
        {
        }

        // insert test data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgencyAgent>()
                .HasKey(k => new { k.AgencyId, k.AgentId});
            modelBuilder.Entity<MissionAgent>()
                .HasKey(k => new { k.MissionId, k.AgentId });
            modelBuilder.Entity<Alias>()
                .HasOne(a => a.Agent)
                .WithMany(a => a.Aliases);
            modelBuilder.Entity<AgencyAgent>()
                .HasOne(aa => aa.Agent)
                .WithMany(a => a.AgencyAgents);
            modelBuilder.Entity<AgencyAgent>()
                .HasOne(aa => aa.Agency)
                .WithMany(a => a.AgencyAgents);
            modelBuilder.Entity<SecurityClearance>()
                .HasData(
                    new SecurityClearance
                    {
                        SecurityClearanceId = 1,
                        SecurityClearanceName = "None"
                    },
                    new SecurityClearance
                    {
                        SecurityClearanceId = 2,
                        SecurityClearanceName = "Retired"
                    },
                    new SecurityClearance
                    {
                        SecurityClearanceId = 3,
                        SecurityClearanceName = "Secret"
                    },
                    new SecurityClearance
                    {
                        SecurityClearanceId = 4,
                        SecurityClearanceName = "Top Secret"
                    },
                    new SecurityClearance
                    {
                        SecurityClearanceId = 5,
                        SecurityClearanceName = "Black Ops"
                    }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
