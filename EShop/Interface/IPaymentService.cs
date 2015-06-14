using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Interface
{
    public interface IPaymentService
    {
        void Pay(string cartNumber, double summ);
    }
}
