using Microsoft.EntityFrameworkCore;
using app.Model;

namespace app.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options) : base(options)
        {}

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}