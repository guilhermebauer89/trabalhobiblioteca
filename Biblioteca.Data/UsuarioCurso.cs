using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Data
{
    [Table("UsuarioCurso")]
    public class UsuarioCurso
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCurso { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MatriculaUsuario { get; set; }

        [Display(Name = "É Coordenador")]
        public bool Coordenador { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
