using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QueR.DAL.Seed;
using QueR.Domain.Entities;
using System;

namespace QueR.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Company> Companies { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<QueueType> QueueTypes { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=QuerDb;Trusted_Connection=True;MultipleActiveResultSets=True;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>()
                .HasOne(c => c.Administrator)
                .WithOne(u => u.AdministratedCompany)
                .HasForeignKey<Company>(c => c.AdministratorId);
            builder.Entity<Company>()
                .HasMany(c => c.AvailableQueueTypes)
                .WithOne(t => t.Company)
                .HasForeignKey(t => t.CompanyId);
            builder.Entity<Company>()
                .HasMany(c => c.Sites)
                .WithOne(s => s.Company)
                .HasForeignKey(s => s.CompanyId);
            builder.Entity<Company>()
                .HasMany(c => c.Users)
                .WithOne(u => u.Company)
                .HasForeignKey(u => u.CompanyId);
            builder.Entity<Company>().HasData(DbSeed.Companies);

            builder.Entity<Queue>()
                .HasOne(q => q.Type)
                .WithMany(t => t.Queues)
                .HasForeignKey(q => q.TypeId);
            builder.Entity<Queue>()
                .HasOne(q => q.Site)
                .WithMany(s => s.Queues)
                .HasForeignKey(q => q.SiteId);
            builder.Entity<Queue>()
                .HasMany(q => q.AssignedEmployees)
                .WithOne(e => e.AssignedQueue)
                .HasForeignKey(e => e.AssignedQueueId);
            builder.Entity<Queue>()
                .HasMany(q => q.Tickets)
                .WithOne(t => t.Queue)
                .HasForeignKey(t => t.QueueId);

            builder.Entity<Site>()
                .HasOne(s => s.Manager)
                .WithOne(u => u.ManagedSite)
                .HasForeignKey<Site>(s => s.ManagerId);
            builder.Entity<Site>()
                .HasMany(s => s.Employees)
                .WithOne(u => u.Worksite)
                .HasForeignKey(u => u.WorksiteId);
            builder.Entity<Site>().HasData(DbSeed.Sites);

            builder.Entity<Ticket>()
                .HasOne(t => t.Owner)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.OwnerId);
            builder.Entity<Ticket>()
                .HasOne(t => t.Handler)
                .WithMany(u => u.HandledTickets)
                .HasForeignKey(t => t.HandlerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<QueueType>().HasData(DbSeed.QueueTypes);
        }
    }
}
