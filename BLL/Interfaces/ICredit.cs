using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ICredit
    {
        string CreditCard { get; set; }
        double CreditToRepay { get; set; }
    }
}
