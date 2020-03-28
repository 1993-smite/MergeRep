using PostgresApp;
using System;
using System.Linq;
using DB;
using DB.Users;

namespace DBtest
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = UserMapper.GetUser(123);

            var type = users.GetType();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            foreach (var item in type.GetProperties())
            {
                Console.WriteLine($"\t{item.Name} : {item.GetValue(users)},\t");
            }

            Console.ReadLine();
        }
    }
}
