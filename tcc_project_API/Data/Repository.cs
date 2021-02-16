using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tcc_project_API.Models;

namespace tcc_project_API.Data
{
    public class Repository : IRepository
    {
        public DataContext _context{get;}
        public Repository(DataContext context)
        {
            _context=context;       
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
         public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return(await _context.SaveChangesAsync()>0);
        }

        //FUNCIONÁRIOS
        public async Task<Funcionario[]> GetAllFuncionariosAsync(bool includeSetor = false)
        {
            IQueryable<Funcionario> query = _context.funcionarios;
            if (includeSetor){
              query = query.Include(f => f.setor); 
            }
            query = query.AsNoTracking().OrderBy(f => f.id);

            return await query.ToArrayAsync();
        }

        public async Task<Funcionario> GetFuncionarioAsyncById(int Funcionarioid, bool includeSetor)
        {
            IQueryable<Funcionario> query = _context.funcionarios;
            if (includeSetor){
              query = query.Include(f => f.setor); 
            }
            query = query.AsNoTracking().OrderBy(f => f.id).Where(funcionario => funcionario.id == Funcionarioid);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Funcionario[]> GetFuncionariosBySetorId(int SetorId, bool includeSetor)
        {
            IQueryable<Funcionario> query = _context.funcionarios;
            if (includeSetor){
              query = query.Include(f => f.setor); 
            }
            query = query.AsNoTracking().OrderBy(f => f.id).Where(funcionario => funcionario.setorId == SetorId);

            return await query.ToArrayAsync();
        }

        //SETORES
        public async Task<Setor[]> GetAllSetoresAsync(bool includeFuncionario = false)
        {
            IQueryable<Setor> query = _context.setores;
            if (includeFuncionario){
              query = query.Include(f => f.funcionarios); 
            }
            query = query.Include(f => f.empresa);
            query = query.OrderBy(f => f.id);

            return await query.ToArrayAsync();
        }

        public async Task<Setor[]> GetSetoresByEmpresaId(int EmpresaId, bool includeEmpresa)
        {
            IQueryable<Setor> query = _context.setores;
            if (includeEmpresa){
              query = query.Include(f => f.empresa); 
            }
            query = query.Include(f => f.funcionarios);
            query = query.AsNoTracking().OrderBy(f => f.id).Where(setor => setor.empresaId == EmpresaId);

            return await query.ToArrayAsync();
        }

        public async Task<Setor> GetSetorAsyncById(int SetorId, bool includeFuncionario = false)
        {
             IQueryable<Setor> query = _context.setores;
             if(includeFuncionario){
                 query = query.Include(f => f.funcionarios);
             }
            query = query.AsNoTracking().OrderBy(f => f.id).Where(setor => setor.id == SetorId);

            return await query.FirstOrDefaultAsync();
        }

        //EMPRESAS
        public async Task<Empresa[]> GetAllEmpresasAsync(bool includeSetor = false)
        {
            IQueryable<Empresa> query = _context.empresas;
            if (includeSetor){
              query = query.Include(f => f.setores); 
            }
            query = query.OrderBy(f => f.id);

            return await query.ToArrayAsync();
        }

        public async Task<Empresa> GetEmpresaAsyncById(int empresaId, bool includeSetor = false)
        {
            IQueryable<Empresa> query = _context.empresas;
            if (includeSetor){
              query = query.Include(f => f.setores); 
            }
            query = query.AsNoTracking().OrderBy(f => f.id).Where(empresa => empresa.id == empresaId);

            return await query.FirstOrDefaultAsync();
        }

      
    }
}