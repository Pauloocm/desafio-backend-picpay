using System.ComponentModel.DataAnnotations;

namespace DesafioBackendPicPay.ViewModels
{
    public abstract class BaseViewModel<T> where T : class
    {
        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(30)]
        public required string FirstName { get; set; }

        [StringLength(50)]
        public required string LastName { get; set; }

        public abstract T ToCommand();
    }
}
