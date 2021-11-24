using DB.DBModels;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DB.Repositories.Task
{
    public class TaskRepository: CommonRepository<DBTask, TaskFilter>
    {
        /// <summary>
        /// get tasks
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<DBTask> GetList([NotNull] TaskFilter filter)
        {
            var tasks = new List<DBTask>();
            using (ApplicationContext db = new ApplicationContext())
            {
                tasks = db.DBTasks
                    .Where(x => x.Status == (int)TaskFilter.State.Active)
                    .OrderBy(x => x.Date)
                    .ToList();
            }
            return tasks;
        }

        /// <summary>
        /// get db task bi id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override DBTask Get([NotNull]TaskFilter filter)
        {
            var task = new DBTask();
            using (ApplicationContext db = new ApplicationContext())
            {
                task = db.DBTasks
                    .Where(x => x.Id == filter.Id)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
            return task;
        }

        /// <summary>
        /// save task 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public DBTask Save(ApplicationContext db, DBTask task)
        {
            int taskId = task.Id;

            DBTask tsk;

            if (taskId < 1)
            {
                taskId = db.DBTasks
                    .OrderBy(x => x.Id)
                    .LastOrDefault()?.Id ?? 1;
                taskId++;
                task.Id = taskId;
                tsk = task;
                db.DBTasks.Add(task);
            }
            else
            {
                tsk = db.DBTasks
                    .FirstOrDefault(x => x.Id == taskId);
                if (tsk == null)
                    throw new NullReferenceException($"Нет записи user с таким Id = {taskId}");
                db.Entry(tsk).CurrentValues.SetValues(task);
                db.Entry(tsk).State = EntityState.Modified;
            }

            return tsk;
        }
    }
}
