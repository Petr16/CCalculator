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


        [Required(ErrorMessage = "Поле 'Сумма займа' обязательно для заполнения.")]
        [CustomValidationAttribute(ErrorMessage = "Значение должно быть больше 0.")]
        /// <summary>
        /// Сумма займа
        /// </summary>
        public decimal LoanSum { get; set; }

        [Required(ErrorMessage = "Поле 'Срок займа' обязательно для заполнения.")]
        [CustomValidationAttribute(ErrorMessage = "Значение должно быть целым и больше 0.")]
        /// <summary>
        /// Срок займа (в месяцах или днях) (0 - 65535)
        /// </summary>
        public ushort LoanTerm { get; set; }

        [Required(ErrorMessage = "Поле 'Процентная ставка' обязательно для заполнения.")]
        [CustomValidationAttribute(ErrorMessage = "Значение должно быть больше 0.")]
        //[Column(TypeName = "decimal(18,9)")]
        /// <summary>
        /// Процентная ставка
        /// </summary>
        public decimal LoanRate { get; set;}

        /// <summary>
        /// Рассчет по дням или месяцам
        /// </summary>
        public bool IsDays { get; set; }

        [Required(ErrorMessage = "Поле 'Шаг платежа' обязательно для заполнения.")]
        [CustomValidationAttribute(ErrorMessage = "Значение должно быть целым и больше 0.")]
        /// <summary>
        /// Шаг платежа (в днях)
        /// </summary>
        public int StepPayment { get; set; }


        public decimal TotalSumPayment {  get; set; }
        public decimal TotalSumPaymentByPercent { get; set; }
        public decimal TotalSumPaymentByBody { get; set; }


        public List<Payment>? Payments { get; set; }
    }
}
