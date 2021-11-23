using DB.DBModels;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.Repositories
{
    public static class ContactRepository
    {
        #region Get
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public static DBContact GetContact(int Id)
        {
            var contact = new DBContact();
            using (ApplicationContext db = new ApplicationContext())
            {
                contact = db
                    .Contacts
                    .Where(x=>x.Id == Id)
                    .FirstOrDefault();
            }
            return contact;
        }

        /// <summary>
        /// get lists
        /// </summary>
        /// <returns></returns>
        public static List<DBContact> GetContacts()
        {
            var contacts = new List<DBContact>();
            using (ApplicationContext db = new ApplicationContext())
            {
                contacts = db
                    .Contacts
                    .Where(x=>x.Status == 1)
                    .ToList();
            }
            return contacts;
        }
        #endregion

        /// <summary>
        /// save contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static int SaveContact(DBContact contact)
        {
            int contactId = contact.Id;
            using (ApplicationContext db = new ApplicationContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        DBContact cnt;

                        if (contactId < 1)
                        {
                            contactId = db
                                .Contacts
                                .OrderBy(x => x.Id)
                                .LastOrDefault()?.Id ?? 1;
                            contactId++;
                            db.Contacts.Add(contact);
                        }
                        else
                        {
                            cnt = db
                                .Contacts
                                .FirstOrDefault(x => x.Id == contactId);
                            if (cnt == null)
                                throw new NullReferenceException($"Нет записи user с таким Id = {contactId}");
                            db.Entry(cnt).CurrentValues.SetValues(contact);
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return contactId;
        }
    }
}
