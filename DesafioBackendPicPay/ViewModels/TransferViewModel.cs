using DesafioBackendPicPay.Platform.Application;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackendPicPay.ViewModels
{
    public class TransferViewModel
    {
        [Required]
        [FromRoute]
        public Guid SendById { get; set; }

        [Required]
        [FromBody]
        public Guid ReceivedById { get; set; }

        //[Required]
        [FromBody]
        public decimal Value { get; set; }

        internal TransferCommand ToCommand(Guid sendById, Guid receivedById)
        {
            var command = new TransferCommand()
            {
                SendById = sendById,
                ReceivedById = receivedById,
                Value = Value
            };

            return command;
        }
    }
}
