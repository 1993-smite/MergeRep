using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebVueTest.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController<T> : ControllerBase
    {
        // GET: api/Default
        [HttpGet]
        public virtual IEnumerable<TGetType> Get<TGetType>()
        {
            return new List<TGetType>();
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public virtual TGetType Get<TGetType>(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Default
        [HttpPost]
        public virtual void Post<TPostType>([FromBody] TPostType model)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Default/5
        [HttpPut("{id}")]
        public virtual void Put<TPutType>(int id, [FromBody] TPutType value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}