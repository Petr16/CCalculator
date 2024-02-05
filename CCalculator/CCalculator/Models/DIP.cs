using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCalculator.Models
{
    public class DIP
    {
        /// <summary>
        /// Он же и номер платежа
        /// </summary>
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
        public decimal LoanRate { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTimeOffset PaymentDate { get; set; }

        /// <summary>
        /// Размер платежа по телу
        /// </summary>
        public decimal PaymentByBody { get; set; }

        /// <summary>
        /// Размер платежа по %
        /// </summary>
        public decimal PamentByPercent { get; set; }

        /// <summary>
        /// Остаток основного долга
        /// </summary>
        public decimal BalanceOwed { get; set; }

        /// <summary>
        /// Последовательность
        /// </summary>
        public int Sequence {  get; set; }
    }
}
