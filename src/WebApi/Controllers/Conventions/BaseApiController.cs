using MediatR;

namespace OpsManagerAPI.WebApi.Controllers.Conventions;
[ApiController]
public class BaseApiController : ControllerBase
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}