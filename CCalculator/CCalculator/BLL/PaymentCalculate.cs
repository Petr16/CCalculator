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
        /// Ежешаговый платеж (в дн.)
        /// </summary>
        private decimal stepPayment { get; set; }
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
        /// <summary>
        /// Дневная процентная ставка
        /// </summary>
        private decimal dayRate {  get; set; }

        private decimal TotalSumPayment { get; set; }
        private decimal TotalSumPaymentByPercent { get; set; }
        private decimal TotalSumPaymentByBody { get; set; }


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
            decimal vPaymentByPercent = 0;
            decimal vPaymentByBody = 0;
            decimal vBalanceOwed = 0;

            for (int i = 0; i < months; i++)
            {
                Payment pay = new Payment();
                pay.DataInnerId = dataInner.Id;
                pay.PaymentDate = (DateTimeOffset.Now).AddMonths(1 + i);

                if (i == 0)
                {
                    vPaymentByPercent = vSumPayment * monthRate;
                    //Основной долг
                    vBalanceOwed = monthPayment - vPaymentByPercent;
                }
                else
                {
                    //остаток кредита
                    vSumPayment = vSumPayment - vBalanceOwed;
                    //Проценты
                    vPaymentByPercent = vSumPayment * monthRate;
                    //Основной долг
                    vBalanceOwed = monthPayment - vPaymentByPercent;
                }
                vPaymentByBody = monthPayment - vPaymentByPercent;

                TotalSumPaymentByPercent += vPaymentByPercent;
                pay.PaymentByPercent = vPaymentByPercent;

                TotalSumPaymentByBody += vPaymentByBody;
                pay.PaymentByBody = vPaymentByBody;

                pay.BalanceOwed = vSumPayment;
                pay.Sequence = i + 1;
                payments.Add(pay);
            }
            dataInner.TotalSumPayment = payments.Where(p => p.DataInnerId == dataInner.Id).Sum(a => a.PaymentByBody) + payments.Where(p => p.DataInnerId==dataInner.Id).Sum(a => a.PaymentByPercent);
            dataInner.TotalSumPaymentByPercent = TotalSumPaymentByPercent;
            dataInner.TotalSumPaymentByBody = TotalSumPaymentByBody;
            _context.AddRange(payments);
        }

        /// <summary>
        /// Рассчет по дням
        /// </summary>
        /// <param name="dataInner"></param>
        public void CalculateByDays(DataInner dataInner)
        {
            List<Payment> payments = new List<Payment>();
            //срок займа в днях
            ushort days = dataInner.LoanTerm;
            //рассчитываем коэффициент аннуитента
            //Дневная ставка
            dayRate = dataInner.LoanRate / 100;
            //рассчитаем общее количество платежей
            int countPayments = (int)Math.Ceiling((decimal)days / dataInner.StepPayment);
            //ежешаговый коэффициент
            koefAnnuitent = (dayRate * DecimalEx.Pow(1 + dayRate, countPayments)) / (DecimalEx.Pow(1 + dayRate, countPayments) - 1);
            //ежешаговый платеж
            stepPayment = koefAnnuitent * dataInner.LoanSum;
            decimal vSumPayment = dataInner.LoanSum;
            //номер дня по счету шага
            int vStepNum = 0;
            //остаток количества дней платежа, например сумма=10, шаг=4, останется 2 дня для последнего платежа
            int vRemainder = days % dataInner.StepPayment;
            decimal vPaymentByPercent = 0;
            decimal vPaymentByBody = 0;
            decimal vBalanceOwed = 0;
            int rowNum = 1;
            for (int i = 1; i <= countPayments; i++)
            {
                Payment pay = new Payment();
                pay.DataInnerId = dataInner.Id;
                vStepNum = (i < countPayments) ? (dataInner.StepPayment * i) : (dataInner.StepPayment * i)+vRemainder;
                pay.PaymentDate = (DateTimeOffset.Now).AddDays(vStepNum);

                decimal vvPaymentByPercent = 0;
                decimal vvPaymentByBody = 0;
                
                /*for (int ii = rowNum; ii <= vStepNum; ii++)
                {*/
                    if (i == 1 /*&& ii==1*/)
                    {
                        vPaymentByPercent = vSumPayment * dayRate;
                        //Основной долг
                        vBalanceOwed = stepPayment - vPaymentByPercent;
                    }
                    else
                    {
                        //остаток кредита
                        vSumPayment = vSumPayment - vBalanceOwed;
                        //Проценты
                        vPaymentByPercent = vSumPayment * dayRate;
                        //Основной долг
                        vBalanceOwed = stepPayment - vPaymentByPercent;
                    }
                    vPaymentByBody = stepPayment - vPaymentByPercent;

                    /*vvPaymentByPercent += vPaymentByPercent;
                    vvPaymentByBody += vPaymentByBody;*/
                    rowNum++;
                /* }*/


                TotalSumPaymentByPercent += vPaymentByPercent;
                pay.PaymentByPercent = vPaymentByPercent;

                TotalSumPaymentByBody += vPaymentByBody;
                pay.PaymentByBody = vPaymentByBody;

                pay.BalanceOwed = vSumPayment;
                pay.Sequence = i;
                payments.Add(pay);
            }

            dataInner.TotalSumPayment = payments.Where(p => p.DataInnerId == dataInner.Id).Sum(a => a.PaymentByBody) + payments.Where(p => p.DataInnerId == dataInner.Id).Sum(a => a.PaymentByPercent);
            dataInner.TotalSumPaymentByPercent = TotalSumPaymentByPercent;
            dataInner.TotalSumPaymentByBody = TotalSumPaymentByBody;

            _context.AddRange(payments);
        }
    }
}
