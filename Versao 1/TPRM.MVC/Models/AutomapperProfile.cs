using AutoMapper;
using TPRM.Domain.Entities;
using TPRM.MVC.ViewModels;

namespace TPRM.MVC.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ServicoViewModel, Servico>();
             
        }
    }
}