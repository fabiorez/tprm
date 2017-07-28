using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TPRM.MVC.ViewModels
{
    public class TransacaoViewModel
    {
        [Key]
        public int TransacaoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Servico")]
        [DisplayName("Tipo de Servico")]
        public int ServicoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Empresa Contratante")]
        [DisplayName("Empresa Contratante")]
        public int EmpresaContratanteId { get; set; }

        public string NomeEmpresaContratante { get; set;}

        [Required(ErrorMessage = "Preencha o campo Empresa Contratada")]
        [DisplayName("Empresa Contratada")]
        public int EmpresaContratadaId { get; set; }

        public string NomeEmpresaContratada { get; set; }


        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime? DataExpedicao { get; set; }

        public string Motivo { get; set; }

        public string Status { get; set; }

        
        public string FileName { get; set; }
        
        public byte[] File { get; set; }

        public HttpPostedFileBase Files { get; set; }


        public class StatusTipo
        {
            public string StatusId { get; set; }
            public string Nome { get; set; }

            public List<StatusTipo> ListaStatus()
            {
                return new List<StatusTipo>
                {
                    new StatusTipo {StatusId = "Aprovado", Nome = "Aprovado"},
                    new StatusTipo {StatusId = "Reprovado", Nome = "Reprovado"},
                    new StatusTipo {StatusId = "Pendente", Nome = "Pendente"}
                };
            }
        }

        public virtual ServicoViewModel Servico { get; set; }
        public virtual EmpresaViewModel Empresa { get; set; }

    }
}