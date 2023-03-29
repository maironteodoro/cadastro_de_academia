using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StrongFit.Models;

namespace StrongFit.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base (options) 
        {
        }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<ExercicioTreino> ExercicioTreinos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
