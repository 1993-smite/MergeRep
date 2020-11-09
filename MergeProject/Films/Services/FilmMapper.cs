using Films.Models;
using Microsoft.EntityFrameworkCore;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Films.Services
{
    public class FilmMapper
    {
        private FilmConverter _converter;
        private FilmTypeConverter _typesConverter;

        public FilmMapper()
        {
            _converter = new FilmConverter();
            _typesConverter = new FilmTypeConverter();
        }

        public Tuple<IEnumerable<Film>, long> GetFilms(
            int page
            , int pageCount
            , string name = ""
            , int year = 0
            , int byType = 0
            )
        {
            long count;

            var films = new List<Film>();
            using (ApplicationContext db = new ApplicationContext())
            {
                IQueryable<DBFilm> dbFilms = db.Films;

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
                dbFilms = dbFilms
                    .Skip((page - 1) * pageCount)
                    .Take(pageCount);

                foreach(var dbFilm in dbFilms)
                {
                    var dbType = db.FilmTypes
                        .FirstOrDefault(x => x.Id == dbFilm.TypeId);
                    var type = _typesConverter
                        .toView(dbType);

                    films.Add(_converter
                                .SetType(type)
                                .toView(dbFilm));
                }

            }
            return new Tuple<IEnumerable<Film>, long>(films, count);
        }

        public Film GetFilm(int id)
        {
            DBFilm dBFilm;
            DBFilmType dBFilmType;

            using (ApplicationContext db = new ApplicationContext())
            {
                dBFilm = db.Films.FirstOrDefault(x => x.Id == id);
                dBFilmType = db.FilmTypes.FirstOrDefault(x => x.Id == dBFilm.TypeId);       
            }

            var type = _typesConverter.toView(dBFilmType);
            var film = _converter.SetType(type).toView(dBFilm);

            return film;
        }

        public long SaveFilm(Film film)
        {
            long Id = film.Id;

            using (ApplicationContext db = new ApplicationContext())
            {
                if (film.Id < 1)
                {
                    Id = db.Films.OrderBy(x=>x.Id).LastOrDefault()?.Id ?? 0;
                    ++Id;
                    var dbFilm = _converter.toDB(film);
                    dbFilm.Id = Id;
                    db.Films.Add(dbFilm);
                }
                else
                {
                    var exist = db.Films.FirstOrDefault(x => x.Id == Id);
                    var upFilm = _converter.toDB(film);
                    db.Entry(exist)
                      .CurrentValues
                      .SetValues(upFilm);
                }

                db.SaveChanges();

            }

            return Id;
        }

        public IEnumerable<FilmType> GetFilmTypes()
        {
            var types = new List<FilmType>();

            using (ApplicationContext db = new ApplicationContext())
            {
                var dbTypes = db.FilmTypes;

                foreach(var dbType in dbTypes)
                {
                    types.Add(_typesConverter.toView(dbType));
                }
            }
            return types;
        }
    }
}
