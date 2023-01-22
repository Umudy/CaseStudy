using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TemplateCore.Api.Infrastructure
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class BaseApiController : ControllerBase
    {
    }
}
