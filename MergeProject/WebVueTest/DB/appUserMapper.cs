using DB.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVueTest.DB.Mappers;
using WebVueTest.Models;

namespace WebVueTest.DB
{
    public class appUserMapper
    {
        private const string dbKey = "users";
        Lazy<UserMapper> _lazyUserMapper = new Lazy<UserMapper>(() => new UserMapper());
        UserMapper UserMapper => _lazyUserMapper.Value;

        public List<appUser> GetAppUsers()
        {
            var userDatas = DBData.GetData("users").Split(';');
            List<appUser> users = new List<appUser>();
            foreach(var userData in userDatas)
            {
                var data = userData.Split(',');
                users.Add(new appUser(data[0],data[1]));
            }
            return users;
        }

        public appUser GetAppDBUser(string login)
        {
            var user = UserMapper.GetUser(new UserFilter(login));
            return user;
        }

        public appUser GetAppUsers(string login)
        {
            var users = GetAppUsers();
            return users.FirstOrDefault(x=>x.Login == login);
        }

    }
}
