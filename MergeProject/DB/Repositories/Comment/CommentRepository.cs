using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using DB.DBModels;
using Microsoft.EntityFrameworkCore;
using PostgresApp;

namespace DB.Repositories.Comment
{
    public class CommentRepository: CommonRepository<DBUserComment, CommentFilter>
    {
        public override IEnumerable<DBUserComment> GetList(CommentFilter filter)
        {
            var userComments = new List<DBUserComment>();
            using (ApplicationContext db = new ApplicationContext())
            {
                userComments = db.UserComments
                    .Where(x => x.UserId == filter.UserId)
                    .Include(x => x.User)
                    .Include(x => x.CreateUser)
                    .ToList();
                foreach (var comment in userComments)
                {
                    comment.Invoits = db.UserCommentInvoits
                        .Where(x => x.UserCommentId == comment.Id)
                        .Include(x => x.User)
                        .ToList();
                }
            }
            return userComments;
        }

        public override DBUserComment Get([NotNull] CommentFilter filter)
        {
            var userComment = new DBUserComment();
            using (ApplicationContext db = new ApplicationContext())
            {
                userComment = db.UserComments
                    .Where(x => x.Id == filter.CommentId)
                    .Include(x => x.User)
                    .Include(x => x.CreateUser)
                    .FirstOrDefault();
                userComment.Invoits = db.UserCommentInvoits
                    .Where(x => x.UserCommentId == userComment.Id)
                    .Include(x => x.User)
                    .ToList();
            }
            return userComment;
        }

        public override DBUserComment Save(ApplicationContext db, DBUserComment comment)
        {
            int id = comment.Id;
            var dBUserComment = db.UserComments
                                .FirstOrDefault(
                                    x => x.Id == comment.Id
                                      && x.UserId == comment.UserId);
            if (id < 1 || dBUserComment == null)
            {
                id = db.UserComments
                    .OrderBy(x => x.Id)
                    .LastOrDefault()?.Id ?? 0;
                id = ++id;
                comment.Id = id;
                comment.UpdateDT = comment.CreateDT;
                db.UserComments.Add(comment);
                dBUserComment = comment;
            }
            else
            {
                if (comment == null)
                    throw new Exception($"Нет записи user с таким Id = {comment.Id} и UserId = {comment.UserId}");
                comment.UpdateDT = DateTime.Now;
                db.Entry(dBUserComment).CurrentValues.SetValues(comment);
                db.Entry(dBUserComment).State = EntityState.Modified;
            }

            return dBUserComment;
        }
    }
}
