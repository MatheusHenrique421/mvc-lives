using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

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

        [Display(Name = "HoraInicio")]
        [Column("HoraInicio")]
        public DateTime HoraInicio { get; set; }

        [Display(Name = "DuracaoMin")]
        [Column("DuracaoMin")]
        public int DuracaoMin { get; set; }

        [Display(Name = "ValorInscricao")]
        [Column("ValorInscricao")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal ValorInscricao
        {
            get
            {
                if (decimal.TryParse(ValorInscricaoFormatado, NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
                {
                    return valor;
                }
                return 0; // ou algum valor padrão, dependendo do que faz sentido para o seu caso
            }
            set
            {
                ValorInscricaoFormatado = value.ToString("N2", CultureInfo.GetCultureInfo("pt-BR"));
            }
        }

        [NotMapped] // Não mapear isso para o banco de dados diretamente
        public string ValorInscricaoFormatado { get; set; }


        [NotMapped]
        public IEnumerable<Instrutor>? ListaDeInstrutor { get; set; }

        [NotMapped]
        public ICollection<Inscrito>? Inscritos { get; set; }

        [NotMapped]
        public ICollection<Live>? Lives { get; set; }

        // Relacionamento com inscrições
        [NotMapped]
        public ICollection<Inscricao>? Inscricoes { get; set; }

    }
}
