using FlowardApp.Services.EmailService.Models;
using FlowardApp.Shared.ControllerBases;
using FlowardApp.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowardApp.Services.EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : CustomControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(EmailDto emailDto)
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
