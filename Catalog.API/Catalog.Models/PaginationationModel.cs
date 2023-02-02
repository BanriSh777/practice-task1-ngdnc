namespace Catalog.Models
{
    public class PaginationationModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool IsDesc { get; set; }
        public PaginationationModel(int pageNumber, int pageSize, string sortBy, bool isDesc = false)
        {
            PageNumber= pageNumber;
            PageSize= pageSize;
            SortBy= sortBy;
            IsDesc= isDesc;
        }

    }
}
