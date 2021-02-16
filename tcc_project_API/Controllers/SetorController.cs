using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tcc_project_API.Data;
using tcc_project_API.Models;

namespace tcc_project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetorController : Controller
    {
        public IRepository _repo { get; }
        public SetorController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            try
            {
                var result = await _repo.GetAllSetoresAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{SetorId}")]
        public async Task<IActionResult> getBySetorId(int SetorId)
        {
            try
            {
                var result = await _repo.GetSetorAsyncById(SetorId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("ByEmpresa/{EmpresaId}")]
        public async Task<IActionResult> getByEmpresaId(int EmpresaId)
        {
            try
            {
                var result = await _repo.GetSetoresByEmpresaId(EmpresaId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Setor model)
        {
             try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/setor/{model.id}", model);
                }              
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{SetorId}")]
        public async Task<IActionResult> delete(int SetorId)
        {
            try
            {
                var setor = await _repo.GetSetorAsyncById(SetorId, false);
                if (setor ==null) return NotFound();
                _repo.Delete(setor);

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