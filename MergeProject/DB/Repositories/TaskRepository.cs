using DB.DBModels;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DB.Repositories
{
    public static class TaskRepository
    {
        /// <summary>
        /// get db task bi id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static DBTask GetTask(long Id)
        {
            var task = new DBTask();
            using (ApplicationContext db = new ApplicationContext())
            {
                task = db.DBTasks
                    .Where(x => x.Id == Id)
                    .AsNoTracking()
                    .FirstOrDefault();
            }
            return task;
        }

        /// <summary>
        /// get tasks
        /// </summary>
        /// <returns></returns>
        public static List<DBTask> GetTasks()
        {
            var tasks = new List<DBTask>();
            using (ApplicationContext db = new ApplicationContext())
            {
                tasks = db.DBTasks
                    .Where(x => x.Status == 1)
                    .OrderBy(x=>x.Date)
                    .ToList();
            }
            return tasks;
        }

        /// <summary>
        /// save task 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static int SaveTask(DBTask task)
        {
            int taskId = task.Id;
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        DBTask tsk;

                        if (taskId < 1)
                        {
                            taskId = db.DBTasks
                                .OrderBy(x => x.Id)
                                .LastOrDefault()?.Id ?? 1;
                            taskId++;
                            task.Id = taskId;
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

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return taskId;
        }
    }
}
