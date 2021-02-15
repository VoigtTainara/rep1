using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tcc_project_API.Data;
using tcc_project_API.Models;

namespace tcc_project_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class FuncionarioController : Controller
    {
        public IRepository _repo { get; }
        public FuncionarioController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            try
            {
                var result = await _repo.GetAllFuncionariosAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("{FuncionarioId}")]
        public async Task<IActionResult> getByFuncionarioId(int FuncionarioId)
        {
            try
            {
                var result = await _repo.GetFuncionarioAsyncById(FuncionarioId,true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpGet("BySetor/{SetorId}")]
        public async Task<IActionResult> getBySetorId(int SetorId)
        {
            try
            {
                var result = await _repo.GetFuncionariosBySetorId(SetorId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Funcionario model)
        {
            try
            {
                _repo.Add(model);
                if(await _repo.SaveChangesAsync()){
                    return Created($"/api/funcionario/{model.id}", model);
                }              
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpPut("{FuncionarioId}")]
        public async Task<IActionResult> put(int FuncionarioId, Funcionario model)
        {
            try
            {
                var funcionario =  await _repo.GetFuncionarioAsyncById(FuncionarioId, false);
                if (funcionario ==null) return NotFound();
                _repo.Update(model);
                if(await _repo.SaveChangesAsync()){
                    funcionario =  await _repo.GetFuncionarioAsyncById(FuncionarioId, true);
                    return Created($"/api/funcionario/{model.id}", funcionario);
                }              
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou");
            }
            return BadRequest();
        }

        [HttpDelete("{FuncionarioId}")]
        public async Task<IActionResult> delete(int FuncionarioId)
        {
            try
            {
                var funcionario =  await _repo.GetFuncionarioAsyncById(FuncionarioId, false);
                if (funcionario ==null) return NotFound();
                _repo.Delete(funcionario);

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