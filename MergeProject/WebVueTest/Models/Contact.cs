using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.Models
{
    public class Contact
    {
        public enum ContactStatus
        {
            Active=1,
            Delete=9
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public ContactStatus Status { get; set; }
    }
}
