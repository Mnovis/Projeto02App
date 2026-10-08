namespace Projeto02App.Entities
{
    public class Professor
    {
        public Guid IdProfessor { get; set; } = Guid.NewGuid();

        public string Nome { get; set; } = string.Empty;

        public string Telefone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
