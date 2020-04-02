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

            //var user = UserMapper.GetUser("trusveld"); //UserMapper.GetUser(4);

            /*var user = new DBUser()
            {
                Id = 0,
                Name = "Блом Владимир",
                Age = 36,
                CityId = 1,
                /*City = new DBCity()
                {
                    Id = 1,
                    Name = "Москва"
                },*
                Logins = new List<DBLogin>()
                {
                    new DBLogin()
                    {
                        UserId = 5,
                        Login = "vblom",
                        Password = "123456".GetHashCode().ToString()
                    }
                }
            };

            UserRepository.SaveUser(user);*/
            Random rand = new Random();
            int cnt = 0;
            int id = 19;
            using (ApplicationContext db = new ApplicationContext())
            {
                var comments = db.UserComments.ToList();
                for(int index = 0;index < comments.Count; index++)
                {
                    cnt = rand.Next(1, 4);
                    for(int count = 0; count < cnt; count++)
                    {
                        UserRepository.SaveUserCommentInvoit(new DBUserCommentInvoit()
                        {
                            Id = id++,
                            UserCommentId = comments[index].Id,
                            UserId = rand.Next(2,8)
                        });
                    }
                }
                db.SaveChanges();
            }

            /*var type = user.GetType();

            foreach (var item in type.GetProperties())
            {
                Console.WriteLine($"\t{item.Name} : {item.GetValue(user)},\t");
            }*/

            Console.ReadLine();
        }
    }
}
