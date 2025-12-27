using Common.Models;
using Microsoft.AspNetCore.Mvc;
using MIOS.Management.Application.Interfaces;
using MIOS.Management.Core.Models;
using MOIS.Common.Models;

namespace mios.management.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly IResponseHandler _responseHandler;

        public UserController(IUserRepository userRepository, IResponseHandler responseHandler, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _responseHandler = responseHandler;
            _logger = logger;
        }


        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUser([FromForm] UserInfo info)
        {
            try
            {
                _logger.LogInformation("Save Student Controller Started. " + info.UserID);
                var data = await _userRepository.SaveUser(info);
                if (data != null)
                {
                    _logger.LogInformation("Save Student Controller Success. " + info.UserID);
                    var response = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R201);
                    return Ok(new { code = response.code, message = response.message, data = data });
                }
                else
                {
                    _logger.LogInformation("Save Student Controller Finished. No Data. " + info.UserID);
                    var res = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R003);
                    return Ok(new { code = res.code, message = res.message, data = "" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Student Controller Process Failed: " + ex.Message);
                var response = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R001);
                return BadRequest(new { code = response.code, message = response.message, description = ex.Message });
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromForm] UserInfo info)
        {
            try
            {
                _logger.LogInformation("Save Student Controller Started. " + info.UserID);
                var data = await _userRepository.SaveUser(info);
                if (data != null)
                {
                    _logger.LogInformation("Save Student Controller Success. " + info.UserID);
                    var response = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R201);
                    return Ok(new { code = response.code, message = response.message, data = data });
                }
                else
                {
                    _logger.LogInformation("Save Student Controller Finished. No Data. " + info.UserID);
                    var res = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R003);
                    return Ok(new { code = res.code, message = res.message, data = "" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Student Controller Process Failed: " + ex.Message);
                var response = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R001);
                return BadRequest(new { code = response.code, message = response.message, description = ex.Message });
            }
        }



    }
}
