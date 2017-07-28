using System.Data.Entity.ModelConfiguration;
using TPRM.Domain.Entities;

namespace TPRM.Infra.Data.EntityConfig
{
    public class ServicoConfiguration : EntityTypeConfiguration<Servico>
    {
        public ServicoConfiguration()
        {
            HasKey(c => c.ServicoId);

            Property(c => c.DescricaoServico)
                .IsRequired()
                .HasMaxLength(200);

            Property(c => c.ValorServico)
                .IsRequired();
        }
    }
}
