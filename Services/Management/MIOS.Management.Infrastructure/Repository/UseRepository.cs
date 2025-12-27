using Common.Models;
using Dapper;
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
    public class UserRepository : IUserRepository
    {

        private IDatabaseConnectionFactory _connectionFactory;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IDatabaseConnectionFactory connectionFactory, ILogger<UserRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public async Task<CodeMessage> SaveUser(UserInfo user)
        {
            try
            {
                _logger.LogInformation("Creating a new user in database: {Email}", user.Email);

                using (IDbConnection connection = _connectionFactory.createConnection(DatabaseConnections.USER))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@name", user.UserName);
                    parameters.Add("@email", user.Email);
                    parameters.Add("@password", user.Password);

                    // Execute stored procedure and get the generated ID
                    int newId = await connection.QuerySingleAsync<int>(
                        "CreateStudent",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    _logger.LogInformation("Successfully created user with ID: {UserId}", newId);

                    // Return the CodeMessage indicating success
                    return new CodeMessage
                    {
                        code = "200",
                        Message = $"User created successfully with ID: {newId}"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user in database");

                // Return a CodeMessage indicating failure instead of crashing the Gateway
                return new CodeMessage
                {
                    code = "500",
                    Message = $"Error: {ex.Message}"
                };
            }
        }

        public async Task<CodeMessage> AddUser(UserInfo user)
        {
            try
            {
                _logger.LogInformation("Creating a new user in database: {Email}", user.Email);

                using (IDbConnection connection = _connectionFactory.createConnection(DatabaseConnections.USER))
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@name", user.UserName);
                    parameters.Add("@email", user.Email);
                    parameters.Add("@password", user.Password);

                    // Execute stored procedure and get the generated ID
                    int newId = await connection.QuerySingleAsync<int>(
                        "CreateStudent",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    _logger.LogInformation("Successfully created user with ID: {UserId}", newId);

                    // Return the CodeMessage indicating success
                    return new CodeMessage
                    {
                        code = "200",
                        Message = $"User created successfully with ID: {newId}"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user in database");

                // Return a CodeMessage indicating failure instead of crashing the Gateway
                return new CodeMessage
                {
                    code = "500",
                    Message = $"Error: {ex.Message}"
                };
            }
        }
    }
}
