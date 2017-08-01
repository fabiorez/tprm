using System;
using TPRM.Shared.Entities;

namespace TPRM.Domain.Entities
{
    public class Transacao : Entity
    {
        public Transacao(string status, string motivo, DateTime? dataExpedicao, Guid servicoId, Guid empresaContratanteId, Guid empresaContratadaId, string fileName, byte[] file)
        {
            Status = status;
            Motivo = motivo;
            DataCadastro = DateTime.Now;
            DataExpedicao = dataExpedicao;
            ServicoId = servicoId;
            EmpresaContratanteId = empresaContratanteId;
            EmpresaContratadaId = empresaContratadaId;
            Ativo = true;
            FileName = fileName;
            File = file;
        }

        public string Status { get; private set; }
        public string Motivo { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataExpedicao { get; private set; }
        public Guid ServicoId { get; private set; }
        public Guid EmpresaContratanteId { get; private set; }
        public Guid EmpresaContratadaId { get; private set; }
        public bool Ativo { get; private set; }
        public string FileName { get; private set; }
        public byte[] File { get; private set; }
    }
}
