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
        [CustomValidationAttribute(ErrorMessage = "Значение должно быть целым и больше 0.")] //если в ushort падает значение с плавающей запятой, то атрибуты валидации игнорируются, но в итоге ошибка будет типа такой "The value '5.5' is not valid for LoanTerm."
        //[IntegerValidationAttribute(ErrorMessage ="Значение должно быть целым.")]
        /// <summary>
        /// Срок займа (в месяцах) (0 - 65535)
        /// </summary>
        public ushort LoanTerm { get; set; }

        [Required(ErrorMessage = "Поле 'Процентная ставка' обязательно для заполнения.")]
        [CustomValidationAttribute(ErrorMessage = "Значение должно быть больше 0.")]
        //[Column(TypeName = "decimal(18,9)")]
        /// <summary>
        /// Процентная ставка
        /// </summary>
        public decimal LoanRate { get; set;}

        
        public List<Payment>? Payments { get; set; }
    }
}
