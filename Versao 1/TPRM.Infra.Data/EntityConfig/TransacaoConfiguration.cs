using System.Data.Entity.ModelConfiguration;
using TPRM.Domain.Entities;

namespace TPRM.Infra.Data.EntityConfig
{
    public class TransacaoConfiguration : EntityTypeConfiguration<Transacao>
    {
        public TransacaoConfiguration()
        {
            HasKey(c => c.TransacaoId);

            Property(c => c.Motivo)
                .HasMaxLength(250);

            HasRequired(p => p.Empresa)
                .WithMany()
                .HasForeignKey(p => p.EmpresaContratanteId);

            HasRequired(p => p.Servico)
                .WithMany()
                .HasForeignKey(p => p.ServicoId);
        }
    }
}
