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
                var personal = new Personal { Nome = "Mairon", Especialidade = "Musculação" };
                context.Personals.AddRange(personal);
                context.SaveChanges();

                // Depois do SaveChanges, se sua coluna for Identity, você deve ter o Id assim:
                var idPersonalID = personal.PersonalID;

                string date = DateTime.Now.ToString("24/06/2001");

                context.Alunos.AddRange(
                  new Aluno
                  {
                      Nome = "Davi",
                      //Data_Nascimento = DateTime.ParseExact("24/06/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                      Data_Nascimento = Convert.ToDateTime(date),
                      E_Mail = "daviOnePunch",
                      Instagram = "OnePunch",
                      Telefone = "99865-3759",
                      Observacoes = "sem observação",
                      PersonalID = idPersonalID
                  });

                context.Categorias.Add(new Categoria { Nome = "Peitoral" });

                var exercicio = new Exercicio { Nome = "Supino Reto", CategoriaID = 1, Descricao = "3 de 8" };
                context.Exercicios.AddRange(exercicio);
                context.SaveChanges();

                var idExercicioID = exercicio.ExercicioID;

                var treino = new Treino { AlunoID = 1, Data = Convert.ToDateTime("27/03/2023"), Hora = Convert.ToDateTime("14:00:00") };
                context.Treinos.AddRange(treino);
                context.SaveChanges();

                var idTreinoID = treino.TreinoID;

                context.ExercicioTreinos.AddRange(
                    new ExercicioTreino { TreinoID = idTreinoID, ExercicioID = idExercicioID });
                context.SaveChanges();
            }
        }
    }
}
