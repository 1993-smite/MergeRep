using System.ComponentModel.DataAnnotations.Schema;

namespace DB.DBModels
{
    [Table("films")]
    public class DBFilm
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("year")]
        public int Year { get; set; }
        [Column("type_id")]
        public int TypeId { get; set; }
        [Column("descriptions")]
        public string Descriptions { get; set; }
        [Column("country")]
        public string Country { get; set; }
        [Column("timing")]
        public long? Timing { get; set; }
        [Column("budget")]
        public long? Budget { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
}
