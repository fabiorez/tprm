using System;
using FluentValidator;
using TPRM.Shared.Entities;

namespace TPRM.Domain.Entities
{
    public class Servico : Entity
    {
        public Servico(string descricaoServico, decimal valorServico)
        {
            DescricaoServico = descricaoServico;
            ValorServico = valorServico;
            DataCadastro = DateTime.Now;
            Ativo = true;

            new ValidationContract<Servico>(this)
                .IsRequired(x => DescricaoServico, "Favor preencher a descriçao do serviço")
                .HasMaxLenght(x => x.DescricaoServico, 60, "Não pode haver mais de 60 caracteres")
                .HasMinLenght(x => x.DescricaoServico, 3, "Não pode haver menos de 3 caracteres");
        }

        public string DescricaoServico { get; private set; }
        public decimal ValorServico { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        public void AtivarServico() => Ativo = true;

        public void DesativarServico() => Ativo = false;
    }
}
