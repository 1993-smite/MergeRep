using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.DBModels
{
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
}
