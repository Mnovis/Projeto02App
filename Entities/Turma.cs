using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto02App.Entities
{
    public class Turma
    {
        public Guid IdTurma { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O Nome da turma é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O {0} deve conter no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "O {0} deve conter no mínimo {1} caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "O horário é obrigatório.")]
        public string Horario { get; set; } = string.Empty;

        public Professor? Professor { get; set; }
        public Curso? Curso { get; set; }
    }
}
