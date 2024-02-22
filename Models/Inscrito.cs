using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mvc_lives.Models
{
    [Table("Inscrito")]
    public class Inscrito
    {
        [Key]
        [Display(Name = "InscritoID")]
        [Column("InscritoID")]
        public int InscritoID { get; set; }

        [Display(Name = "Nome")]
        [Column("Nome")]
        public string? Nome { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "DataNascimento")]
        [Column("DataNascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Email")]
        [Column("Email")]
        public string? Email { get; set; }

        [Display(Name = "EnderecoInstagram")]
        [Column("EnderecoInstagram")]
        public string? Instagram { get; set; }

        public ICollection<Inscricao>? Inscricoes { get; set; }
    }
}