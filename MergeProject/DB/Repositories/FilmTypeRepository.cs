using DB.DBModels;
using Microsoft.EntityFrameworkCore;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB.Repositories
{
    public class FilmTypeRepository
    {
        #region GET
        /// <summary>
        /// get list types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DBFilmType> Get()
        {
            IEnumerable<DBFilmType> _types;
            using (ApplicationContext db = new ApplicationContext())
            {
                _types = db.FilmTypes.ToList();
            }
            return _types;
        }
        /// <summary>
        /// get film types by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<DBFilmType> Get(string name)
        {
            IEnumerable<DBFilmType> res = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                IQueryable<DBFilmType> dbFilmTypes = db.FilmTypes;

                res = dbFilmTypes.Where(x => string.Equals(x.Name.Trim(), name.Trim()));
            }

            return res;
        }

        /// <summary>
        /// get film type by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DBFilmType Get(int id)
        {
            DBFilmType res = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                IQueryable<DBFilmType> dbFilmTypes = db.FilmTypes;

                res = dbFilmTypes.FirstOrDefault(x => x.Id == id);
            }

            return res;
        }
        #endregion

        #region SAVE

        /// <summary>
        /// сохранение фильма
        /// </summary>
        /// <param name="dbFilm"></param>
        /// <returns></returns>
        public long Save(DBFilm dbFilm)
        {
            long id = dbFilm.Id;
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (id < 1)
                        {
                            id = db.Films.OrderBy(x => x.Id).LastOrDefault()?.Id ?? 0;
                            ++id;
                            dbFilm.Id = id;
                            db.Films.Add(dbFilm);
                        }
                        else
                        {
                            var exist = db.Films.FirstOrDefault(x => x.Id == id);
                            db.Entry(exist)
                              .CurrentValues
                              .SetValues(dbFilm);
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
            return id;
        }
        #endregion
    }
}
