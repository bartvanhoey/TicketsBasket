using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketsBasket.Services;
using TicketsBasket.Shared.Dtos;
using TicketsBasket.Shared.Requests;
using TicketsBasket.Shared.Responses;

namespace TicketsBasket.Api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class UserProfilesController : ControllerBase
  {
    private readonly IUserProfilesService _userProfilesService;

    public UserProfilesController(IUserProfilesService userProfilesService)
    {
      _userProfilesService = userProfilesService;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(OperationResponse<UserProfileDto>))]
    [ProducesResponseType(400, Type = typeof(OperationResponse<UserProfileDto>))]
    public async Task<IActionResult> Get()
    {
      var operationResponse = await _userProfilesService.GetUserProfileByUserIdAsync();
      if (operationResponse.IsSuccess) return Ok(operationResponse);
      return NotFound();
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(OperationResponse<UserProfileDto>))]
    [ProducesResponseType(400, Type = typeof(OperationResponse<UserProfileDto>))]
    public async Task<IActionResult> Post([FromForm] CreateUserProfileRequest model)
    {
      var operationResponse = await _userProfilesService.CreateUserProfileAsync(model);
      if (operationResponse.IsSuccess) return Ok(operationResponse);
      return BadRequest(operationResponse);
    }
  }

}