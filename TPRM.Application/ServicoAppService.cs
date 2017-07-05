using TPRM.Application.Interface;
using TPRM.Domain.Entities;
using TPRM.Domain.Interfaces.Services;

namespace TPRM.Application
{
    public class ServicoAppService : AppServiceBase<Servico>, IServicoAppService
    {
        private readonly IServicoService _servicoService;

        public ServicoAppService(IServicoService servicoService)
            : base(servicoService)
        {
            _servicoService = servicoService;
        }
    }
}

