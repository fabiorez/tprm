using System;
using System.Collections.Generic;

namespace TPRM.Domain.Entities
{
    public class Servico
    {
        public int ServicoId { get; set; }
        public string DescricaoServico { get; set; }
        public decimal ValorServico { get; set; }
        public DateTime DataCadastro { get; set; }
        public Boolean Ativo { get; set; }

        public virtual IEnumerable<Transacao> Transacoes { get; set; }

    }
}