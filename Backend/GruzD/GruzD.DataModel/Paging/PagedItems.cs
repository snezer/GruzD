namespace GruzD.DataModel.Paging
{
    public class PagedItems<T>
    {
        public int Total { get; set; }
        public T[] Items { get; set; }
        public string ErrMess { get; set; }
    }
}
