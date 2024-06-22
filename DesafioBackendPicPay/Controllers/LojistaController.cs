using DesafioBackendPicPay.Platform.Application;
using DesafioBackendPicPay.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackendPicPay.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LojistaController(IDesafioPicpayAppService appService) : ControllerBase
    {
        private readonly IDesafioPicpayAppService picpayAppService = appService ?? throw new ArgumentNullException(nameof(appService));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddLojistaViewModel viewModel, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

            var id = await picpayAppService.Add(viewModel.ToCommand(), cancellationToken);

            return Ok(id);
        }
    }
}
