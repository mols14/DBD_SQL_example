using Microsoft.AspNetCore.Mvc;

namespace SQLdatabase;

[ApiController]
[Route("api/[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly SqlDbRepo _sqlDbRepo;
    public DatabaseController(SqlDbRepo sqlDbRepo)
    {
        _sqlDbRepo = sqlDbRepo;
    }
    
    [HttpGet]
    [Route("getAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            var products = await _sqlDbRepo.GetAllProducts();
            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("getProduct")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        try
        {
            var product = await _sqlDbRepo.GetProductById(productId);
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    
}