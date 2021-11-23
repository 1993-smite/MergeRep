using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.DBModels
{
    [Table("tasks")]
    public class DBTask
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
}
