using Projeto02App.Entities;
using System.Xml;
using System.Xml.Serialization;

namespace Projeto02App.Repositories
{
    public class TurmaXmlRepository
    {
        public void ExportarXml(Turma turma)
        {
            var xmlSerializer = new XmlSerializer(typeof(Turma));

            var diretorio = "c:\\temp";
            var caminho = Path.Combine(diretorio, $"turma_{turma.Nome}.xml");

            Directory.CreateDirectory(diretorio);

            using (var writer = XmlWriter.Create(caminho))
            {
                xmlSerializer.Serialize(writer, turma);
            }
        }
    }
}
