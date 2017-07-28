using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPRM.MVC.ViewModels
{
    public class EmpresaViewModel
    {
        [Key]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "Preencha o campo nome")]
        [MaxLength(150, ErrorMessage = "Máximo de {0} caracteres")]
        [MinLength(2, ErrorMessage = "Minimo de {0} caracteres")]
        [DisplayName("Nome")]
        public string EmpresaNome { get; set; }

        public int TransacaoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Tipo de empresa")]
        [DisplayName("Tipo")]
        public string EmpresaTipo { get; set; }

        [Required(ErrorMessage = "Preencha o campo Servico")]
        [DisplayName("Tipo de Servico")]
        public int ServicoId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor Devido")]
        public Decimal ValorDevido { get; set; }

        [DisplayName("Status")]
        public bool Ativo { get; set; }

        public class Tipo
        {
            public string TipoId { get; set; }
            public string Nome { get; set; }

            public List<Tipo> ListaTipos()
            {
                return new List<Tipo>
                {
                    new Tipo {TipoId = "Contratante", Nome = "Contratante"},
                    new Tipo {TipoId = "Contratado", Nome = "Contratado"}
                };
            }
        }

        public virtual IEnumerable<TransacaoViewModel> Transacoes { get; set; }

        public virtual ServicoViewModel Servico { get; set; }

    }
}