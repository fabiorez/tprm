using TPRM.Application.Interface;
using TPRM.Domain.Entities;
using TPRM.Domain.Interfaces.Services;

namespace TPRM.Application
{
    public class TransacaoAppService : AppServiceBase<Transacao>, ITransacaoAppService
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoAppService(ITransacaoService transacaoService)
            : base(transacaoService)
        {
            _transacaoService = transacaoService;
        }
    }
}

