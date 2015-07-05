namespace Website.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Emprestimo")]
    public partial class Emprestimo
    {
        [Key]
        public int IdEmprestimo { get; set; }

        [Display(Name = "Locatário")]
        public int MatriculaUsuario { get; set; }

        [Display(Name = "Exemplar")]
        public int IdExemplar { get; set; }

        [Display(Name = "Livro")]
        public int IdLivro { get; set; }

        [Display(Name = "Data Prevista")]
        public DateTime DataPrevista { get; set; }

        [Display(Name = "Data Devolução")]
        public DateTime? DataDevolucao { get; set; }

        [Display(Name = "Valor Multa")]
        [Column(TypeName = "money")]
        public decimal? ValorMulta { get; set; }

        public virtual Exemplar Exemplar { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
