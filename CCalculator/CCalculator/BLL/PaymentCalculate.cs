using CCalculator.Data;
using CCalculator.Models;

namespace CCalculator.BLL
{
    public class PaymentCalculate
    {
        private readonly CCalculatorContext _context;

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
