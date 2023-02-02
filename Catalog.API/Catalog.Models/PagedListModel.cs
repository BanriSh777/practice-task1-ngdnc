namespace Catalog.Models
{
    public class PagedListModel<T>
    {
        public List<T> Data { get; set; }
        public PageModel Page { get; set; }
        public PagedListModel(List<T> data, PageModel pageModel)
        {
            Page = pageModel;
            Data = data;
        }
    }
}
