using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TPRM.MVC.ViewModels
{
    public class ServicoViewModel
    {
        [Key]
        public int ServicoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(150, ErrorMessage = "Máximo de 150 caracteres")]
        [MinLength(2, ErrorMessage = "Minimo de 2 caracteres")]
        [DisplayName("Descrição")]
        public string DescricaoServico { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Preencha o campo Valor")]
        [DisplayName("Valor")]
        public decimal ValorServico { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        public Boolean Ativo { get; set; }
    }
}