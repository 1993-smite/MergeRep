using DB.Repositories;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVueTest.DB.Converters;
using WebVueTest.Models;

namespace WebVueTest.DB.Mappers
{
    public static class ContactMapper
    {
        public static Contact GetContact(int Id)
        {
            return ContactConverter.Convert(ContactRepository.GetContact(Id));
        }

        public static List<Contact> GetContacts()
        {
            var dbContacts = ContactRepository.GetContacts();
            var contacts = new List<Contact>();
            foreach(var dbcontact in dbContacts)
            {
                contacts.Add(ContactConverter.Convert(dbcontact));
            }
            return contacts;
        }

        public static int SaveContact(Contact contact)
        {
            return ContactRepository.SaveContact(ContactConverter.Convert(contact));
        }
    }
}
