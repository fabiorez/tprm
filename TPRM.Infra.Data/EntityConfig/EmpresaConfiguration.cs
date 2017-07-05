using System.Data.Entity.ModelConfiguration;
using TPRM.Domain.Entities;

namespace TPRM.Infra.Data.EntityConfig
{
    public class EmpresaConfiguration : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfiguration()
        {
            HasKey(c => c.EmpresaId);

            Property(c => c.EmpresaTipo)
               .IsRequired();

            Property(c => c.ServicoId)
                .IsRequired();

            Property(c => c.EmpresaNome)
                .IsRequired()
                .HasMaxLength(200);

            HasRequired(p => p.Servico)
                .WithMany()
                .HasForeignKey(p => p.ServicoId);
        }
    }
}
