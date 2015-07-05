namespace Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PerfilUsuario")]
    public partial class PerfilUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PerfilUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }

        public int QuantidadeLocacoes { get; set; }

        public int QuantidadeCursos { get; set; }

        [Column(TypeName = "money")]
        public decimal ValorMultaDiario { get; set; }

        [Required]
        [StringLength(50)]
        public string Descricao { get; set; }

        public bool PodeSerCoordenador { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
