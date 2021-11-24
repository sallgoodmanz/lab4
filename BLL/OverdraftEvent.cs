using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class OverdraftEvent
    {
        public double MoneyToReturn { get; set; }
        
        public OverdraftEvent() { }
        public OverdraftEvent(double moneyToReturn)
        {
            MoneyToReturn = moneyToReturn;
        }
    }
}
