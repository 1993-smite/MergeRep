using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebVueTest.Models;

namespace WebVueTest.Controllers.api
{
    public class UserController : AppController<User>
    {
        // GET: api/User
        public IEnumerable<UserViewValidate> Get()
        {
            var users = FactoryUserView.GetUsers();
            var list = new List<UserViewValidate>();
            foreach (var user in users)
            {
                list.Add(FactoryUserView.Convert<User, UserViewValidate>(user));
            }
            return list;
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public UserViewValidate Get(int id)
        {
            var model = FactoryUserView.Convert<User, UserViewValidate>(FactoryUserView.GetUser(id));
            return model;
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] UserViewValidate model)
        {
            if (!ModelState.IsValid)
                return;
            FactoryUserView.SaveUser(model);
            
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserViewValidate model)
        {
            if (!ModelState.IsValid)
                return;
            model.Id = id;
            FactoryUserView.SaveUser(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public override void Delete(int id)
        {
        }
    }
}
