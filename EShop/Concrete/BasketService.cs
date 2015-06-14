using EShop.Entities;
using EShop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Concrete
{
    public class BasketService : AbstractBasketService
    {
        public BasketService(IDiscountPolicy discountPolicy)
            :base(discountPolicy)
        { }
    }
}
