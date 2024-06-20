using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackendPicPay.Domain.Lojista.Exceptions
{
    public class InvalidOperationException() : Exception("Lojistas cannot make transfers")
    {
    }
}
