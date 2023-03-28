using Microsoft.EntityFrameworkCore;

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
                context.Personals.AddRange(
                    new Personal { Nome = "Mairon", Especialidade = "Musculação", PersonalID = 1 });

                context.Alunos.AddRange(
                    new Aluno { Nome = "Davi", Data_Nascimento = Convert.ToDateTime("24/06/2001"), E_Mail = "daviOnePunch", Instagram = "OnePunch", Telefone = "99865-3759", PersonalID = 1, Observacoes = "sem observação" });
                context.SaveChanges();
            }
        }
    }
}
