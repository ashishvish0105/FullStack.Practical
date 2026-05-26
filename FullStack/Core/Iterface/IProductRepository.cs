using FullStack.Core.Entity;

namespace FullStack.Core.Iterface
{
    public interface IProductRepository
    {
        public Task<List<ProductModel>> getProductList();
        public Task<int> addEditProduct(ProductModel productObj);
        public Task<ProductModel> getProductById(int productId);

        public Task<PaginationResponse<ProductModel>> GetProducts(
int pageNumber = 1,
int pageSize = 10,
string? search = null);
    }
}
