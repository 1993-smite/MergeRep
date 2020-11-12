using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Films.Models;
using Films.Services;
using Microsoft.AspNetCore.Mvc;

namespace Films.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private Lazy<FilmMapper> _db = new Lazy<FilmMapper>(() => new FilmMapper());

        // GET list
        [HttpGet]
        public ActionResult<Tuple<IEnumerable<Film>,long>> Get(int page = 1
            , int pageCount = 20
            , string name = ""
            , int year = 0
            , int type = 0)
        {
            return Ok(_db.Value.GetFilms(page, pageCount, name, year, type));
        }

        // GET
        [HttpGet("{id}")]
        public ActionResult<Film> Get(int id)
        {
            return Ok(_db.Value.GetFilm(id));
        }

        // POST
        [HttpPost]
        public ActionResult<long> Post(FilmValid film)
        {
            return Ok(_db.Value.SaveFilm(film));
        }

        // Delete
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _db.Value.DeleteFilm(id);
        }
    }
}
