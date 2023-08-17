using app.Data;
using app.Model;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public class FuncionarioRepository
    {
        private readonly Context context;

        public FuncionarioRepository(Context context)
        {
            this.context = context;
        }

        public void AdicionaFuncionario(Funcionario funcionario)
        {
            this.context.Add(funcionario);
        }

        public void AtualizaFuncionario(Funcionario funcionario)
        {
            this.context.Update(funcionario);
        }

        public async Task<Funcionario?> BuscaFuncionario(int id)
        {
            return await this.context.Funcionarios.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Funcionario>> BuscaFuncionarios()
        {
            return await this.context.Funcionarios.ToListAsync();
        }

        public void DeleteFuncionario(Funcionario funcionario)
        {
            this.context.Remove(funcionario);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}