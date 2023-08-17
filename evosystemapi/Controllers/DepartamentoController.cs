using app.Model;
using app.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly DepartamentoRepository repository;

        public DepartamentoController(DepartamentoRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.repository.BuscaDepartamentos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var departamentoBanco = await this.repository.BuscaDepartamento(id);

            if (departamentoBanco == null)
            {
                return NotFound("Departamento não encontrado");
            }

            return Ok(departamentoBanco);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Departamento departamento)
        {
            if (departamento.Nome.Length == 0)
            {
                return BadRequest("Informe o nome");
            }

            if (departamento.Sigla.Length == 0)
            {
                return BadRequest("Informe a sigla");
            }

            this.repository.AdicionaDepartamento(departamento);

            try
            {
                await this.repository.SaveChangesAsync();
                return Created("", "Departamento criado com sucesso");
            }
            catch (System.Exception)
            {
                return new ObjectResult("Erro ao cadastrar departamento.")
                {
                    StatusCode = 500
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Departamento departamento)
        {
            var departamentoBanco = await this.repository.BuscaDepartamento(id);

            if (departamentoBanco == null)
            {
                return NotFound("Deptartamento não encontrado");
            }

            departamentoBanco.Nome = departamento.Nome.Length == 0 ? departamentoBanco.Nome : departamento.Nome;
            departamentoBanco.Sigla = departamento.Sigla.Length == 0 ? departamentoBanco.Sigla : departamento.Sigla;

            this.repository.AtualizaDepartamento(departamentoBanco);

            try
            {
                await this.repository.SaveChangesAsync();
                return Ok("Departamento atualizado com sucesso");
            }
            catch (System.Exception)
            {
                return new ObjectResult("Erro ao atualizar departamento.")
                {
                    StatusCode = 500
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var departamentoBanco = await this.repository.BuscaDepartamento(id);

            if (departamentoBanco == null)
            {
                return NotFound("Departamento não encontrado");
            }

            this.repository.DeleteDepartamento(departamentoBanco);

            return await this.repository.SaveChangesAsync() ? Ok("200") : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}