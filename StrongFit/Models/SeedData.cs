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
                
                context.SaveChanges();
            }
        }
    }
}
