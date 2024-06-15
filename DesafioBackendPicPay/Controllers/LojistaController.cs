using DesafioBackendPicPay.Platform.Application;
using DesafioBackendPicPay.Platform.Application.Lojista.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackendPicPay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LojistaController(IDesafioPicpayAppService appService) : ControllerBase
    {
        private readonly IDesafioPicpayAppService picpayAppService = appService ?? throw new ArgumentNullException(nameof(appService));

        public async Task<IActionResult> Add([FromBody] AddLojistaCommand command, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var id = await picpayAppService.Add(command, cancellationToken);

            return Ok(id);
        }
    }
}
