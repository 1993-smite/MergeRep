using DB.DBModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DB.Repositories.User
{
    public class UserFilter : Filter
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

        public UserFilter(string login)
        {
            Login = login;
        }

        public UserFilter(int id)
        {
            Id = id;
        }
    }
}
