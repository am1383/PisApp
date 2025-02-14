using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PisApp.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1")]
    public class CompatibleController : ControllerBase
    {
        
    }
}