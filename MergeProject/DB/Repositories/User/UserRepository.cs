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
    }
}
