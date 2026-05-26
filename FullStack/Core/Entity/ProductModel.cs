namespace FullStack.Core.Entity
{
    public class ProductModel
    {
        public int productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string categories { get; set; }
        public double price { get; set; }
    }

    public class PaginationResponse<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
