using FullStack.Core.Comman;
using FullStack.Core.Entity;
using FullStack.Core.Iterface;
using FullStack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

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

    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts(
      int pageNumber = 1,
      int pageSize = 10,
      string? search = null)
    {
        var result = await productRepository.GetProducts(pageNumber, pageSize, search);
        return Ok(new ApiCommanModel
        {
            payload = result,
            messgae = "Success",
            statusCode = StatusCodes.Status200OK
        });
    }
}