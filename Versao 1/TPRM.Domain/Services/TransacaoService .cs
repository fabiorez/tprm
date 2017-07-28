using TPRM.Domain.Entities;
using TPRM.Domain.Interfaces.Repositories;
using TPRM.Domain.Interfaces.Services;

namespace TPRM.Domain.Services
{
    public class TransacaoService : ServiceBase<Transacao>, ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository)
            : base(transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }
    }
}
