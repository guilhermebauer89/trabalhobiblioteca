using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Biblioteca.Data
{
    [Table("PerfilUsuario")]
    public class PerfilUsuario
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PerfilUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }

        [Display(Name = "Número de locações")]
        public int QuantidadeLocacoes { get; set; }

        [Display(Name = "Número de cursos")]
        public int QuantidadeCursos { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Perfil")]
        public string Descricao { get; set; }

        
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
