using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApp1.Models;

namespace WebApp1.Services
{
    public class ProductService
    {
        private string _dataSource = "dbserverharshit.database.windows.net";
        private string _database = "DBHarshit";
        private string _userId = "harshit";
        private string _password = "India_2020";

        private string GetSqlConnectionString()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = _dataSource;
            connectionStringBuilder.InitialCatalog = _database;
            connectionStringBuilder.UserID = _userId;
            connectionStringBuilder.Password = _password;
            connectionStringBuilder.ConnectTimeout = 300;
            return connectionStringBuilder.ConnectionString;
        }

        public List<Product> GetProducts()
        {
            var conn = new SqlConnection(GetSqlConnectionString());
            conn.Open();

            List<Product> products = new List<Product>();
            string query = "select * from Products";
            SqlCommand cmd = new SqlCommand(query, conn);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var product = new Product()
                    {
                        ProductId = dr.GetInt32(0),
                        ProductName = dr.GetString(1),
                        Quantity = dr.GetInt32(2)
                    };
                    products.Add(product);
                }
            }

            conn.Close();
            return products;
        }
    }
}
