using FullStack.Models;
using FullStack.Repository.Product;
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
    public async Task<IActionResult> GetAll()
    {
        var products = await productRepository.getProductList();

        return Ok(new ApiCommanModel
        {
            payload = products,
            messgae = "Success",
            statusCode = StatusCodes.Status200OK
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await productRepository.getProductById(id);

        if (product == null)
            return NotFound(new ApiCommanModel
            {
                messgae = "Product not found",
                statusCode = StatusCodes.Status404NotFound
            });

        return Ok(new ApiCommanModel
        {
            payload = product,
            messgae = "Success",
            statusCode = StatusCodes.Status200OK
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductModel product)
    {
        var result = await productRepository.addEditProduct(product);

        return Ok(new ApiCommanModel
        {
            payload = product,
            messgae = "Created successfully",
            statusCode = StatusCodes.Status201Created
        });
    }
}