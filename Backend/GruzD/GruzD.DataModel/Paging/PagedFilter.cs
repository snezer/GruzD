namespace GruzD.DataModel.Paging
{
    public class PagedFilter
    {
        public PropertyFilter[] Filters { get; set; } = new PropertyFilter[0];
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }

    public class PropertyFilter
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public FilterType Type { get; set; }
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }

    public enum FilterType
    {
        Date = 1
    }

    public static class PagedFilterExtensions
    {
        public static bool IsEmpty(this PagedFilter pagedFilter)
        {
            return pagedFilter == null || (pagedFilter.ItemsPerPage == 0 && pagedFilter.Page == 0);
        }
    }
}
