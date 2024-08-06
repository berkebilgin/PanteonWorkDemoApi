using BusinessLayer.Services;
using CrossCutting.CoreModels;
using EntityLayer.PanteonEntity.MongoEntity;
using Microsoft.AspNetCore.Mvc;
using PanteonWorkDemo.Filters;
using PanteonWorkDemo.Models.Base;


namespace PanteonWorkDemo.Controllers
{
    [TokenAuthenticationFilter]
    [ApiController]
    [Route("Api/[controller]")]
    public class BuildingController : ControllerBase
    {
        private readonly PanteonBuildingService _buildingService;

        public BuildingController()
        {
            _buildingService = new PanteonBuildingService();
        }


        [HttpPut("AddBuilding")]
        public async Task<IActionResult> AddBuilding(PanteonBuilding building)
        {
            var validationResult = await new BuildingValidator().ValidateAsync(building);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResult
                {
                    IsValid = false,
                    ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                });
            }

            OperationResult operationResult = await _buildingService.AddBuildingAsync(building);
            if (!operationResult.IsValid)
            {
                return BadRequest(new ApiResult
                {
                    ErrorMessages = operationResult.Messages
                });
            }
            return Ok(new ApiResult
            {
                IsValid = true,
                SuccessMessages = operationResult.Messages
            }); ;
        }

        [HttpGet("GetBuildingList")]
        public async Task<IActionResult> GetBuildingListByUserId(string userId)
        {
            return Ok(new ApiResult
            {
                IsValid = true,
                Model = await _buildingService.GetBuildingListByUserId(userId)
            });
        }

        [HttpGet("GetBuildingTypeList")]
        public async Task<IActionResult> GetBuildingTypeListByUserId(string userId)
        {
            return Ok(new ApiResult
            {
                IsValid = true,
                Model = await _buildingService.GetBuildingTypeListByUserId(userId)
            });
        }
    }
}