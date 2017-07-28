using AutoMapper;
using TPRM.Domain.Entities;
using TPRM.MVC.ViewModels;

namespace TPRM.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ServicoViewModel, Servico>();
            CreateMap<TransacaoViewModel, Transacao>();
            CreateMap<EmpresaViewModel, Empresa>();
        }
    }
}