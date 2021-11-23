using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.DBModels
{
    [Table("addresses")]
    public class DBAddress
    {
        [Key]
        [Column("address")]
        public string Address { get; set; }

        [Column("lon")]
        public decimal Lon { get; set; }

        [Column("lat")]
        public decimal Lat { get; set; }
    }
}
