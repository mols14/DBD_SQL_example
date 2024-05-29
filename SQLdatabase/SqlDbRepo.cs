using System.Data.SqlClient;

namespace SQLdatabase;

public class SqlDbRepo
{
    private readonly string _connectionString = "Server=.;Database=Store;";

    public SqlDbRepo(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void GetAllProducts(string userInput)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            //The danger takes place right here
            var query = $"SELECT * FROM Products WHERE product_name = '{userInput}'";

            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var productId = reader.GetInt32(0);
                        var productName = reader.GetString(1);
                        var productPrice = reader.GetDecimal(2);
                        var stock = reader.GetInt32(3);
                        
                        Console.WriteLine($"Product ID: {productId}, Product Name: {productName}, Product Price: {productPrice}, Stock: {stock}");
                    }
                }
            }

            connection.Close();
        }
    }
}