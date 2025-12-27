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
    public class RoleRepository : IRoleRepository
    {
        private IDatabaseConnectionFactory _connectionFactory;
        private ILogger<RoleRepository> _logger;

        public RoleRepository(IDatabaseConnectionFactory connectionFactory, ILogger<RoleRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<CodeMessage> CreateRole(RoleInfo role)
        {
            try
            {
                using IDbConnection connection =
                    _connectionFactory.createConnection(DatabaseConnections.USER);

                var parameters = new DynamicParameters();
                parameters.Add("@RoleCode", role.RoleCode);
                parameters.Add("@RoleName", role.RoleName);
                parameters.Add("@CreatedBy", role.CreatedBy);

                await connection.ExecuteAsync(
                    "spRoleCreate",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return new CodeMessage
                {
                    code = "200",
                    Message = "Role created successfully"
                };
            }
            catch (SqlException ex) when (ex.Message.Contains("RoleCode already exists"))
            {
                return new CodeMessage
                {
                    code = "409",
                    Message = "RoleCode already exists"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role");

                return new CodeMessage
                {
                    code = "500",
                    Message = ex.Message
                };
            }
        }

    }
}
