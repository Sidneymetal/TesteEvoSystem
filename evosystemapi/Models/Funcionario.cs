namespace app.Model
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Rg { get; set; }
        public int DepartamentoId { get; set; }
    }
}