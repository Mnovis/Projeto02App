using Microsoft.Data.SqlClient;
using Projeto02App.Entities;
using Projeto02App.Repositories;
using Projeto02App.Validators;

namespace Projeto02App.Services
{
    public class ProfessorService
    {
        public void ExecutarMenu()
        {
            Console.WriteLine("\n<---- COTI INFORMÁTICA ---->\n");
            Console.WriteLine("\nMenu Professor\n");

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
                Console.Clear();
                ExecutarMenu();
            }
        }

        private void Cadastrar()
        {
            Console.WriteLine("\nCadastrar Professor\n");

            var professor = new Professor();

            Console.Write("Digite o Nome: ");
            professor.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o Telefone (99) 99999-9999: ");
            professor.Telefone = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o Email: ");
            professor.Email = Console.ReadLine() ?? string.Empty;

            if (ObjetoValidator.ValidarObjeto(professor))
            {
                var professorRepository = new ProfessorRepository();
                professorRepository.Inserir(professor);

                Console.WriteLine("Professor Cadastrado!");
            }
        }

        private void Atualizar()
        {
            Console.WriteLine("\nAtualizar Professor\n");

            Console.Write("Digite o Id Do Professor: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var professorRepository = new ProfessorRepository();
            var professor = professorRepository.ObterPorId(id);

            if (professor == null)
            {
                Console.WriteLine("\nProfessor não encontrado\n");
                return;
            }

            Console.WriteLine("\nDados do Professor");
            Console.WriteLine("\tNome: " + professor.Nome);
            Console.WriteLine("\tTelefone: " + professor.Telefone);
            Console.WriteLine("\tEmail: " + professor.Email);

            Console.Write("\nEste é o professor? (S/N): ");
            var resposta = Console.ReadLine() ?? string.Empty;

            if (resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("\nInforme os novos dados: ");

                Console.Write("Informe o Nome: ");
                professor.Nome = Console.ReadLine() ?? string.Empty;

                Console.Write("Informe o Telefone (99) 99999-9999: ");
                professor.Telefone = Console.ReadLine() ?? string.Empty;

                Console.Write("Informe o Email: ");
                professor.Email = Console.ReadLine() ?? string.Empty;

                if (ObjetoValidator.ValidarObjeto(professor))
                {
                    professorRepository.Atualizar(professor);

                    Console.WriteLine("\nProfessor Atualizado!");
                }
            }
            else
            {
                Console.WriteLine("\nOperação cancelada!");
            }
        }

        private void Excluir()
        {
            Console.WriteLine("\nExcluir Professor\n");

            Console.Write("Digite o Id Do Professor: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("\nId Inválido\n");
                return;
            }

            var professorRepository = new ProfessorRepository();
            var professor = professorRepository.ObterPorId(id);

            if (professor == null)
            {
                Console.WriteLine("\nProfessor não encontrado\n");
                return;
            }

            Console.WriteLine("\nDados do Professor");
            Console.WriteLine("\tNome: " + professor.Nome);
            Console.WriteLine("\tTelefone: " + professor.Telefone);
            Console.WriteLine("\tEmail: " + professor.Email);

            Console.Write("\nDeseja excluir esse professor? (S/N): ");
            var resposta = Console.ReadLine() ?? string.Empty;

            if (resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    professorRepository.Excluir(professor.IdProfessor);

                    Console.WriteLine("\nProfessor Excluído!");
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    Console.WriteLine("\nNão é possível excluir este professor, pois ele possui pelo menos uma turma cadastrada.");
                }
            }
            else
            {
                Console.WriteLine("\nOperação cancelada!");
            }
        }

        private void Consultar()
        {
            Console.WriteLine("\nConsultar Professores\n");

            var professorRepository = new ProfessorRepository();
            var professores = professorRepository.ObterTodos();

            if (professores.Count == 0)
            {
                Console.WriteLine("Nenhum Professor Cadastrado");
                return;
            }

            foreach (var professor in professores)
            {
                Console.WriteLine($"\nDados do Professor: Id: {professor.IdProfessor} Nome: {professor.Nome} Telefone: {professor.Telefone} Email: {professor.Email}");
            }

            Console.WriteLine("Pressione uma tecla para continuar...");
            Console.ReadKey();
        }
    }
}