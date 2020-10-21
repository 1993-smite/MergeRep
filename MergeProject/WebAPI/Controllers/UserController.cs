using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        // GET api/values/5
        /// <summary>
        /// get for user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(int id)
        {
            var usr = new User(id);

            return usr;
        }

        // POST api/values
        /// <summary>
        /// пост для юзера
        /// </summary>
        /// <param name="user"></param>
        public void Post([FromBody]User user)
        {

        }

    }
}
