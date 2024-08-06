using BusinessLayer.BLL;
using CrossCutting.CoreModels;
using EntityLayer.PanteonEntity.MsEntity;
using Microsoft.AspNetCore.Mvc;
using PanteonWorkDemo.Helpers;
using PanteonWorkDemo.Models;
using PanteonWorkDemo.Models.Base;


namespace PanteonWorkDemo.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly PanteonUserService _userService;

        public AuthorizationController()
        {
            /* Büyük ölçekli durumlar için PROGRAM.CS'TE Dependency Injection İle Yapılandırılabilir. */
            _userService = new();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel loginModel)
        {
            var validationResult = await new LoginValidator().ValidateAsync(loginModel);
            
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResult
                {
                    IsValid = false,
                    ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                });
            }

            OperationResult loginResult =  await _userService.LoginAync(loginModel.username, loginModel.password);
            
            ApiResult apiResult = new ApiResult();
            if (!loginResult.IsValid)
            {
                apiResult.IsValid = false;
                apiResult.ErrorMessages = loginResult.Messages;
                return BadRequest(apiResult);
            }
            apiResult.IsValid = true;
            
            PanteonUser user = (PanteonUser)loginResult.Data;
            apiResult.Model = new
            {
                Token = user.Reserved1,
                UserId = user.Id
            };
            return Ok(apiResult);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterModel registerModel)
        {
            var validationResult = await new UserRegisterValidator().ValidateAsync(registerModel);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResult
                {
                    IsValid = false,
                    ErrorMessages = validationResult.Errors.Select(x => x.ErrorMessage).ToList()
                });
            }

            PanteonUser userModel = (PanteonUser)MapperHelper.MapFrom(registerModel, typeof(PanteonUser), typeof(UserRegisterModel));
            
            OperationResult registerResult = await _userService.RegisterAsync(
                userModel);

            ApiResult apiResult = new ApiResult();
            if (!registerResult.IsValid)
            {
                apiResult.IsValid = false;
                apiResult.ErrorMessages = registerResult.Messages;
                return BadRequest(apiResult);
              
            }
            apiResult.IsValid = true;
            apiResult.SuccessMessages = registerResult.Messages;

            return Ok(apiResult);
        }
    }
}