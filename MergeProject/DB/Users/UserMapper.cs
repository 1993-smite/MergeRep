using Microsoft.EntityFrameworkCore;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB.Users
{
    public class FilterUser
    {
        public int Id { get; set; } = -1;
        public string Name { get; set; } = "";
        public string Login { get; set; } = "";

        public Func<DBUser, bool> ToFilter()
        {
            Func<DBUser, bool> func = x =>
            (
                (Id < 1) || (x.Id == Id)
            )
            &&
            (
                (string.IsNullOrEmpty(Name)) || EF.Functions.Like(x.Name, $"%{Name}%")
            );
            return func;
        }
    }

    public static class UserRepository
    {
        #region Get
        public static DBUser GetUser(int Id)
        {
            var filter = new FilterUser() { Id = Id }.ToFilter();
            var user = new DBUser();
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.Include(x=>x.City).Where(filter).FirstOrDefault();
            }
            return user;
        }

        public static DBUser GetUser(string login)
        {
            var logins = new List<DBLogin>();
            var users = new List<DBUser>();
            var user = new DBUser();
            using (ApplicationContext db = new ApplicationContext())
            {
                logins = db.Logins
                    .Where(l => l.Login == login).ToList();
                for (int i = 0; i < logins.Count; i++)
                {
                    user = users.FirstOrDefault(x => x.Id == logins[i].UserId);
                    user = user == null ? db.Users.FirstOrDefault(x => x.Id == logins[i].UserId) : user;
                    if (user == null)
                        throw new Exception($"Нет записи user с таким Login = {login}");
                    user.Logins = new List<DBLogin>();
                    user.Logins.Add(logins[i]);
                    users.Add(user);
                }
            }
            return users.FirstOrDefault();
        }

        public static List<DBUser> GetUsers(string name)
        {
            var filter = new FilterUser() { Name = name }.ToFilter();
            var user = new List<DBUser>();
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.Where(filter).ToList();
            }
            return user;
        }
        #endregion
        public static int SaveUser(DBUser user)
        {
            int userId = user.Id;
            using (ApplicationContext db = new ApplicationContext())
            {
                DBUser usr;

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
                    //db.Update(usr);
                }

                db.SaveChanges();

                usr = db.Users.FirstOrDefault(x => x.Id == userId);

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

                db.SaveChanges();
            }
            return userId;
        }

        public static void SaveUserLogin(DBLogin login)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                DBLogin lgn = db.Logins
                    .FirstOrDefault(x => x.Login == login.Login);
                lgn = login;

                db.SaveChanges();
            }
        }
    }
}
