namespace StrongFit.Models
{
    public class Treino
    {
        public int TreinoID { get; set; }
        public int AlunoID { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }
        public Aluno? aluno { get; set; }
        public ICollection<ExercicioTreino>? ExercicioTreino { get; set; }
    }
}
