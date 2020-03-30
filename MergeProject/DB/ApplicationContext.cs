using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgresApp
{
    [Table("cities")]
    public class DBCity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }

    [Table("user-logins")]
    public class DBLogin
    {
        [Column("user-login")]
        [Key]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public DBUser User { get; set; }
    }

    [Table("users")]
    public class DBUser
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("last-name")]
        public string LastName { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("middle-name")]
        public string MiddleName { get; set; }
        [Column("e-mail")]
        public string Email { get; set; }
        [Column("age")]
        public int Age { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        public DBCity City { get; set; }
        public List<DBLogin> Logins { get; set; } = new List<DBLogin>();
    }

    [Table("user-comments")]
    public class DBUserComment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public DBUser User { get; set; }
        [Column("create-user_id")]
        public int? CreateUserId { get; set; }
        public DBUser CreateUser { get; set; }
        [Column("parent_id")]
        public int ParentId { get; set; }
        [Column("create-dt")]
        public DateTime CreateDT { get; set; }
        /*[Column("update-dt")]
        public DateTime? UpdateDT { get; set; }*/
        [Column("text")]
        public string Text { get; set; }
    }

    public class ApplicationContext : DbContext
    {
        public DbSet<DBUser> Users { get; set; }
        public DbSet<DBCity> Cities { get; set; }
        public DbSet<DBLogin> Logins { get; set; }
        public DbSet<DBUserComment> UserComments { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test;Username=user-query;Password=123456");
        }
    }
}
