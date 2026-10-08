namespace Projeto02App.Services
{
    public class MenuPrincipalService
    {
        public void ExecutarMenuPrincipal()
        {
            Console.WriteLine("\n<---- COTI INFORMÁTICA ---->\n");
            Console.WriteLine("\nMenu Principal\n");

            Console.WriteLine("(1) Cursos");
            Console.WriteLine("(2) Professores");
            Console.WriteLine("(3) Turmas");
            Console.WriteLine("(0) Sair");

            Console.Write("Informe a opção desejada: ");
            var opcao = int.Parse(Console.ReadLine() ?? string.Empty);

            switch (opcao)
            {
                case 1: Console.Clear();
                    break;

                case 2: Console.Clear();
                    new ProfessorService().ExecutarMenu();
                    break;

                case 3: Console.Clear();
                    break;

                case 0: Console.Clear();
                    Console.WriteLine("\nFim do Programa!\n");
                    break;

                default: Console.WriteLine("\nOpção Inválida!\n");
                    break;
            }

            if (opcao != 0)
            {
                Console.Clear();
                ExecutarMenuPrincipal();
            }
        }
    }
}
