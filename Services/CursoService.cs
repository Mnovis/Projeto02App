using Microsoft.Data.SqlClient;
using Projeto02App.Entities;
using Projeto02App.Repositories;
using Projeto02App.Validators;

namespace Projeto02App.Services
{
    public class CursoService
    {
        public void ExecutarMenu()
        {
            Console.WriteLine("\n<---- COTI INFORMÁTICA ---->\n");
            Console.WriteLine("\nMenu Curso\n");

            Console.WriteLine("(1) Cadastrar");
            Console.WriteLine("(2) Atualizar");
            Console.WriteLine("(3) Excluir");
            Console.WriteLine("(4) Consultar");
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
            Console.WriteLine("\nCadastrar Curso\n");

            var curso = new Curso();

            Console.Write("Digite o Nome: ");
            curso.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite a Carga Horária: ");
            if (!int.TryParse(Console.ReadLine(), out var cargaHoraria))
            {
                Console.WriteLine("\nCarga horária inválida\n");
                return;
            }

            curso.CargaHoraria = cargaHoraria;

            if (ObjetoValidator.ValidarObjeto(curso))
            {
                var cursoRepository = new CursoRepository();
                cursoRepository.Inserir(curso);

                Console.WriteLine("Curso Cadastrado!");
            }
        }

        private void Atualizar()
        {
            Console.WriteLine("\nAtualizar Curso\n");

            Console.Write("Digite o Id Do Curso: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var cursoRepository = new CursoRepository();
            var curso = cursoRepository.ObterPorId(id);

            if (curso == null)
            {
                Console.WriteLine("\nCurso não encontrado\n");
                return;
            }

            Console.WriteLine("\nDados do Curso");
            Console.WriteLine("\tNome: " + curso.Nome);
            Console.WriteLine("\tCargaHoraria: " + curso.CargaHoraria);

            Console.Write("\nEste é o Curso? (S/N): ");
            var resposta = Console.ReadLine() ?? string.Empty;

            if (resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nInforme os novos dados: ");

                Console.Write("Informe o Nome: ");
                curso.Nome = Console.ReadLine() ?? string.Empty;

                Console.Write("Informe a Carga Horária: ");
                if (!int.TryParse(Console.ReadLine(), out var cargaHoraria))
                {
                    Console.WriteLine("\nCarga horária inválida\n");
                    return;
                }

                curso.CargaHoraria = cargaHoraria;

                if (ObjetoValidator.ValidarObjeto(curso))
                {
                    cursoRepository.Atualizar(curso);

                    Console.WriteLine("\nCurso Atualizado!");
                }
            }
            else
            {
                Console.WriteLine("\nOperação cancelada!");
            }
        }

        private void Excluir()
        {
            Console.WriteLine("\nExcluir Curso\n");

            Console.Write("Digite o Id Do Curso: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var cursoRepository = new CursoRepository();
            var curso = cursoRepository.ObterPorId(id);

            if (curso == null)
            {
                Console.WriteLine("\nCurso não encontrado\n");
                return;
            }

            Console.WriteLine("\nDados do Curso");
            Console.WriteLine("\tNome: " + curso.Nome);
            Console.WriteLine("\tCargaHoraria: " + curso.CargaHoraria);

            Console.Write("\nDeseja excluir esse curso? (S/N): ");
            var resposta = Console.ReadLine() ?? string.Empty;

            if (resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    cursoRepository.Excluir(curso.IdCurso);

                    Console.WriteLine("\nCurso Excluído!");
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    Console.WriteLine("\nNão é possível excluir este curso, pois ele possui pelo menos uma turma cadastrada.");
                }
            }
            else
            {
                Console.WriteLine("\nOperação cancelada!");
            }
        }

        private void Consultar()
        {
            Console.WriteLine("\nConsultar Cursos\n");

            var cursoRepository = new CursoRepository();
            var cursos = cursoRepository.ObterTodos();

            if (cursos.Count == 0)
            {
                Console.WriteLine("Nenhum Curso Cadastrado");
                return;
            }

            foreach (var curso in cursos)
            {
                Console.WriteLine($"\nDados do Curso: Id: {curso.IdCurso} Nome: {curso.Nome} Carga Horária: {curso.CargaHoraria}");
            }
        }

    }
}