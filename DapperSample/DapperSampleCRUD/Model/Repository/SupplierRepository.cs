using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperSampleCRUD.Model.Repository
{
    public class SupplierRepository
    {
        public string ConnectionString { get; private set; }

        public SupplierRepository(IConfiguration config)
        {
            this.ConnectionString = config.GetConnectionString("DapperSampleConnection") ?? throw new ArgumentNullException();
        }

        public void Save(Supplier model)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute("INSERT INTO SUPPLIER(CompanyName,ContactName, City, Country) VALUES (@CompanyName, @ContactName, @City, @Country)", model);
            }
        }

        public void Update(Supplier model)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute(@"UPDATE SUPPLIER
                                        SET CompanyName = @CompanyName, 
                                            ContactName = @ContactName,
                                            City = @City,
                                            Country = @Country
                                        WHERE ID = @Id", model);
            }
        }

        public void Delete(int supplierId)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute("DELETE FROM SUPPLIER WHERE ID = @SupplierId", new { SupplierId = supplierId });
            }
        }

    }
}
