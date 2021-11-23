using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.DBModels
{
    [Table("user-comment-invoits")]
    public class DBUserCommentInvoit
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("UserCommentId")]
        public DBUserComment UserComment { get; set; }
        public int UserCommentId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public DBUser User { get; set; }
    }
}
