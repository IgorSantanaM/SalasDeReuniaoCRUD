using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalasDeReuniaoCRUD.Application.Features.Reservas.Dtos
{
    public record ReservaDto(
        int Id,
        string Titulo,  
        string Responsavel,
        DateTime DataInicio,
        DateTime DataFim,
        int ParticipantesPrevistos,
        decimal ValorHora,
        decimal Desconto,
        decimal ValorTotal);
}
