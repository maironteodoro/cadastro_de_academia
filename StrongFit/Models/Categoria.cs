namespace StrongFit.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string Nome { get; set; }

        public ICollection<Exercicio> Exercicios { get; set; }
    }
}
