using System.ComponentModel.DataAnnotations;

namespace Projeto02App.Entities
{
    public class Professor
    {
        public Guid IdProfessor { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O {0} deve conter no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O {0} deve conter no mínimo {1} caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        [RegularExpression(@"^\(\d{2}\) \d{5}-\d{4}$", ErrorMessage = "O {0} deve estar no formato (99) 99999-9999.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O {0} deve ser válido.")]
        [MaxLength(150, ErrorMessage = "O {0} deve conter no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O {0} deve conter no mínimo {1} caracteres.")]
        public string Email { get; set; } = string.Empty;
    }
}
