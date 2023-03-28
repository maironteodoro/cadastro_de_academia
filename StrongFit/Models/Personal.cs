namespace StrongFit.Models
{
    public class Personal
    {
        public int PersonalID { get; set; }
        public string? Nome { get; set; }
        public ICollection<Aluno>? Alunos { get; set; }
        public string? Especialidade { get; set; }
    }
}
