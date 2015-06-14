using EShop.Entities;
using EShop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Concrete
{
    public class BasketDiscountPolicy : IDiscountPolicy
    {
        private readonly IDiscount discount;

        public BasketDiscountPolicy(IDiscount discount)
        {
            if (null == discount)
                throw new ArgumentNullException("Discount is null");
            this.discount = discount;
        }
        public double CalculateDiscount(BasketEntity basket)
        {
            if (null == basket)
                throw new ArgumentNullException("Basket is null");
            if (basket.Items.Count == 0)
                return 0;
            return discount.GetDiscount(basket);
        }
    }
}
