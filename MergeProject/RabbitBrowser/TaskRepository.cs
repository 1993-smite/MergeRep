using DB.DBModels;
using DB.Repositories;
using DB.Repositories.Task;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;

namespace RedisBrowser
{
    public class TaskRedisRepository: CommonRepository<DBTask, TaskFilter>
    {
        Lazy<RedisManagerPool> _lazyRedisManagerPool 
            = new Lazy<RedisManagerPool>(()=> new RedisManagerPool("127.0.0.1:6379"));
        RedisManagerPool Manager => _lazyRedisManagerPool.Value;

        public override IEnumerable<DBTask> GetList(TaskFilter filter = null)
        {
            IEnumerable<DBTask> tasks;

            IEnumerable<string> filters = new List<string>() { "task-1", "task-2" };

            using (var client = Manager.GetClient())
            {
                tasks = (IEnumerable<DBTask>)client
                    .GetAll<DBTask>(filters);

            }
            return tasks;
        }

        public override DBTask Get(TaskFilter filter = null)
        {
            DBTask tasks;
            using (var client = Manager.GetClient())
            {
                tasks = client
                    .Get<DBTask>($"task-{filter?.Id ?? 0}");

            }
            return tasks;
        }

        public override TOutParam Save<TOutParam>(DBTask model)
        {
            throw new NotImplementedException();
        }
    }
}
