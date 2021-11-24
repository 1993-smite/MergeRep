using DB.Repositories.Contact;
using System;
using System.Collections.Generic;
using WebVueTest.DB.Converters;
using WebVueTest.Models;

namespace WebVueTest.DB.Mappers.Contact
{
    public class ContactMapper
    {
        Lazy<ContactRepository> _repository = new Lazy<ContactRepository>(() => new ContactRepository());
        ContactRepository Repository => _repository.Value;


        public ContactMapper()
        {

        }


        public Models.Contact GetContact(ContactFilter filter)
        {
            return ContactConverter.Convert(Repository.Get(filter));
        }

        public List<Models.Contact> GetContacts(ContactFilter filter = default)
        {
            var dbContacts = Repository.GetList(filter);
            var contacts = new List<Models.Contact>();
            foreach(var dbcontact in dbContacts)
            {
                contacts.Add(ContactConverter.Convert(dbcontact));
            }
            return contacts;
        }

        public int SaveContact(Models.Contact contact)
        {
            var dbModel = ContactConverter.Convert(contact);
            dbModel = Repository.SaveTransaction(dbModel);
            return dbModel.Id;
        }
    }
}
