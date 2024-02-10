using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCalculator.Models
{
    public class Payment
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTimeOffset PaymentDate { get; set; }

        /// <summary>
        /// Размер платежа по телу
        /// </summary>
        public decimal PaymentByBody {  get; set; }

        /// <summary>
        /// Размер платежа по %
        /// </summary>
        public decimal PaymentByPercent { get; set; }

        /// <summary>
        /// Остаток основного долга
        /// </summary>
        public decimal BalanceOwed { get; set; }
        

        public int Sequence { get; set; }

        /// <summary>
        /// Он же и номер платежа - id в таблице DataInners (foreignKey)
        /// </summary>
        public int DataInnerId { get; set; }
        public DataInner? DataInner { get; set; }

    }
}
