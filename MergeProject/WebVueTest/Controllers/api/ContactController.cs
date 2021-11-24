using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Repositories.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebVueTest.DB.Mappers.Contact;
using WebVueTest.Models;

namespace WebVueTest.Controllers.api
{
    public class ContactController : AppController<Contact>
    {
        Lazy<ContactMapper> _lazyMapper = new Lazy<ContactMapper>(()=>new ContactMapper());
        ContactMapper Mapper => _lazyMapper.Value;

        // GET: api/Contact
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return Mapper.GetContacts();
        }

        // GET: api/Contact/5
        /*[HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST: api/Contact
        [HttpPost]
        public int Post([FromBody] ContactValidate model)
        {
            if (!ModelState.IsValid)
                throw new Exception("Not valid");
            model.Id = Mapper.SaveContact(model);
            return model.Id;
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ContactValidate model)
        {
            if (!ModelState.IsValid)
                throw new Exception("Not valid");
            Mapper.SaveContact(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public override void Delete(int id)
        {
            if (!ModelState.IsValid)
                throw new Exception("Not valid");
            var contact = Mapper.GetContact(new ContactFilter(id));
            if (contact == null || contact.Id != id)
                throw new ArgumentException($"Not contact with id equals {id}");
            contact.Status = Contact.ContactStatus.Delete;
            Mapper.SaveContact(contact);
        }
    }
}
