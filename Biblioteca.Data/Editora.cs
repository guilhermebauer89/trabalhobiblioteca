using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Biblioteca.Data
{
    [Table("Editora")]
    public class Editora
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Editora()
        {
            Livro = new HashSet<Livro>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Nome Editora")]
        public string Nome { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Livro> Livro { get; set; }
    }
}
