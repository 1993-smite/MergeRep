using DB.DBModels;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace PostgresApp
{
    public class ApplicationContext : DbContext
    {
        //private readonly StreamWriter _sw = new StreamWriter("D:/logs/db1.txt", true);

        public DbSet<DBFilmType> FilmTypes { get; set; }
        public DbSet<DBFilm> Films { get; set; }
        public DbSet<DBUser> Users { get; set; }
        public DbSet<DBCity> Cities { get; set; }
        public DbSet<DBContact> Contacts { get; set; }
        public DbSet<DBLogin> Logins { get; set; }
        public DbSet<DBTask> DBTasks { get; set; }
        public DbSet<DBAddress> DBAddress { get; set; }
        public DbSet<DBUserComment> UserComments { get; set; }
        public DbSet<DBUserCommentInvoit> UserCommentInvoits { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=user-query;Password=123456");
            //optionsBuilder.LogTo(_sw.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBFilm>().HasIndex(u => u.Id);
            modelBuilder.Entity<DBFilm>().HasIndex(u => u.Status);
            modelBuilder.Entity<DBFilm>().HasIndex(u => u.Year);
            modelBuilder.Entity<DBFilm>().HasIndex(u => u.TypeId);
        }
    }
}
