namespace API_Library.Models
{
    public class DataSearch
    {
            public int page { get; set; }
            public int pageSize { get; set; }
            public long totalItem { get; set; }
            public dynamic data { get; set; }
    }
}
