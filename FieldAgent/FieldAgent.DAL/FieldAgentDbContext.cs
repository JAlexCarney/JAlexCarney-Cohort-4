using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

        public FieldAgentDbContext GetDbContext(string ConnectionString) 
        {
            var options = new DbContextOptionsBuilder<FieldAgentDbContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            return new FieldAgentDbContext(options);
        }

        // insert test data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Create Keys For Bridge Tables
            modelBuilder.Entity<AgencyAgent>()
                .HasKey(k => new { k.AgencyId, k.AgentId});
            modelBuilder.Entity<MissionAgent>()
                .HasKey(k => new { k.MissionId, k.AgentId });

            // Set Delete Mode and key constraintes
            modelBuilder.Entity<Alias>()
                .HasOne(a => a.Agent)
                .WithMany(a => a.Aliases)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AgencyAgent>()
                .HasOne(aa => aa.Agent)
                .WithMany(a => a.AgencyAgents)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Agent>()
                .HasMany(a => a.Missions)
                .WithMany(m => m.Agents);
            modelBuilder.Entity<Location>()
                .HasOne(l => l.Agency)
                .WithMany(a => a.Locations)
                .OnDelete(DeleteBehavior.NoAction);

            // Populate Constant Security Clearance Table
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

            // Call Method on Base Class
            base.OnModelCreating(modelBuilder);
        }
    }
}
