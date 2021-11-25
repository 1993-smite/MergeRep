using DB.DBModels;
using Microsoft.EntityFrameworkCore;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace DB.Repositories.User
{
    public class UserRepository: CommonRepository<DBUser, UserFilter>
    {
        public override IEnumerable<DBUser> GetList([NotNull] UserFilter filter)
        {
            var users = new List<DBUser>();
            using (ApplicationContext db = new ApplicationContext())
            {
                users = db.Users.Include(x => x.City).Where(filter.ToFilter()).ToList();
                foreach (var usr in users)
                {
                    var login = db.Logins.FirstOrDefault(x => x.UserId == usr.Id).Login;
                    usr.Logins.Add(new DBLogin()
                    {
                        Login = login
                    });
                }
            }
            return users;
        }

        public override DBUser Get([NotNull] UserFilter filter)
        {
            var user = new DBUser();
            using (ApplicationContext db = new ApplicationContext())
            {
                IQueryable<DBUser> users = db.Users.Include(x => x.City);

                if (filter.Id > 0)
                {
                    user = users.FirstOrDefault(x => x.Id == filter.Id);
                }

                if (string.IsNullOrEmpty(filter.Login))
                {
                    DBLogin login = db.Logins
                        .Include(x => x.User)
                        .FirstOrDefault(x => x.Login == filter.Login);
                    user = login.User;
                    user.Logins.Add(login);
                }
            }
            return user;
        }

        public override DBUser Save(ApplicationContext db, DBUser user)
        {
            DBUser usr;
            int userId = user.Id;

            if (userId < 1)
            {
                userId = db.Users.OrderBy(x => x.Id).LastOrDefault()?.Id ?? 1;
                user.Id = ++userId;
                db.Users.Add(user);
                usr = user;
            }
            else
            {
                usr = db.Users.FirstOrDefault(x => x.Id == userId);
                if (usr == null)
                    throw new Exception($"Нет записи user с таким Id = {userId}");
                user.CityId = usr.CityId;
                db.Entry(usr).CurrentValues.SetValues(user);
                db.Entry(usr).State = EntityState.Modified;
            }

            db.SaveChanges();

            foreach (var login in user.Logins)
            {
                DBLogin lgn = db.Logins
                    .FirstOrDefault(x => x.Login == login.Login);
                if (lgn == null)
                {
                    lgn = login;
                    lgn.UserId = userId;
                    db.Logins.Add(lgn);
                }
                else
                {
                    lgn = login;
                }
            }

            return usr;
        }

        #region UserComment
        public static List<DBUserComment> GetUserComments(int userId)
        {
            var userComments = new List<DBUserComment>();
            using (ApplicationContext db = new ApplicationContext())
            {
                userComments = db.UserComments.Where(x => x.UserId == userId).Include(x => x.User).Include(x => x.CreateUser).ToList();
                foreach(var comment in userComments)
                {
                    comment.Invoits = db.UserCommentInvoits.Where(x => x.UserCommentId == comment.Id).Include(x => x.User).ToList();
                }
            }
            return userComments;
        }

        public static DBUserComment GetUserComment(int id)
        {
            var userComment = new DBUserComment();
            using (ApplicationContext db = new ApplicationContext())
            {
                userComment = db.UserComments.Where(x => x.Id == id).Include(x => x.User).Include(x => x.CreateUser).FirstOrDefault();
                userComment.Invoits = db.UserCommentInvoits.Where(x => x.UserCommentId == userComment.Id).Include(x => x.User).ToList();
            }
            return userComment;
        }

        public static int SaveUserComment(DBUserComment comment)
        {
            int id = comment.Id;
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var dBUserComment = db.UserComments
                                .FirstOrDefault(
                                    x => x.Id == comment.Id
                                      && x.UserId == comment.UserId);
                        if (id < 1)
                        {
                            id = db.UserComments.OrderBy(x => x.Id).LastOrDefault()?.Id ?? 0;
                            id = ++id;
                            comment.Id = id;
                            comment.UpdateDT = comment.CreateDT;
                            db.UserComments.Add(comment);
                        }
                        else
                        {
                            if (comment == null)
                                throw new Exception($"Нет записи user с таким Id = {comment.Id} и UserId = {comment.UserId}");
                            comment.UpdateDT = DateTime.Now;
                            db.Entry(dBUserComment).CurrentValues.SetValues(comment);
                            db.Entry(dBUserComment).State = EntityState.Modified;
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return id;
        }
        public static void SaveUserCommentInvoit(DBUserCommentInvoit commentInvoit)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var dBUserCommentInvoit = db.UserCommentInvoits
                            .FirstOrDefault(
                                x => x.UserCommentId == commentInvoit.UserCommentId
                                  && x.UserId == commentInvoit.UserId);
                        if (dBUserCommentInvoit == null)
                        {
                            var lastId = db.UserCommentInvoits.LastOrDefault().Id;
                            commentInvoit.Id = ++lastId;
                            db.UserCommentInvoits.Add(commentInvoit);
                        }
                        else
                        {
                            db.UserCommentInvoits.Remove(dBUserCommentInvoit);
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
        #endregion
    }
}
