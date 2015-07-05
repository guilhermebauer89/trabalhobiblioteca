using Biblioteca.Data;

namespace Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Livro")]
    public partial class Livro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Livro()
        {
            Exemplar = new HashSet<Exemplar>();
        }

        public int Id { get; set; }

        public int IdAutor { get; set; }

        public int IdEditora { get; set; }

        [Required]
        [StringLength(30)]
        public string Titulo { get; set; }

        public virtual Autor Autor { get; set; }

        public virtual Editora Editora { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exemplar> Exemplar { get; set; }
    }
}
