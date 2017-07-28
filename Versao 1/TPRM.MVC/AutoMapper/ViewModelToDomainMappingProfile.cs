using AutoMapper;
using TPRM.Domain.Entities;
using TPRM.MVC.ViewModels;

namespace TPRM.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<Servico, ServicoViewModel>();
            CreateMap<Transacao, TransacaoViewModel>();
            CreateMap<Empresa, EmpresaViewModel>();
        }
    }
}