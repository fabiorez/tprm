using System.Collections.Generic;
using TPRM.Domain.Entities;

namespace TPRM.Domain.Interfaces.Services
{
    public interface IEmpresaService : IServiceBase<Empresa>
    {
        IEnumerable<Empresa> BuscarPorNome(string nome);
    }
}
