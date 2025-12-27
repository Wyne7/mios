using Common.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MIOS.Management.Application.Interfaces;
using MIOS.Management.Core.Models;
using MOIS.Shared.Core.Helper.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOS.Management.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {

        private IDatabaseConnectionFactory _connectionFactory;
        private ILogger<ProductRepository> _logger;

        public ProductRepository(IDatabaseConnectionFactory connectionFactory, ILogger<ProductRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public async Task<CodeMessage> CreatePoduct(Product product)
        {
            try
            {
                using IDbConnection connection =
                    _connectionFactory.createConnection(DatabaseConnections.USER);

                var parameters = new DynamicParameters();
                parameters.Add("@ProuctCode", product.ProuctCode);
                parameters.Add("@ProductName", product.ProductName);
                parameters.Add("@ProductDescription", product.ProductDescription);
                parameters.Add("@CreatedBy", product.CreatedBy);

                await connection.ExecuteAsync(
                    "spProductCreate",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return new CodeMessage
                {
                    code = "200",
                    Message = "Product created successfully"
                };
            }
            catch (SqlException ex) when (ex.Message.Contains("Product already exists"))
            {
                return new CodeMessage
                {
                    code = "409",
                    Message = "Product already exists"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Product");

                return new CodeMessage
                {
                    code = "500",
                    Message = ex.Message
                };
            }
        }
    }
}
