using app.Model;
using app.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepository repository;

        public FuncionarioController(FuncionarioRepository repository)
        {
            this.repository = repository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var funcionarioBanco = await this.repository.BuscaFuncionario(id);

            if (funcionarioBanco == null)
            {
                return NotFound("Funcionário não encontrado");
            }

            return Ok(funcionarioBanco);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.repository.BuscaFuncionarios());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Funcionario funcionario)
        {
            if (funcionario.Nome.Length == 0) {
                return BadRequest("Informe o nome");
            }

            if (funcionario.Foto.Length == 0) {
                return BadRequest("Informe a foto");
            }

            if (funcionario.Rg.Length == 0) {
                return BadRequest("Informe o RG");
            }

            if (funcionario.DepartamentoId == 0) {
                return BadRequest("Informe o departamento");
            }

            this.repository.AdicionaFuncionario(funcionario);

            try
            {
                await this.repository.SaveChangesAsync();
                return Created("", "Funcionário criado com sucesso");
            }
            catch (System.Exception)
            {
                return new ObjectResult("Erro ao cadastrar funcionário.")
                {
                    StatusCode = 500
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Funcionario funcionario)
        {
            var funcionarioBanco = await this.repository.BuscaFuncionario(id);

            if (funcionarioBanco == null)
            {
                return NotFound("Usuário não encontrado");
            }

            funcionarioBanco.Nome = funcionario.Nome.Length == 0 ? funcionarioBanco.Nome : funcionario.Nome;
            funcionarioBanco.Foto = funcionario.Foto.Length == 0 ? funcionarioBanco.Foto : funcionario.Foto;
            funcionarioBanco.Rg = funcionario.Rg.Length == 0 ? funcionarioBanco.Rg : funcionario.Rg;
            funcionarioBanco.DepartamentoId = funcionario.DepartamentoId == 0 ? funcionarioBanco.DepartamentoId : funcionario.DepartamentoId;

            this.repository.AtualizaFuncionario(funcionarioBanco);

            try
            {
                await this.repository.SaveChangesAsync();
                return Ok("Funcionário atualiazdo com sucesso");
            }
            catch (System.Exception)
            {
                return new ObjectResult("Erro ao atualizar funcionário.")
                {
                    StatusCode = 500
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var funcionarioBanco = await this.repository.BuscaFuncionario(id);

            if (funcionarioBanco == null)
            {
                return NotFound("Usuário não encontrado");
            }

            this.repository.DeleteFuncionario(funcionarioBanco);

            return await this.repository.SaveChangesAsync() ? Ok("Funcionário atualizado com sucesso") : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}