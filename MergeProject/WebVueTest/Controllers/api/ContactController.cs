using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebVueTest.DB.Mappers;
using WebVueTest.Models;

namespace WebVueTest.Controllers.api
{
    public class ContactController : AppController<Contact>
    {
        // GET: api/Contact
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return ContactMapper.GetContacts();
        }

        // GET: api/Contact/5
        /*[HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/Contact
        [HttpPost]
        public int Post([FromBody] Contact model)
        {
            if (!ModelState.IsValid)
                throw new Exception("Not valid");
            model.Id = ContactMapper.SaveContact(model);
            return model.Id;
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Contact model)
        {
            if (!ModelState.IsValid)
                throw new Exception("Not valid");
            ContactMapper.SaveContact(model);
        }

        // DELETE: api/ApiWithActions/5
        /*[HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
