using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Biblioteca.Data
{
    [Table("Unidade")]
    public sealed partial class Unidade
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Unidade()
        {
            Curso = new HashSet<Curso>();
            Exemplar = new HashSet<Exemplar>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Unidade")]
        public string Nome { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Curso> Curso { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Exemplar> Exemplar { get; set; }
    }
}
