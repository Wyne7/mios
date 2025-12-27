using Common.Models;
using Microsoft.AspNetCore.Mvc;
using MIOS.Management.Application.Interfaces;
using MIOS.Management.Core.Models;
using MIOS.Management.Infrastructure.Repository;
using MOIS.Common.Models;

namespace mios.management.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RoleController : ControllerBase
    {

        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleController> _logger;
        private readonly IResponseHandler _responseHandler;

        public RoleController(IRoleRepository roleRepository, ILogger<RoleController> logger, IResponseHandler responseHandler    )
        {
            _roleRepository = roleRepository;
            _logger = logger;
            _responseHandler = responseHandler;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] RoleInfo info)
        {
            try
            {
                _logger.LogInformation("Create Role Controller Started. " + info.RoleId);
                var data = await _roleRepository.CreateRole(info);
                if (data != null)
                {
                    _logger.LogInformation("Create Role Controller Success. " + info.RoleId);
                    var response = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R201);
                    return Ok(new { code = response.code, message = response.message, data = data });
                }
                else
                {
                    _logger.LogInformation("Create Role Controller Finished. No Data. " + info.RoleId);
                    var res = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R003);
                    return BadRequest(new { code = res.code, message = res.message, data = "" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create Role Controller Process Failed: " + ex.Message);
                var response = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R001);
                return BadRequest(new { code = response.code, message = response.message, description = ex.Message });
            }
        }

    }
}
