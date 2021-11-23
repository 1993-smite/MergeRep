using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.DBModels
{
    [Table("user-comments")]
    public class DBUserComment
    {
        [Column("id")]
        [Key]
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
        [Column("update-dt")]
        public DateTime? UpdateDT { get; set; }
        [Column("text")]
        public string Text { get; set; }
        public int InvoitCount => Invoits?.Count ?? 0;
        public List<DBUserCommentInvoit> Invoits { get; set; }
    }
}
