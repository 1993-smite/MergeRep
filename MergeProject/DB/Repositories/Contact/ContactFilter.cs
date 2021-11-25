namespace DB.Repositories.Contact
{
    public class ContactFilter: Filter
    {
        public enum Status
        {
            Active=1,
            Delete=8
        }

        public int Id;

        public ContactFilter(int id)
        {
            Id = id;
        }
    }
}
