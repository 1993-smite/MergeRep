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
    public class FilmTypesController : ControllerBase
    {
        private Lazy<FilmMapper> _db = new Lazy<FilmMapper>(() => new FilmMapper());

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Film>> Get()
        {
            return Ok(_db.Value.GetFilmTypes());
        }
    }
}
