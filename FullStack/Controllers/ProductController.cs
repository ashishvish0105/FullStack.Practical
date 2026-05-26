using FullStack.Core.Comman;
using FullStack.Core.Entity;
using FullStack.Core.Iterface;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository productRepository;

    public ProductController(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<ApiCommanModel>> GetAll()
    {
        var products = await productRepository.getProductList();

        if (products == null || !products.Any())
        {
            return NotFound(new ApiCommanModel
            {
                message = "No products found",
                statusCode = StatusCodes.Status404NotFound
            });
        }

        return Ok(new ApiCommanModel
        {
            payload = products,
            message = "Success",
            statusCode = StatusCodes.Status200OK
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiCommanModel>> GetById(int id)
    {
        var product = await productRepository.getProductById(id);

        if (product == null)
            return NotFound(new ApiCommanModel
            {
                message = "Product not found",
                statusCode = StatusCodes.Status404NotFound
            });

        return Ok(new ApiCommanModel
        {
            payload = product,
            message = "Success",
            statusCode = StatusCodes.Status200OK
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiCommanModel>> Create(ProductModel product)
    {
        var result = await productRepository.addEditProduct(product);

        return Ok(new ApiCommanModel
        {
            payload = product,
            message = "Created successfully",
            statusCode = StatusCodes.Status201Created
        });
    }

    [HttpGet("GetProducts")]
    public async Task<ActionResult<ApiCommanModel>> GetProducts(
      int pageNumber = 1,
      int pageSize = 10,
      string? search = null)
    {
        var result = await productRepository.GetProducts(pageNumber, pageSize, search);
        return Ok(new ApiCommanModel
        {
            payload = result,
            message = "Success",
            statusCode = StatusCodes.Status200OK
        });
    }
}