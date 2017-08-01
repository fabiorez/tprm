using System;
using System.Linq;
using TPRM.Shared.Entities;

namespace TPRM.Domain.Entities
{
    public class Empresa : Entity
    {
        public Empresa(string empresaNome, string empresaTipo, decimal valorDevido, Guid transacaoId, Guid servicoId)
        {
            EmpresaNome = empresaNome;
            EmpresaTipo = empresaTipo;
            Ativo = true;
            ValorDevido = valorDevido;
            TransacaoId = transacaoId;
            ServicoId = servicoId;
            DataCadastro = DateTime.Now;

            //Validações
        }

        public string EmpresaNome { get; private set; }
        public string EmpresaTipo { get; private set; }
        public bool Ativo { get; private set; }
        public decimal ValorDevido { get; private set; }
        public Guid TransacaoId { get; private set; }
        public Guid ServicoId { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public void AtivarEmpresa() => Ativo = true;

        public void DesativarEmpresa() => Ativo = false;

        public string BuscaNomeEmpresa(Guid Id)
        {
            return null;
        }
    }
}
