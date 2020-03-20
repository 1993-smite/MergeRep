using ServiceStack.Redis;
using System;

namespace RabbitBrowser
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new RedisManagerPool("127.0.0.1:6379");
            using (var client = manager.GetClient())
            {
                /*client.Set("users", "gbond,1266;" +
                                     "dduff,4488;" +
                                     "ikant,9991;" +
                                     "ablock,1177");*/
                var users = client.Get<string>("users").Split(";");
                foreach(var user in users)
                {
                    var data = user.Split(',');
                    Console.WriteLine($"login - '{data[0]}' , password - '{data[1]}'");
                }
            }
            Console.WriteLine("The end!");
            Console.ReadLine();
        }
    }
}
