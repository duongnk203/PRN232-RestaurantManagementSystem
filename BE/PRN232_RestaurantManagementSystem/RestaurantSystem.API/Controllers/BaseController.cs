using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.DTOs;

namespace RestaurantSystem.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult OkWrapper<T>(T data, string message = "Success")
        {
            var response = ResponseModel<T>.Success(data, message);
            return Ok(response);
        }

        protected IActionResult BadRequestWrapper<T>(string message, List<string> errors = null)
        {
            var response = ResponseModel<T>.Fail(message, errors);
            return BadRequest(response);
        }

        protected IActionResult NotFoundWrapper<T>(string message = "Resource not found")
        {
            var response = ResponseModel<T>.Fail(message);
            return NotFound(response);
        }

        protected IActionResult ServerErrorWrapper<T>(string message = "Internal server error", List<string> errors = null)
        {
            var response = ResponseModel<T>.Fail(message, errors);
            return StatusCode(500, response);
        }

        protected IActionResult HandleServiceResult<T>(ServiceResult<T> result)
        {
            var response = new ResponseModel<T>
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                Data = result.Data,
                Errors = result.Errors
            };

            return StatusCode((int)result.StatusCode, response);
        }
    }
}