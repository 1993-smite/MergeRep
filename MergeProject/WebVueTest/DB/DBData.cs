using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.DB
{
    public static class DBData
    {
        public static string GetData(string key)
        {
            var manager = new RedisManagerPool("127.0.0.1:6379");
            string datas;
            using (var client = manager.GetClient())
            {
                datas = client.Get<string>(key);
            }
            return datas;
        }
    }
}
