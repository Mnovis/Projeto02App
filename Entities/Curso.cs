using System.ComponentModel.DataAnnotations;

namespace Projeto02App.Entities
{
    public class Curso
    {
        public Guid IdCurso { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O Nome é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O {0} deve conter no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O {0} deve conter no mínimo {1} caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int CargaHoraria { get; set; }
    }
}
