namespace Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Curso")]
    public partial class Curso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curso()
        {
            UsuarioCurso = new HashSet<UsuarioCurso>();
        }

        public int Id { get; set; }

        public int IdUnidade { get; set; }

        public int IdDepartamento { get; set; }

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        public virtual Departamento Departamento { get; set; }

        public virtual Unidade Unidade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioCurso> UsuarioCurso { get; set; }
    }
}
