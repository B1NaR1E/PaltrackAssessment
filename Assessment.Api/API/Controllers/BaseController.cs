using Assessment.Api.WebApi;

namespace Assessment.Api.API.Controllers;

public abstract class BaseController :  ControllerBase
{
    protected IActionResult BadRequestActionResult(string resultErrors) 
        => BadRequest(new ApiResponse<IActionResult>
        {
            Success = false,
            Message = resultErrors
        });
}