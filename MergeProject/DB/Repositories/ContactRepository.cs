using Microsoft.EntityFrameworkCore;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB.Users
{
    public static class ContactRepository
    {
        #region GetUser
        public static DBContact GetContact(int Id)
        {
            var contact = new DBContact();
            using (ApplicationContext db = new ApplicationContext())
            {
                contact = db.Contacts.Where(x=>x.Id == Id).FirstOrDefault();
            }
            return contact;
        }

        public static List<DBContact> GetContacts()
        {
            var contacts = new List<DBContact>();
            using (ApplicationContext db = new ApplicationContext())
            {
                contacts = db.Contacts.Where(x=>x.Status == 1).ToList();
            }
            return contacts;
        }
        #endregion
        public static int SaveContact(DBContact contact)
        {
            int contactId = contact.Id;
            using (ApplicationContext db = new ApplicationContext())
            {
                DBContact cnt;

                if (contactId < 1)
                {
                    contactId = db.Contacts.OrderBy(x => x.Id).LastOrDefault()?.Id ?? 1;
                    contactId++;
                    db.Contacts.Add(contact);
                }
                else
                {
                    cnt = db.Contacts.FirstOrDefault(x => x.Id == contactId);
                    if (cnt == null)
                        throw new Exception($"Нет записи user с таким Id = {contactId}");
                    db.Entry(cnt).CurrentValues.SetValues(contact);
                    //db.Update(usr);
                }

                db.SaveChanges();
            }
            return contactId;
        }
    }
}
