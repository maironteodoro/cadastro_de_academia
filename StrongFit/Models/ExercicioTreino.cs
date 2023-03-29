namespace StrongFit.Models
{
    public class ExercicioTreino
    {
        public int ExercicioTreinoID { get; set; }
        public int ExercicioID { get; set; }
        public int TreinoID { get; set; }
        public Exercicio? exercicio { get; set; }
        public Treino? treino { get; set; }
    }
}
