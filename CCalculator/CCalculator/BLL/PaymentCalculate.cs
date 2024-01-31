using CCalculator.Data;
using CCalculator.Models;
using DecimalMath;//возведение в степень для decimal


namespace CCalculator.BLL
{
    public class PaymentCalculate
    {
        private readonly CCalculatorContext _context;

        /// <summary>
        /// Ежемесячный аннуитентный платеж
        /// </summary>
        private decimal monthPayment { get; set; }
        /// <summary>
        /// Коэффициент аннуитента
        /// </summary>
        private decimal koefAnnuitent { get; set; }
        /// <summary>
        /// Годовая процентная ставка
        /// </summary>
        private decimal yearRate {  get; set; }
        /// <summary>
        /// Месячная процентная ставка
        /// </summary>
        private decimal monthRate { get; set; }


        public PaymentCalculate(CCalculatorContext context)
        {
            _context = context;
        }

        public void Calculate(DataInner dataInner)
        {
            //Payment p = new Payment();
            //p.DataInnerId = dataInner.Id;
            //p.PaymentDate = DateTimeOffset.Now;
            //p.PaymentByBody = 1;
            //p.PamentByPercent = 2;
            //p.BalanceOwed = 3;
            //_context.Add(p);

            List<Payment> payments = new List<Payment>();
            ushort months = dataInner.LoanTerm;

            //рассчитываем коэффициент аннуитента
            yearRate = dataInner.LoanRate / 1000;
            monthRate = yearRate / 12;
            //koefAnnuitent= (monthRate*Math.Pow(1+monthRate,months))
            koefAnnuitent = (monthRate * DecimalEx.Pow(1 + monthRate, months));

            for (int i = 1; i < months + 1; i++) 
            {
                Payment pay = new Payment();
                pay.PaymentDate = DateTimeOffset.Now;
                pay.PaymentByBody = i;
                pay.PamentByPercent = i;
                pay.BalanceOwed = i;
                payments.Add(pay);
            }

            _context.AddRange(payments);
        }
    }
}
