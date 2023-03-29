using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace StrongFit.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            Context context = app.ApplicationServices.GetRequiredService<Context>();
            context.Database.Migrate();       
            if (!context.Personals.Any())
            {
                var registro = new Personal { Nome = "Mairon", Especialidade = "Musculação" };
                context.Personals.AddRange(registro);
                context.SaveChanges();

                // Depois do SaveChanges, se sua coluna for Identity, você deve ter o Id assim:
                var idGerado = registro.PersonalID ;

                context.Alunos.AddRange(
                  new Aluno
                  {
                      Nome = "Davi",
                      Data_Nascimento = DateTime.ParseExact("24/06/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                      E_Mail = "daviOnePunch",
                      Instagram = "OnePunch",
                      Telefone = "99865-3759",
                      Observacoes = "sem observação",
                      PersonalID = idGerado
                  });

                context.Categorias.Add(new Categoria { Nome = "Peitoral" });

                context.Exercicios.AddRange(
                    new Exercicio { Nome = "Supino Reto", CategoriaID = 1, Descricao = "3 de 8" });

                context.Treinos.AddRange(
                    new Treino { AlunoID = 1, Data = Convert.ToDateTime("27/03/2023"), Hora = Convert.ToDateTime("14:00:00") });

                context.ExercicioTreinos.AddRange(
                    new ExercicioTreino { TreinoID = 1, ExercicioID = 1 });
                context.SaveChanges();
            }
        }
    }
}
