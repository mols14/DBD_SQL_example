using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using SQLdatabase.models;

namespace SQLdatabase;

public class SqlDbRepo
{
    private readonly string _connectionString = "Server=.;Database=Store;";

    public SqlDbRepo(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IActionResult> GetAllProducts()
    {
        var products = new List<Product>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var query = "SELECT * FROM Products";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var product = new Product
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3)
                        };

                        products.Add(product);
                    }
                }
            }

            connection.Close();
        }

        return new OkObjectResult(products);
    }

    public async Task<IActionResult> GetProductById(int productId)
    {
        Product product = null;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Vulnerable to SQL injection
            var query = $"SELECT * FROM Products WHERE product_id = {productId}";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        product = new Product
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3),
                        };
                    }
                }
            }

            connection.Close();
        }

        if (product == null)
        {
            return new NotFoundResult();
        }
        
        return new OkObjectResult(product);
    }
    
    //USED IN FRONTEND
    public async Task<List<Product>> GetProductsByName(string name)
    {
        var products = new List<Product>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            // Use parameters in the query to avoid SQL injection
            var query = "SELECT * FROM Products WHERE product_name LIKE @name";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", $"%{name}%");
            
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var product = new Product
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3)
                        };
                        products.Add(product);
                    }
                }
            }
            connection.Close();
        }
        return products;
    }
    
    public async Task<List<Product>> GetProductsByNameVulnerable(string name)
    {
        var products = new List<Product>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            // Vulnerable to SQL injection
            var query = $"SELECT * FROM Products WHERE product_name LIKE '%{name}%'";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var product = new Product
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Stock = reader.GetInt32(3)
                        };
                        products.Add(product);
                    }
                }
            }
            connection.Close();
        }
        return products;
    }
}