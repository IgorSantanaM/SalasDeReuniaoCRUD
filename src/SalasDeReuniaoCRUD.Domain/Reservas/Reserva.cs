using SalasDeReuniaoCRUD.Domain.Core.Exceptions;
using SalasDeReuniaoCRUD.Domain.Core.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalasDeReuniaoCRUD.Domain.Reservas
{
    public class Reserva : Entity<int>, IAggregateRoot
    {
        public string Titulo { get; set; } = string.Empty;
        public string Responsavel { get; set; } = string.Empty;

        [Column(TypeName = "timestamp without time zone")]
        public DateTime DataInicio { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime DataFim { get; set; }
        public int ParticipantesPrevistos { get; set; }
        public decimal ValorHora { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        private Reserva() { }

        public Reserva(string titulo, string responsavel, DateTime dataInicio, DateTime dataFim, int participantesPrevistos, decimal valorHora, decimal desconto)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new DomainException("O título é obrigatório.");

            if (string.IsNullOrWhiteSpace(responsavel))
                throw new DomainException("O nome do responsável é obrigatório.");

            if (dataInicio < DateTime.Now)
                throw new DomainException("A data de início não pode ser no passado.");

            if (dataFim <= dataInicio)
                throw new DomainException("A data de fim deve ser posterior à data de início.");

            if (participantesPrevistos <= 0)
                throw new DomainException("O número de participantes previstos deve ser maior que zero.");

            if (valorHora < 0)
                throw new DomainException("O valor por hora não pode ser negativo.");

            if (desconto < 0)
                throw new DomainException("O valor do desconto não pode ser negativo.");

            if(desconto > 30)
                throw new DomainException("O desconto não pode ser maior que 30%.");

            Titulo = titulo;
            Responsavel = responsavel;
            DataInicio = dataInicio;
            DataFim = dataFim;
            ParticipantesPrevistos = participantesPrevistos;
            ValorHora = valorHora;
            Desconto = desconto;

            ValorTotal = CalcularValorTotal();
        }

        public void Update(string titulo, string responsavel, DateTime dataInicio, DateTime dataFim, int participantesPrevistos, decimal valorHora, decimal desconto)
        {
            if (DateTime.Now > DataInicio)
                throw new DomainException("Não é possível atualizar reservas passadas.");
            if (string.IsNullOrWhiteSpace(titulo))
                throw new DomainException("O título é obrigatório.");
            if (string.IsNullOrWhiteSpace(responsavel))
                throw new DomainException("O nome do responsável é obrigatório.");
            if (dataInicio < DateTime.Now)
                throw new DomainException("A data de início não pode ser no passado.");
            if (dataFim <= dataInicio)
                throw new DomainException("A data de fim deve ser posterior à data de início.");
            if (participantesPrevistos <= 0)
                throw new DomainException("O número de participantes previstos deve ser maior que zero.");
            if (valorHora < 0)
                throw new DomainException("O valor por hora não pode ser negativo.");
            if (desconto < 0 || desconto > 30)
                throw new DomainException("O valor do desconto deve estar entre 0% e 30%.");
            Titulo = titulo;
            Responsavel = responsavel;
            DataInicio = dataInicio;
            DataFim = dataFim;
            ParticipantesPrevistos = participantesPrevistos;
            ValorHora = valorHora;
            Desconto = desconto;
            ValorTotal = CalcularValorTotal();
        }

        public void AplicarDesconto(decimal percentualDesconto)
        {
            if (percentualDesconto < 0 || percentualDesconto > 30)
                throw new DomainException("O percentual de desconto deve estar entre 0% e 30%.");
            Desconto = percentualDesconto;
            ValorTotal = CalcularValorTotal();
        }

        private decimal CalcularValorTotal()
        {
            if (Desconto < 0 || Desconto > 30)
                throw new DomainException("O percentual de desconto deve estar entre 0 e 30.");

            TimeSpan duracao = DataFim - DataInicio;
            decimal totalHoras = (decimal)duracao.TotalHours;
            decimal subTotal = totalHoras * ValorHora;

            decimal valorDoDesconto = subTotal * (Desconto / 100);
            decimal valorTotal = subTotal - valorDoDesconto;

            return valorTotal;
        }
    }
}
