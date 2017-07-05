using TPRM.Domain.Entities;
using TPRM.Domain.Interfaces.Repositories;
using TPRM.Domain.Interfaces.Services;

namespace TPRM.Domain.Services
{
    public class ServicoService : ServiceBase<Servico>, IServicoService
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoService(IServicoRepository servicoRepository)
            : base(servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }
    }
}
