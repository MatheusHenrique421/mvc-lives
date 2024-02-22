using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvc_lives.Models
{
    [Table("Live")]
    public class Live
    {
        [Key]
        [Display(Name = "LiveID")]
        [Column("LiveID")]
        public int LiveID { get; set; }

        [ForeignKey("Instrutor")]
        [Display(Name = "InstrutorID")]
        [Column("InstrutorID")]
        public int InstrutorID { get; set; }

        [Display(Name = "Instrutor")]
        [Column("Instrutor")]
        public virtual Instrutor? Instrutor { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        public string? Nome { get; set; }

        [Display(Name = "Descricao")]
        [Column("Descicao")]
        public string? Descricao { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = false)]
        [Display(Name = "HoraInicio")]
        [Column("HoraInicio")]
        public DateTime HoraInicio { get; set; }

        [Display(Name = "DuracaoMin")]
        [Column("DuracaoMin")]
        public int DuracaoMin { get; set; }

        [Display(Name = "ValorInscricao")]
        [Column("ValorInscricao")]
        public decimal ValorInscricao { get; set; }

        [NotMapped]
        public IEnumerable<Instrutor>? ListaDeInstrutor { get; set; }

        [NotMapped]
        public ICollection<Inscrito>? Inscritos { get; set; }

        [NotMapped]
        public ICollection<Live>? Lives { get; set; }

        [NotMapped]
        public ICollection<Inscricao>? Inscricoes { get; set; }

    }
}
