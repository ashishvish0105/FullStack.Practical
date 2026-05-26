using FullStack.Models;

namespace FullStack.Repository.Product
{
    public interface IProductRepository
    {
        public Task<List<ProductModel>> getProductList();
        public Task<int> addEditProduct(ProductModel productObj);
        public Task<ProductModel> getProductById(int productId);
    }
}
