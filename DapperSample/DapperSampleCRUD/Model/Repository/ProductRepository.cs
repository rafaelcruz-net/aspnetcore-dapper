using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSampleCRUD.Model.Repository
{
    public class ProductRepository
    {
        public string ConnectionString { get; private set; }

        public ProductRepository(IConfiguration config)
        {
            this.ConnectionString = config.GetConnectionString("DapperSampleConnection") ?? throw new ArgumentNullException();
        }

        public List<Product> GetProducts()
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                var result = sqlConnection.Query<Product>("Select * from Product");

                return result.ToList();
            }
        }

        public Product GetProductById(int productId)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<Product>("Select * from Product where Id = @Id", new { Id = productId }).SingleOrDefault();
            }

        }

        public List<Product> GetProductBySupplier(int supplierId)
        {
            var query = @" Select * from Product
                           join Supplier on Product.SupplierId = Supplier.Id
                           where SupplierId = @SupplierId; ";


            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                var result = sqlConnection.Query<Product, Supplier, Product>(query, map: (p, s) =>
                {
                    p.Supplier = s;
                    p.SupplierID = s.Id;
                    return p;

                }, splitOn: "Id,SupplierId", param: new { SupplierId = supplierId });

                return result.ToList();
            }



        }



    }
}
