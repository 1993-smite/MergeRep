using Films.Models;
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

        public IEnumerable<Film> GetFilms(int page, int pageCount)
        {
            var films = new List<Film>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var dbFilms = db.Films.Skip((page - 1) * pageCount).Take(pageCount);

                foreach(var dbFilm in dbFilms)
                {
                    var dbType = db.FilmTypes.FirstOrDefault(x => x.Id == dbFilm.TypeId);
                    var type = _typesConverter.toView(dbType);

                    films.Add(_converter.SetType(type).toView(dbFilm));
                }

            }
            return films;
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
