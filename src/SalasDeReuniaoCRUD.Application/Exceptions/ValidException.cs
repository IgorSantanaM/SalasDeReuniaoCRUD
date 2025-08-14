using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Application.Exceptions
{
    public class ValidException : Exception
    {
        public ValidException(string message) : base($"Validação falhou:{message}")
        {
        }
    }
}
