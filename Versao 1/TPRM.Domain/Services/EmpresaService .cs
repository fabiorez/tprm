using System.Collections.Generic;
using TPRM.Domain.Entities;
using TPRM.Domain.Interfaces.Repositories;
using TPRM.Domain.Interfaces.Services;

namespace TPRM.Domain.Services
{
    public class EmpresaService : ServiceBase<Empresa>, IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
            : base(empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public IEnumerable<Empresa> BuscarPorNome(string nome)
        {
            return _empresaRepository.BuscarPorNome(nome);
        }
    }
}
