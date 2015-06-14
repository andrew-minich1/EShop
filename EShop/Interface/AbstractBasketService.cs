using EShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Interface
{
    public abstract class AbstractBasketService
    {
        public BasketEntity Basket { get; private set; }

        private readonly IDiscountPolicy discountPolicy;


        public AbstractBasketService(IDiscountPolicy discountPolicy)
        {
            if (null == discountPolicy)
                throw new ArgumentNullException("Discount policy is null");
            Basket = new BasketEntity();
            this.discountPolicy = discountPolicy;
        }

        public virtual void AddToBasket(ItemEntity entity, int quantity)
        {
            if (null == entity)
                throw new ArgumentNullException("Null entity");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("Quantity is less than 1");

            ItemInBacket current = Basket.Items.Where(item => item.Item.Id == entity.Id).FirstOrDefault();
            if (current == null)
            {
                Basket.Items.Add(new ItemInBacket
                {
                    Item = entity,
                    Quantity = quantity,
                    state = States.Odered
                });
            }
            else
            {
                current.Quantity += quantity;
            }
        }

        public virtual double GetTotalSumm()
        {
            if (Basket.Items.Count == 0)
                return 0;
            else
                return (Basket.Items.Sum(item => item.Item.Price * item.Quantity)) - GetDiscount();
        }

        public virtual double GetDiscount()
        {
            if (Basket.Items.Count == 0)
                return 0;
            return discountPolicy.CalculateDiscount(Basket);
        }

        public virtual void RemoveFromBasket(ItemEntity entity)
        {
            if (null == entity)
                throw new ArgumentNullException("Null entity");
            Basket.Items.RemoveAll(item => item.Item.Id == entity.Id);
        }

        public virtual void Empty()
        {
            Basket.Items.Clear();
        }

        public virtual void Pay(IPaymentService payment, string cartNumber)
        {
            if (null == payment)
                throw new ArgumentOutOfRangeException("Null payment service");
            if (Basket.Items.Count == 0)
                return;
            var totalSumm = GetTotalSumm();
            payment.Pay(cartNumber, totalSumm);
            Basket.Items.ForEach(item => item.state = States.InProgress);
        }
    }
}
