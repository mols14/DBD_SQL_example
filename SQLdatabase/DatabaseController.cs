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
    [Route("getProductById")]
    public async Task<IActionResult> GetProductById(int productId)
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
    
    //USED IN FRONTEND
    [HttpGet]
    [Route("GetProducts")]
    public async Task<IActionResult> GetProductsByName([FromQuery] string search = "")
    {
        try
        {
            var products = await _sqlDbRepo.GetProductsByName(search);
            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    [Route("GetProductsVulnerable")]
    public async Task<IActionResult> GetProductsByNameVulnerable([FromQuery] string search = "")
    {
        try
        {
            var products = await _sqlDbRepo.GetProductsByNameVulnerable(search);
            return Ok(products);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}