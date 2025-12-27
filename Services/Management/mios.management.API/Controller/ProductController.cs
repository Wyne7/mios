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
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        private readonly IResponseHandler _responseHandler;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger, IResponseHandler responseHandler)
        {
            _productRepository = productRepository;
            _logger = logger;
            _responseHandler = responseHandler;
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product info)
        {
            try
            {
                _logger.LogInformation("Create Product Controller Started. " + info.ProductName);
                var data = await _productRepository.CreatePoduct(info);
                if (data != null)
                {
                    _logger.LogInformation("Create Product Controller Success. " + info.ProductName);
                    var response = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R201);
                    return Ok(new { code = response.code, message = response.message, data = data });
                }
                else
                {
                    _logger.LogInformation("Create Product Controller Finished. No Data. " + info.ProductName);
                    var res = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R003);
                    return BadRequest(new { code = res.code, message = res.message, data = "" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create Product Controller Process Failed: " + ex.Message);
                var response = _responseHandler.GetResponse<ResponseData>(ResponseEnum.R001);
                return BadRequest(new { code = response.code, message = response.message, description = ex.Message });
            }
        }

    }
}
