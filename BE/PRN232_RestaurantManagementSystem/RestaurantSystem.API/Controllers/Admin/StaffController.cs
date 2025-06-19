using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using RestaurantSystem.DTOs;
using RestaurantSystem.Services;

namespace RestaurantSystem.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : BaseController
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [EnableQuery]
        [HttpGet("GetStaffs")]
        public async Task<IActionResult> GetStaffs([FromQuery] StaffQueryDTO query)
        {
            var result = await _staffService.GetStaffs(query);
            return HandleServiceResult(result);
        }

        [HttpPost("CreateStaff")]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaff createStaff)
        {
            if (createStaff == null)
            {
                return BadRequestWrapper<CreateStaff>("Invalid staff data");
            }
            var result = await _staffService.CreateStaff(createStaff);
            return HandleServiceResult(result);
        }

        [HttpPut("UpdateStaff")]
        public async Task<IActionResult> UpdateStaff([FromBody] UpdateStaff updateStaff)
        {
            if (updateStaff == null)
            {
                return BadRequestWrapper<UpdateStaff>("Invalid staff data");
            }
            var result = await _staffService.UpdateStaff(updateStaff);
            return HandleServiceResult(result);
        }

        [HttpDelete("DeleteStaff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            if (id <= 0)
            {
                return BadRequestWrapper<int>("Invalid staff ID");
            }
            var result = await _staffService.DeleteStaff(id);
            return HandleServiceResult(result);
        }
    }
}
