using EShop.Entities;
using EShop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Concrete
{
    public class TotalValueDiscount : IDiscount
    {
        public double GetDiscount(BasketEntity basket)
        {           
            if (null == basket)
                throw new ArgumentNullException("Basket is null");
            if (basket.Items.Count == 0)
                return 0;
            var totalSumm = basket.Items.Sum(item => item.Item.Price * item.Quantity);
            if (totalSumm >= 1000)
                return totalSumm - totalSumm * 0.9;
            else return 0;
        }
    }
}
