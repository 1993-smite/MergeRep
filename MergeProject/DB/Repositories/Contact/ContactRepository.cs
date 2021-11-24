using DB.DBModels;
using Microsoft.EntityFrameworkCore;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.Repositories.Contact
{
    public class ContactRepository: CommonRepository<DBContact, ContactFilter>
    {
        public override IEnumerable<DBContact> GetList(ContactFilter filter)
        {
            var contacts = new List<DBContact>();
            using (ApplicationContext db = new ApplicationContext())
            {
                contacts = db
                    .Contacts
                    .Where(x => x.Status == (int)ContactFilter.Status.Active)
                    .ToList();
            }
            return contacts;
        }

        public override DBContact Get(ContactFilter filter)
        {
            var contact = new DBContact();
            using (ApplicationContext db = new ApplicationContext())
            {
                contact = db
                    .Contacts
                    .Where(x => x.Id == filter.Id)
                    .FirstOrDefault();
            }
            return contact;
        }

        public override DBContact Save(ApplicationContext db, DBContact contact)
        {
            DBContact cnt;

            if (contact.Id < 1)
            {
                contact.Id = db
                    .Contacts
                    .OrderBy(x => x.Id)
                    .LastOrDefault()?.Id ?? 0;
                contact.Id++;
                cnt = contact;
                db.Contacts.Add(contact);
            }
            else
            {
                cnt = db
                    .Contacts
                    .FirstOrDefault(x => x.Id == contact.Id);
                if (cnt == null)
                    throw new NullReferenceException($"Нет записи user с таким Id = {contact.Id}");
                db.Entry(cnt).CurrentValues.SetValues(contact);
                db.Entry(cnt).State = EntityState.Modified;
            }

            db.SaveChanges();

            return cnt;
        }
    }
}
