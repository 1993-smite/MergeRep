﻿using System;
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

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Film>> Get(int page = 1, int pageCount = 20)
        {
            return Ok(_db.Value.GetFilms(page, pageCount));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Film> Get(int id)
        {
            return Ok(_db.Value.GetFilm(id));
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
