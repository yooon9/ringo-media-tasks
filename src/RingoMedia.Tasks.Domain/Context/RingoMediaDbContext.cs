namespace RingoMedia.Tasks.Domain.Context
{
    using Microsoft.EntityFrameworkCore;
    using RingoMedia.Tasks.Domain.DbEntities;

    public class RingoMediaDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        public RingoMediaDbContext(DbContextOptions<RingoMediaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(d => d.SubDepartments)
                .WithOne(d => d.ParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId);

            modelBuilder.Entity<Reminder>();
        }
    }
}
