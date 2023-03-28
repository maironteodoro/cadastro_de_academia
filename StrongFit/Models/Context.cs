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
    }
}
