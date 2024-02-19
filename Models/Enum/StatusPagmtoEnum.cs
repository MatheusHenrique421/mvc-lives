using System.ComponentModel;

namespace mvc_lives.Models.Enum
{
    public enum StatusPagmtoEnum
    {
        [Description("Pago")]
        Pago = 1,
        [Description("Não Pago")]
        NaoPago = 2
    }
}