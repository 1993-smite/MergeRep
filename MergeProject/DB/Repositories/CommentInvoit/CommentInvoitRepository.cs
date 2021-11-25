using System.Linq;
using DB.DBModels;
using PostgresApp;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories.CommentInvoit
{
    public class CommentInvoitRepository: CommonRepository<DBUserCommentInvoit, CommentInvoitFilter>
    {
        public override DBUserCommentInvoit Save(ApplicationContext db, DBUserCommentInvoit commentInvoit)
        {
            var dBUserCommentInvoit = db.UserCommentInvoits
                    .FirstOrDefault(
                        x => x.UserCommentId == commentInvoit.UserCommentId
                            && x.UserId == commentInvoit.UserId);
            if (dBUserCommentInvoit == null)
            {
                var lastId = db.UserCommentInvoits
                    .LastOrDefault().Id;

                commentInvoit.Id = ++lastId;
                db.UserCommentInvoits.Add(commentInvoit);
                db.Entry(commentInvoit).State = EntityState.Added;
            }
            else
            {
                db.UserCommentInvoits.Remove(dBUserCommentInvoit);
                db.Entry(commentInvoit).State = EntityState.Deleted;
            }

            return dBUserCommentInvoit;
        }
    }
}
