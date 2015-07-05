using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Biblioteca.Data
{
    [Table("Emprestimo")]
    public class Emprestimo
    {
        [Key]
        [Editable(false)]
        public int IdEmprestimo { get; set; }

        public int MatriculaUsuario { get; set; }

        [Display(Name = "Exemplar")]
        public int IdExemplar { get; set; }

        [Display(Name = "Livro")]
        public int IdLivro { get; set; }

        [Display(Name = "Data Prevista")]
        public DateTime DataPrevista { get; set; }

        public DateTime DataEmprestimo
        {
            get
            {
                return _dataEmprestimo.HasValue
                   ? _dataEmprestimo.Value
                   : DateTime.Now;
            }

            set { _dataEmprestimo = value; }
        }

        private DateTime? _dataEmprestimo;

        [Display(Name = "Date de Devolução")]
        public DateTime? DataDevolucao { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Valor da multa")]
        public decimal? ValorMulta { get; set; }

        public virtual Exemplar Exemplar { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
