using System.Collections.Generic;
using TPRM.Domain.Entities;

namespace TPRM.Application.Interface
{
    public interface IEmpresaAppService : IAppServiceBase<Empresa>
    {
        IEnumerable<Empresa> BuscarPorNome(string nome);
    }
}

