using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using mvc_lives.Models.Enum;

namespace mvc_lives.Models
{
    [Table("Inscricao")]
    public class Inscricao
    {
        [Key]
        [Display(Name = "InscricaoID")]
        [Column("InscricaoID")]
        public int InscricaoID { get; set; }

        [ForeignKey("Inscrito")]
        [Display(Name = "InscritoID")]
        [Column("InscritoID")]
        public int InscritoID { get; set; }

        [ForeignKey("Live")]
        [Display(Name = "LiveID")]
        [Column("LiveID")]
        public int LiveID { get; set; }

        [NotMapped]
        [Column("Live")]
        [Display(Name = "Live")]
        public Live? Live { get; set; }

        [Column("ValorInscricao")]
        [Display(Name = "ValorInscricao")]
        public decimal ValorInscricao { get; set; }
        
        [NotMapped]
        [Column("Inscrito")]
        [Display(Name = "Inscrito")]
        public Inscrito? Inscrito { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Column("DataVencimento")]
        [Display(Name = "DataVencimento")]
        public DateTime? DataVencimento { get; set; }

        [Column("StatusPagamento")]
        [Display(Name = "Status Pagamento")]
        private StatusPagmtoEnum _statusPagamento;

        public StatusPagmtoEnum StatusPagamento
        {
            get { return _statusPagamento; }
            set { _statusPagamento = value; }
        }

        [NotMapped]
        public string StatusPagamentoDescription => _statusPagamento.GetDescription();

        [NotMapped]
        public IEnumerable<Inscrito>? Inscritos { get; set; }

        [NotMapped]
        public List<Live>? Lives { get; set; }
    }
}
