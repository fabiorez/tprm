using System;

namespace TPRM.Domain.Entities
{
    public class Transacao
    {
        public int TransacaoId { get; set; }
        public string Status { get; set; }
        public string Motivo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataExpedicao { get; set; }
        public int ServicoId { get; set; }
        public int EmpresaContratanteId { get; set; }
        public string NomeEmpresaContratante { get; set; }
        public int EmpresaContratadaId { get; set; }
        public string NomeEmpresaContratada { get; set; }
        public bool Ativo { get; set; }

        public string FileName { get; set; }
        public byte[] File { get; set; }

        public virtual Empresa Empresa { get; set; }
        public virtual Servico Servico { get; set; }


    }
}
