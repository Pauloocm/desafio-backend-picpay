using DesafioBackendPicPay.Platform.Application;
using DesafioBackendPicPay.Platform.Application.User.Commands;
using DesafioBackendPicPay.ViewModels;
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

        [HttpPost("Transfer/{SendById:guid}/{ReceivedById:guid}")]
        public async Task<IActionResult> Transfer([FromRoute] Guid sendById, [FromRoute] Guid receivedById,
            [FromBody] TransferViewModel viewModel, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

            await picpayAppService.Transfer(viewModel.ToCommand(sendById, receivedById), cancellationToken);

            return Ok();
        }
    }
}
