using PostgresApp;
using System;
using System.Linq;

namespace DBtest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Users
                    .Where(x=>x.Age == 20)
                    .Take(10)
                    .Join(
                        db.Cities
                       ,u=>u.CityId
                       ,c=>c.Id
                       ,(u,c)=> new User
                       {
                           Id = u.Id,
                           Name = u.Name,
                           Age = u.Age,
                           City = new City()
                           {
                                Id = c.Id,
                                Name = c.Name
                           }
                       }).ToList();
                Console.WriteLine("Users list:");
                foreach (User u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Age} - {u.City.Name}");
                }
            }

            Console.ReadLine();
        }
    }
}
