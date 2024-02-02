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
            yearRate = dataInner.LoanRate / 100;
            monthRate = yearRate / 12;
            //koefAnnuitent= (monthRate*Math.Pow(1+monthRate,months))
            koefAnnuitent = (monthRate * DecimalEx.Pow(1 + monthRate, months))/(DecimalEx.Pow(1+monthRate,months)-1);
            monthPayment = koefAnnuitent * dataInner.LoanSum;
            decimal vSumPayment = dataInner.LoanSum;
            for (int i = 0; i < months; i++) 
            {
                Payment pay = new Payment();
                pay.DataInnerId = dataInner.Id;
                pay.PaymentDate = (DateTimeOffset.Now).AddMonths(1+i);
                
                
                if (i == 0)
                {
                    pay.BalanceOwed = vSumPayment;
                    pay.PamentByPercent = vSumPayment * monthRate;
                    
                }
                else//////////////////TODO
                {
                    pay.BalanceOwed=vSumPayment-(monthPayment*(0+i));
                    pay.PamentByPercent=(vSumPayment-monthPayment)*monthRate;
                }
                pay.PaymentByBody = monthPayment - pay.PamentByPercent;
                payments.Add(pay);
            }

            _context.AddRange(payments);
        }
    }
}
