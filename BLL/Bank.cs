using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Bank: ICredit
    {
        public double BankBalance { get; private set; }
        public string CreditCard { get; set; }
        public double CreditToRepay { get; set; }

        public Bank()
        {
            BankBalance = Math.Pow(10,10);
            CreditCard = string.Empty;
            CreditToRepay = 0;
        }
        
    }
}
