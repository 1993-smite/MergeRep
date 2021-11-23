using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.DBModels
{
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
}
