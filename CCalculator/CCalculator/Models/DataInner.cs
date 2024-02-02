using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCalculator.Models
{
    //[PrimaryKey(nameof(Id))]
    public class DataInner
    {
        /// <summary>
        /// Он же и номер платежа
        /// </summary>
        [Key]
        public int Id { get; set; }


        //[Column(TypeName = "decimal(18,9)")]
        /// <summary>
        /// Сумма займа
        /// </summary>
        public decimal LoanSum { get; set; }

        /// <summary>
        /// Срок займа (в месяцах) (0 - 65535)
        /// </summary>
        public ushort LoanTerm { get; set; }

        //[Column(TypeName = "decimal(18,9)")]
        /// <summary>
        /// Ставка
        /// </summary>
        public decimal LoanRate { get; set;}

        
        public List<Payment>? Payments { get; set; }
    }
}
