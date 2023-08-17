using app.Data;
using app.Model;
using Microsoft.EntityFrameworkCore;

namespace app.Repositories
{
    public class DepartamentoRepository
    {
        private readonly Context context;

        public DepartamentoRepository(Context context)
        {
            this.context = context;
        }

        public void AdicionaDepartamento(Departamento departamento)
        {
            this.context.Add(departamento);
        }

        public void AtualizaDepartamento(Departamento departamento)
        {
            this.context.Update(departamento);
        }

        public async Task<Departamento?> BuscaDepartamento(int id)
        {
            return await this.context.Departamentos.Include(f => f.ListaFuncionarios).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Departamento>> BuscaDepartamentos()
        {
            return await this.context.Departamentos.ToListAsync();
        }

        public void DeleteDepartamento(Departamento departamento)
        {
            this.context.Remove(departamento);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }
    }
}