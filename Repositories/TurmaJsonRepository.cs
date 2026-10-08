using System.Text.Json;
using Projeto02App.Entities;

namespace Projeto02App.Repositories
{
    public class TurmaJsonRepository
    {
        public void ExportarJson(Turma turma)
        {
            var diretorio = "c:\\temp";
            var caminho = Path.Combine(diretorio, $"turma_{turma.Nome}.json");

            Directory.CreateDirectory(diretorio);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(turma, options);

            File.WriteAllText(caminho, json);

        }
    }
}
