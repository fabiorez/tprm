using System.Collections.Generic;
using TPRM.Application.Interface;
using TPRM.Domain.Entities;
using TPRM.Domain.Interfaces.Services;

namespace TPRM.Application
{
    public class EmpresaAppService : AppServiceBase<Empresa>, IEmpresaAppService
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaAppService(IEmpresaService empresaService)
            : base(empresaService)
        {
            _empresaService = empresaService;
        }

        public IEnumerable<Empresa> BuscarPorNome(string nome)
        {
            return _empresaService.BuscarPorNome(nome);
        }
    }
}

