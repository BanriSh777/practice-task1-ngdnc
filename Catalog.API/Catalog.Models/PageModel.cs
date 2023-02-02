namespace Catalog.Models
{
    public class PageModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int RecordsInPage { get; set; }
        public int TotalRecords { get; set; }
        public string SortedBy { get; set; }
        public bool IsDesc { get; set; }
        public PageModel(string sortedBy)
        {
            SortedBy = sortedBy;
        }
    }
}
