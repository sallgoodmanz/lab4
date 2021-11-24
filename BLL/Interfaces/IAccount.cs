using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IAccount
    {
        int AccountName { get; set; }
        double AccountBalance { get; set; }
        double AccountDepositBalance { get; set; }
       
    }
}
