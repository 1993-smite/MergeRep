using DB.DBModels;
using PostgresApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB.Repositories.Address
{
    public class AddressRepository: CommonRepository<DBAddress, AddressFilter>
    {
        public override IEnumerable<DBAddress> GetList(AddressFilter filter = default)
        {
            var addresses = new List<DBAddress>();
            using (ApplicationContext db = new ApplicationContext())
            {
                addresses = db.DBAddress
                    .Where(x => x.Address.Contains(filter.Filter))
                    .Take(filter.Count)
                    .ToList();
            }
            return addresses;
        }

        public override DBAddress Save(ApplicationContext db, DBAddress address)
        {
            var exist = db.DBAddress
                            .FirstOrDefault(x => x.Address == address.Address
                                            || (x.Lat == address.Lat && x.Lon == address.Lon));

            if (exist == null)
            {
                db.DBAddress.Add(address);
            }
            else
            {
                db.DBAddress.Remove(exist);
                db.DBAddress.Add(address);
            }

            db.SaveChanges();

            return address;
        }
    }
}
