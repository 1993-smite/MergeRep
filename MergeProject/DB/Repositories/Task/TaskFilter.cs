namespace DB.Repositories.Task
{
    public class TaskFilter: Filter
    {
        public enum State
        {
            Active=1,
            Dalete=9
        }

        public int Id;
    }
}
