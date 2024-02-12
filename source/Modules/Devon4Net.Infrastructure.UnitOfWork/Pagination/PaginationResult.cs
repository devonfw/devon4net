namespace Devon4Net.Infrastructure.UnitOfWork.Pagination
{
    public class PaginationResult<T> : PaginationBase where T : class
    {
        public IList<T> Results { get; set; }

        public PaginationResult()
        {
            Results = new List<T>();
        }
    }
}
