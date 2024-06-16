using DesafioBackendPicPay.Platform.Application;
using DesafioBackendPicPay.Platform.Application.User.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackendPicPay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IDesafioPicpayAppService appService) : ControllerBase
    {
        private readonly IDesafioPicpayAppService picpayAppService = appService ?? throw new ArgumentNullException(nameof(appService));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserCommand command, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var id = await picpayAppService.AddUser(command, cancellationToken);

            return Ok(id);
        }
    }
}
