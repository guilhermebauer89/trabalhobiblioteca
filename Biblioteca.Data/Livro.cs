using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Biblioteca.Data
{
    [Table("Livro")]
    public class Livro
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Livro()
        {
            Exemplar = new HashSet<Exemplar>();
        }

        public int Id { get; set; }

        [Display(Name = "Autor")]
        public int IdAutor { get; set; }

        [Display(Name = "Editora")]
        public int IdEditora { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        public virtual Autor Autor { get; set; }

        public virtual Editora Editora { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exemplar> Exemplar { get; set; }
    }
}
