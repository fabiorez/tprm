using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using TPRM.Domain.Entities;
using TPRM.Infra.Data.EntityConfig;

namespace TPRM.Infra.Data.Contexto
{
    public class TprmContext : DbContext
    {
        public TprmContext()
            : base("TPRMConnection")
        {
        }

        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Properties()
            //    .Where(p => p.Name == p.ReflectedType.Name + "Id")
            //    .Configure((p => p.IsKey() ));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ServicoConfiguration());
            modelBuilder.Configurations.Add(new EmpresaConfiguration());
            modelBuilder.Configurations.Add(new TransacaoConfiguration());

        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Status") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Status").CurrentValue = "Pendente";
                }
            }
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Ativo") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Ativo").CurrentValue = true;
                }
            }
            return base.SaveChanges();
        }
    }
}
