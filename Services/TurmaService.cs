using Projeto02App.Entities;
using Projeto02App.Repositories;
using Projeto02App.Validators;

namespace Projeto02App.Services
{
    public class TurmaService
    {
        public void ExecutarMenu()
        {
            Console.WriteLine("\n<---- COTI INFORMÁTICA ---->\n");
            Console.WriteLine("\nMenu Turma\n");

            Console.WriteLine("(1) Cadastrar");
            Console.WriteLine("(2) Atualizar");
            Console.WriteLine("(3) Excluir");
            Console.WriteLine("(4) Consultar");
            Console.WriteLine("(5) Exportar");
            Console.WriteLine("(0) Voltar");

            Console.Write("Informe a opção desejada: ");
            if (!int.TryParse(Console.ReadLine(), out var opcao))
            {
                opcao = -1;
            }

            switch (opcao)
            {
                case 1:
                    Cadastrar();
                    break;

                case 2:
                    Atualizar();
                    break;

                case 3:
                    Excluir();
                    break;

                case 4:
                    Consultar();
                    break;

                case 5:
                    Exportar();
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("\nOpção Inválida!");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione uma tecla para continuar...");
                Console.ReadKey();

                Console.Clear();
                ExecutarMenu();
            }
        }

        private void Cadastrar()
        {
            Console.WriteLine("\nCadastrar Turma\n");

            Console.Write("Digite o Id do Professor: ");
            if (!Guid.TryParse(Console.ReadLine(), out var idProfessor))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var professorRepository = new ProfessorRepository();
            var professor = professorRepository.ObterPorId(idProfessor);

            if (professor == null)
            {
                Console.WriteLine("\nProfessor não encontrado\n");
                return;
            }

            Console.WriteLine("\tProfessor: " + professor.Nome);

            Console.Write("\nDigite o Id do Curso: ");
            if (!Guid.TryParse(Console.ReadLine(), out var idCurso))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var cursoRepository = new CursoRepository();
            var curso = cursoRepository.ObterPorId(idCurso);

            if (curso == null)
            {
                Console.WriteLine("\nCurso não encontrado\n");
                return;
            }

            Console.WriteLine("\tCurso: " + curso.Nome);

            var turma = new Turma();
            turma.Professor = professor;
            turma.Curso = curso;

            Console.Write("\nDigite o Nome da Turma: ");
            turma.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite a Data de Início (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var dataInicio))
            {
                Console.WriteLine("\nData inválida\n");
                return;
            }

            turma.DataInicio = dataInicio;

            Console.Write("Digite o Horário: ");
            turma.Horario = Console.ReadLine() ?? string.Empty;

            if (ObjetoValidator.ValidarObjeto(turma))
            {
                var turmaRepository = new TurmaRepository();
                turmaRepository.Inserir(turma);

                Console.WriteLine("\nTurma Cadastrada!");
            }
        }

        private void Atualizar()
        {
            Console.WriteLine("\nAtualizar Turma\n");

            Console.Write("Digite o Id Da Turma: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var turmaRepository = new TurmaRepository();
            var turma = turmaRepository.ObterPorId(id);

            if (turma == null)
            {
                Console.WriteLine("\nTurma não encontrada\n");
                return;
            }

            Console.WriteLine("\nDados da Turma");
            Console.WriteLine("\tNome: " + turma.Nome);
            Console.WriteLine("\tData de Início: " + turma.DataInicio.ToString("dd/MM/yyyy"));
            Console.WriteLine("\tHorário: " + turma.Horario);

            Console.Write("\nEsta é a turma? (S/N): ");
            var resposta = Console.ReadLine() ?? string.Empty;

            if (resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nInforme os novos dados: ");

                Console.Write("Informe o Nome: ");
                turma.Nome = Console.ReadLine() ?? string.Empty;

                Console.Write("Informe a Data de Início (dd/mm/aaaa): ");
                if (!DateTime.TryParse(Console.ReadLine(), out var dataInicio))
                {
                    Console.WriteLine("\nData inválida\n");
                    return;
                }

                turma.DataInicio = dataInicio;

                Console.Write("Informe o Horário: ");
                turma.Horario = Console.ReadLine() ?? string.Empty;

                if (ObjetoValidator.ValidarObjeto(turma))
                {
                    turmaRepository.Atualizar(turma);

                    Console.WriteLine("\nTurma Atualizada!");
                }
            }
            else
            {
                Console.WriteLine("\nOperação cancelada!");
            }
        }

        private void Excluir()
        {
            Console.WriteLine("\nExcluir Turma\n");

            Console.Write("Digite o Id Da Turma: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var turmaRepository = new TurmaRepository();
            var turma = turmaRepository.ObterPorId(id);

            if (turma == null)
            {
                Console.WriteLine("\nTurma não encontrada\n");
                return;
            }

            Console.WriteLine("\nDados da Turma");
            Console.WriteLine("\tNome: " + turma.Nome);
            Console.WriteLine("\tData de Início: " + turma.DataInicio.ToString("dd/MM/yyyy"));
            Console.WriteLine("\tHorário: " + turma.Horario);

            Console.Write("\nDeseja excluir essa turma? (S/N): ");
            var resposta = Console.ReadLine() ?? string.Empty;

            if (resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                turmaRepository.Excluir(turma.IdTurma);

                Console.WriteLine("\nTurma Excluída!");
            }
            else
            {
                Console.WriteLine("\nOperação cancelada!");
            }
        }

        private void Consultar()
        {
            Console.WriteLine("\nConsultar Turmas\n");

            var turmaRepository = new TurmaRepository();
            var turmas = turmaRepository.ObterTodas();

            if (turmas.Count == 0)
            {
                Console.WriteLine("Nenhuma Turma Cadastrada");
                return;
            }

            foreach (var turma in turmas)
            {
                Console.WriteLine($"\nDados da Turma: Id: {turma.IdTurma} Nome: {turma.Nome} Início: {turma.DataInicio:dd/MM/yyyy} Horário: {turma.Horario}");
            }
        }

        private void Exportar()
        {
            Console.WriteLine("\nExportar Turma\n");

            Console.Write("Digite o Id Da Turma: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var turmaRepository = new TurmaRepository();
            var turma = turmaRepository.ObterExportar(id);

            if (turma == null)
            {
                Console.WriteLine("\nTurma não encontrada\n");
                return;
            }

            Console.WriteLine("\tTurma: " + turma.Nome);

            Console.WriteLine("\nEscolha o formato:");
            Console.WriteLine("(1) JSON");
            Console.WriteLine("(2) XML");
            Console.WriteLine("(3) Os dois");

            Console.Write("Informe a opção desejada: ");
            if (!int.TryParse(Console.ReadLine(), out var opcao))
            {
                opcao = -1;
            }

            var turmaJsonRepository = new TurmaJsonRepository();
            var turmaXmlRepository = new TurmaXmlRepository();

            switch (opcao)
            {
                case 1:
                    turmaJsonRepository.ExportarJson(turma);
                    Console.WriteLine("\nTurma exportada com Sucesso!");

                    break;

                case 2: turmaXmlRepository.ExportarXml(turma);
                    Console.WriteLine("\nTurma exportada com Sucesso!");

                    break;

                case 3: turmaXmlRepository.ExportarXml(turma); turmaJsonRepository.ExportarJson(turma);
                    Console.WriteLine("\nTurma exportada com Sucesso!");
                    break;
            }
        }
    }
}
