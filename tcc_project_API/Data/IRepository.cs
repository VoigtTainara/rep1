using System.Threading.Tasks;
using tcc_project_API.Models;

namespace tcc_project_API.Data
{
    public interface IRepository
    {
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveChangesAsync();
        //funcionarios
        Task<Funcionario[]> GetAllFuncionariosAsync(bool includeSetor); //false
        Task<Funcionario> GetFuncionarioAsyncById(int Funcionarioid, bool includeSetor);
        Task<Funcionario[]> GetFuncionariosBySetorId(int SetorId, bool includeSetor);
        
        //setores
        Task<Setor[]> GetAllSetoresAsync(bool includeFuncionario); //false
        Task<Setor[]> GetSetoresByEmpresaId(int EmpresaId, bool includeEmpresa);
        Task<Setor> GetSetorAsyncById(int SetorId, bool includeFuncionario);

        //empresas
        Task<Empresa[]> GetAllEmpresasAsync(bool includeSetor);
        Task<Empresa> GetEmpresaAsyncById(int empresaId, bool includeSetor);

    }
}