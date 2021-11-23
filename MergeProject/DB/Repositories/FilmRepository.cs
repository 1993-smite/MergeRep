using DB.DBModels;
using Microsoft.EntityFrameworkCore;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB.Repositories
{
    public class FilmRepository
    {
        public enum FilmState
        {
            Active = 0
        }

        #region GET
        /// <summary>
        /// get list
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageCount"></param>
        /// <param name="name"></param>
        /// <param name="year"></param>
        /// <param name="byType"></param>
        /// <returns></returns>
        public Tuple<IEnumerable<DBFilm>,int> Get(
            int page
            , int pageCount
            , string name = ""
            , int year = 0
            , int byType = 0
            )
        {
            IQueryable<DBFilm> dbFilms;
            IEnumerable<DBFilm> dbFilmsList;
            int count = 0;

            using (ApplicationContext db = new ApplicationContext())
            {
                dbFilms = db.Films;

                dbFilms = dbFilms
                    .Where(x => x.Status == (int)FilmState.Active);

                if (year > 0)
                    dbFilms = dbFilms
                        .Where(x => x.Year == year);

                if (byType > 0)
                    dbFilms = dbFilms
                        .Where(x => x.TypeId == byType);

                if (!string.IsNullOrEmpty(name))
                {
                    var lowerName = name.ToLower();
                    dbFilms = dbFilms
                        .Where(x => x.Name
                                    .ToLower()
                                    .Contains(lowerName));
                }


                count = dbFilms.Count();
                dbFilmsList = dbFilms
                    .OrderByDescending(x => x.Id)
                    .Skip((page - 1) * pageCount)
                    .Take(pageCount).ToList();
            }
            return new Tuple<IEnumerable<DBFilm>, int>(dbFilmsList, count);
        }
        /// <summary>
        /// get film by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<DBFilm> Get(string name)
        {
            IEnumerable<DBFilm> res = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                IQueryable<DBFilm> dbFilms = db.Films;

                res = dbFilms
                    .Where(x => string
                        .Equals(
                            x.Name.Trim()
                            , name.Trim()
                            , StringComparison.CurrentCultureIgnoreCase));
            }

            return res;
        }

        /// <summary>
        /// get film by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DBFilm Get(int id)
        {
            DBFilm res = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                IQueryable<DBFilm> dbFilms = db.Films;

                res = dbFilms
                    .FirstOrDefault(x => x.Id == id);
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
                            id = db.Films
                                .OrderBy(x => x.Id)
                                .LastOrDefault()?.Id ?? 0;
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
                            db.Entry(exist)
                              .State = EntityState.Modified;
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return id;
        }

        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteFilm(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var film = db.Films.FirstOrDefault(x => x.Id == id);
                        if (film != null)
                        {
                            db.Films.Remove(film);
                        }
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }

        }
        #endregion
    }
}
