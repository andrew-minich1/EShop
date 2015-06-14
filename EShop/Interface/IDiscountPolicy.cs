using EShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Interface
{
    public interface IDiscountPolicy
    {
        double CalculateDiscount(BasketEntity bascet);
    }
}
