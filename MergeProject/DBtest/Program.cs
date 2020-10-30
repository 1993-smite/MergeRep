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
            using (ApplicationContext db = new ApplicationContext())
            {
                //var comments = db.UserComments.ToList();
                //for(int index = 0;index < comments.Count; index++)
                //{
                //    cnt = rand.Next(1, 4);
                //    for(int count = 0; count < cnt; count++)
                //    {
                //        UserRepository.SaveUserCommentInvoit(new DBUserCommentInvoit()
                //        {
                //            Id = id++,
                //            UserCommentId = comments[index].Id,
                //            UserId = rand.Next(2,8)
                //        });
                //    }
                //}

                //var types = Enum.GetValues(typeof(FilmTypes));

                //foreach (FilmTypes type in types)
                //{
                //    db.FilmTypes.Add(new DBFilmType()
                //    {
                //        Id = (int)type,
                //        Name = type.GetDisplay()
                //    });
                //}

                long count = long.Parse("482522") + 1;

                for(double index=count; index < 2e+6; index += 1)
                {
                    long id = (long)index;

                    db.Films.Add(new DBFilm() {
                        Id = id,
                        Name = $"Фильм № {id}",
                        Year = rand.Next(2000, 2020),
                        TypeId = rand.Next(1,9)
                    });

                    if (index % 1000 == 0)
                        Console.WriteLine(index);

                    db.SaveChanges();
                }
            }

            /*var type = user.GetType();

            foreach (var item in type.GetProperties())
            {
                Console.WriteLine($"\t{item.Name} : {item.GetValue(user)},\t");
            }*/

            Console.WriteLine("The End!");

            Console.ReadLine();
        }
    }
}
