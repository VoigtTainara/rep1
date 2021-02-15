using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tcc_project_API.Data;
using tcc_project_API.Models;

namespace tcc_project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : Controller
    {
      
        public IRepository _repo { get; }
        public EmpresaController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            try
            {
                var result = await _repo.GetAllEmpresasAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{EmpresaId}")]
        public async Task<IActionResult> getByEmpresaId(int EmpresaId)
        {
            try
            {
                var result = await _repo.GetEmpresaAsyncById(EmpresaId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Empresa model)
        {
            try
            {
                _repo.Add(model);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/empresa/{model.id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpPut("{EmpresaId}")]
        public async Task<IActionResult> put(int EmpresaId, Empresa model)
        {
            try
            {
                var empresa = await _repo.GetEmpresaAsyncById(EmpresaId, false);
                if (empresa == null) return NotFound();
                _repo.Update(model);
                if (await _repo.SaveChangesAsync()){
                    return Created($"/api/empresa/{model.id}", empresa);
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{EmpresaId}")]
        public async Task<IActionResult> delete(int EmpresaId)
        {
            try
            {
                var empresa = await _repo.GetEmpresaAsyncById(EmpresaId, false);
                if (empresa == null) return NotFound();
                _repo.Delete(empresa);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }            
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }
    }
}