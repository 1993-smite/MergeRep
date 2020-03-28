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

        public Func<User,bool> ToFilter()
        {
            Func<User, bool> func = x =>
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

    public static class UserMapper
    {
        public static User GetUser(int Id)
        {
            var filter = new FilterUser() { Id = Id }.ToFilter();
            var user = new User();
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.FirstOrDefault(filter);
            }
            return user;
        }

        public static List<User> GetUsers(string name)
        {
            var filter = new FilterUser() { Name = name }.ToFilter();
            var user = new List<User>();
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.Where(filter).ToList();
            }
            return user;
        }
    }
}
