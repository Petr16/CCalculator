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

        /// <summary>
        /// Рассчет по месяцам
        /// </summary>
        /// <param name="dataInner"></param>
        public void Calculate(DataInner dataInner)
        {
            List<Payment> payments = new List<Payment>();
            ushort months = dataInner.LoanTerm;

            //рассчитываем коэффициент аннуитента
            yearRate = dataInner.LoanRate / 100;
            monthRate = yearRate / 12;
            //koefAnnuitent= (monthRate*Math.Pow(1+monthRate,months))
            koefAnnuitent = (monthRate * DecimalEx.Pow(1 + monthRate, months)) / (DecimalEx.Pow(1 + monthRate, months) - 1);
            monthPayment = koefAnnuitent * dataInner.LoanSum;
            decimal vSumPayment = dataInner.LoanSum;
            decimal vPamentByPercent = 0;
            decimal vPamentByBody = 0;
            decimal vBalanceOwed = 0;

            for (int i = 0; i < months; i++)
            {
                Payment pay = new Payment();
                pay.DataInnerId = dataInner.Id;
                pay.PaymentDate = (DateTimeOffset.Now).AddMonths(1 + i);

                if (i == 0)
                {
                    vPamentByPercent = vSumPayment * monthRate;
                    //Основной долг
                    vBalanceOwed = monthPayment - vPamentByPercent;
                }
                else
                {
                    //остаток кредита
                    vSumPayment = vSumPayment - vBalanceOwed;
                    //Проценты
                    vPamentByPercent = vSumPayment * monthRate;
                    //Основной долг
                    vBalanceOwed = monthPayment - vPamentByPercent;
                }
                vPamentByBody = monthPayment - vPamentByPercent;

                pay.PamentByPercent = vPamentByPercent;
                pay.PaymentByBody = vPamentByBody;
                pay.BalanceOwed = vSumPayment;
                pay.Sequence = i + 1;
                payments.Add(pay);
            }

            _context.AddRange(payments);
        }

        /// <summary>
        /// Рассчет по дням
        /// </summary>
        /// <param name="dataInner"></param>
        public void CalculateByDays(DataInner dataInner)
        {
            List<Payment> payments = new List<Payment>();
            ushort months = dataInner.LoanTerm;

            //рассчитываем коэффициент аннуитента
            yearRate = dataInner.LoanRate / 100;
            monthRate = yearRate / 12;
            //koefAnnuitent= (monthRate*Math.Pow(1+monthRate,months))
            koefAnnuitent = (monthRate * DecimalEx.Pow(1 + monthRate, months)) / (DecimalEx.Pow(1 + monthRate, months) - 1);
            monthPayment = koefAnnuitent * dataInner.LoanSum;
            decimal vSumPayment = dataInner.LoanSum;
            decimal vPamentByPercent = 0;
            decimal vPamentByBody = 0;
            decimal vBalanceOwed = 0;
            Payment pay = new Payment();
            pay.DataInnerId = dataInner.Id;
            for (int i = 0; i < months; i++)
            {
                pay.PaymentDate = (DateTimeOffset.Now).AddMonths(1 + i);

                if (i == 0)
                {
                    vPamentByPercent = vSumPayment * monthRate;
                    //Основной долг
                    vBalanceOwed = monthPayment - vPamentByPercent;
                }
                else
                {
                    //остаток кредита
                    vSumPayment = vSumPayment - vBalanceOwed;
                    //Проценты
                    vPamentByPercent = vSumPayment * monthRate;
                    //Основной долг
                    vBalanceOwed = monthPayment - vPamentByPercent;
                }
                vPamentByBody = monthPayment - vPamentByPercent;

                pay.PamentByPercent = vPamentByPercent;
                pay.PaymentByBody = vPamentByBody;
                pay.BalanceOwed = vSumPayment;
                payments.Add(pay);
            }

            _context.AddRange(payments);
        }
    }
}
