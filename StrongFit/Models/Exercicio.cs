namespace StrongFit.Models
{
    public class Exercicio
    {
        public int ExercicioID { get; set; }
        public string? Nome { get; set; }
        public int CategoriaID { get; set; }// mudar para classe categoria
        public string? Descricao { get; set; }
        public Categoria? Categoria { get; set; }
        public ICollection<ExercicioTreino> exercicioTreino { get; set; }
    }
}
