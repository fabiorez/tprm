using System.Collections.Generic;
using TPRM.Domain.Entities;

namespace TPRM.Domain.Interfaces.Repositories
{
    public interface IEmpresaRepository : IRepositoryBase<Empresa>
    {
        IEnumerable<Empresa> BuscarPorNome(string nome);
    }
}
