using PostgresApp;
using System;
using System.Linq;
using DB;
using DB.Users;
using System.Collections.Generic;

namespace DBtest
{
    class Program
    {
        static void Main(string[] args)
        {

            var user = UserMapper.GetUser("trusveld"); //UserMapper.GetUser(4);

            /*var user = new DBUser()
            {
                Id = 5,
                Name = "Черчель Уинстон",
                Age = 35,
                City = new DBCity()
                {
                    Id = 3,
                    Name = "Лондон"
                },
                Logins = new List<DBLogin>()
                {
                    new DBLogin()
                    {
                        UserId = 5,
                        Login = "ucherchel",
                        Password = "123456".GetHashCode().ToString()
                    }
                }
            };

            UserMapper.SaveUserLigin(user.Logins.FirstOrDefault());*/

            var type = user.GetType();

            foreach (var item in type.GetProperties())
            {
                Console.WriteLine($"\t{item.Name} : {item.GetValue(user)},\t");
            }

            Console.ReadLine();
        }
    }
}
