using System;
using System.Collections.Generic;

namespace TPRM.Domain.Entities
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public string EmpresaTipo { get; set; }
        public bool Ativo { get; set; }
        public decimal ValorDevido { get; set; }
        public int TransacaoId { get; set; }
        public int ServicoId { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual Servico Servico { get; set; }

        public virtual IEnumerable<Transacao> Transacoes { get; set; }
    }
}
