using System.Collections.Generic;
using System.Linq;
using TPRM.Domain.Entities;
using TPRM.Domain.Interfaces.Repositories;

namespace TPRM.Infra.Data.Repositorios
{
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        public IEnumerable<Empresa> BuscarPorNome(string nome)
        {
            return Db.Empresas.Where(p => p.EmpresaNome == nome);
        }
    }
}
