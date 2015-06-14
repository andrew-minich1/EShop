using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EShop.Concrete;
using EShop.Interface;
using Rhino.Mocks;
using EShop.Entities;
using System.Collections.Generic;

namespace EShopTest
{
    #region TotalValueDiscountTest
    [TestClass]
    public class TotalValueDiscountTest
    {
        [TestMethod]
        public void Calculate_discount_when_summ_more_than_1000()
        {
            var basket = new BasketEntity();
            ItemEntity item1 = new ItemEntity { Price = 1000 };
            ItemEntity item2 = new ItemEntity { Price = 500 };
            basket.Items.Add(new ItemInBacket { Item = item1, Quantity = 2 });
            basket.Items.Add(new ItemInBacket { Item = item2, Quantity = 4 });
            IDiscount discount = new TotalValueDiscount();
            double result = discount.GetDiscount(basket);
            Assert.AreEqual(400, result);
        }

        [TestMethod]
        public void Calculate_discount_when_summ_less_than_1000()
        {
            var basket = new BasketEntity();
            ItemEntity item1 = new ItemEntity { Price = 100 };
            ItemEntity item2 = new ItemEntity { Price = 50 };
            basket.Items.Add(new ItemInBacket { Item = item1, Quantity = 2 });
            basket.Items.Add(new ItemInBacket { Item = item2, Quantity = 4 });
            IDiscount discount = new TotalValueDiscount();
            double result = discount.GetDiscount(basket);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Calculate_discount_when_basket_is_empty()
        {
            var basket = new BasketEntity();
            IDiscount discount = new TotalValueDiscount();
            double result = discount.GetDiscount(basket);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Basket is null")]
        public void Calculate_discount_when_basket_is_null()
        {
            BasketEntity basket = null;
            IDiscount discount = new TotalValueDiscount();
            double result = discount.GetDiscount(basket);
        }
    }

    #endregion

    #region BasketDiscountPolicyTest
    [TestClass]
    public class BasketDiscountPolicyTest
    {
        [TestMethod]
        public void Calculate_discount_if_basket_is_not_empty()
        {
            var stubDiscount = MockRepository.GenerateStub<IDiscount>();
            var basketEntity = new BasketEntity();
            var discountPolicy = new BasketDiscountPolicy(stubDiscount);

            basketEntity.Items.Add(new ItemInBacket());
            discountPolicy.CalculateDiscount(basketEntity);
            stubDiscount.AssertWasCalled(x => x.GetDiscount(basketEntity));
        }

        [TestMethod]
        public void Calculate_discount_if_basket_is_empty()
        {
            var stubDiscount = MockRepository.GenerateStub<IDiscount>();
            var basketEntity = new BasketEntity();
            var discountPolicy = new BasketDiscountPolicy(stubDiscount);
            discountPolicy.CalculateDiscount(basketEntity);
            stubDiscount.AssertWasNotCalled(x => x.GetDiscount(basketEntity));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Basket is null")]
        public void Calculate_discount_when_basket_is_null()
        {
            var stubDiscount = MockRepository.GenerateStub<IDiscount>();
            var discountPolicy = new BasketDiscountPolicy(stubDiscount);
            discountPolicy.CalculateDiscount(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Discount is null")]
        public void Calculate_discount_when_discount_is_null()
        {
            var stubBasketEntity = MockRepository.GenerateStub<BasketEntity>();
            var discountPolicy = new BasketDiscountPolicy(null);
            var result = discountPolicy.CalculateDiscount(stubBasketEntity);
        }
    }
    #endregion

    #region BasketServiceTest
    [TestClass]
    public class BasketServiceTest
    {
        [TestMethod]
        public void Add_one_Item_in_Basket()
        {
            var stubDiscountPolicy = MockRepository.GenerateStub<IDiscountPolicy>();

            AbstractBasketService bascetService = new BasketService(stubDiscountPolicy);
            ItemEntity itemEntity = new ItemEntity
            {
                Id = 1,
                Name = "Name1",
            };
            bascetService.AddToBasket(itemEntity, 1);
            Assert.ReferenceEquals(bascetService.Basket.Items[0], itemEntity);
        }

        [TestMethod]
        public void Add_quantity_Item_in_Basket()
        {
            var stubDiscountPolicy = MockRepository.GenerateStub<IDiscountPolicy>();

            AbstractBasketService bascetService = new BasketService(stubDiscountPolicy);
            ItemEntity itemEntity = new ItemEntity
            {
                Id = 1,
                Name = "Name1",
            };
            bascetService.AddToBasket(itemEntity, 1);
            bascetService.AddToBasket(itemEntity, 3);
            Assert.ReferenceEquals(bascetService.Basket.Items[0].Quantity, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Quantity is less than 1")]
        public void Add_Item_in_Basket_when_quantity_less_than_1()
        {
            var stubDiscountPolicy = MockRepository.GenerateStub<IDiscountPolicy>();

            AbstractBasketService bascetService = new BasketService(stubDiscountPolicy);
            ItemEntity itemEntity = new ItemEntity
            {
                Id = 1,
                Name = "Name1",
            };
            bascetService.AddToBasket(itemEntity, 0);
            Assert.ReferenceEquals(bascetService.Basket.Items[0], itemEntity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Quantity is less than 1")]
        public void Add_null_Item_in_Basket()
        {
            var stubDiscountPolicy = MockRepository.GenerateStub<IDiscountPolicy>();

            AbstractBasketService bascetService = new BasketService(stubDiscountPolicy);
            ItemEntity itemEntity = null;
            bascetService.AddToBasket(itemEntity, 0);
            Assert.ReferenceEquals(bascetService.Basket.Items[0], itemEntity);
        }

       

        [TestMethod]
        public void Сalculate_the_total_summ_when_Discount_is_not_null()
        {
            var stubDiscountPolicy = MockRepository.GenerateStub<IDiscountPolicy>();

            AbstractBasketService basketService = new BasketService(stubDiscountPolicy);
            ItemEntity itemEntity1 = new ItemEntity
            {
                Id = 1,
                Name = "Name1",
                Price = 50
            };
            ItemEntity itemEntity2 = new ItemEntity
            {
                Id = 2,
                Name = "Name2",
                Price = 100
            };
            basketService.AddToBasket(itemEntity1, 1);
            basketService.AddToBasket(itemEntity2, 3);
            basketService.GetTotalSumm();
            stubDiscountPolicy.AssertWasCalled(x => x.CalculateDiscount(basketService.Basket));
        }

        [TestMethod]
        public void Сalculate_the_total_summ_if_basket_is_empty()
        {
            var stubDiscountPolicy = MockRepository.GenerateStub<IDiscountPolicy>();
            AbstractBasketService bascetService = new BasketService(stubDiscountPolicy);
            double totalSum = bascetService.GetTotalSumm();
            stubDiscountPolicy.AssertWasNotCalled(x => x.CalculateDiscount(bascetService.Basket));
            Assert.AreEqual(totalSum, 0);
        }

        [TestMethod]
        public void Сlear_basket()
        {
            var stubDiscountPolicy = MockRepository.GenerateStub<IDiscountPolicy>();

            AbstractBasketService bascetService = new BasketService(stubDiscountPolicy);
            ItemEntity itemEntity1 = new ItemEntity
            {
                Id = 1,
                Name = "Name1",
                Price = 50
            };
            ItemEntity itemEntity2 = new ItemEntity
            {
                Id = 2,
                Name = "Name2",
                Price = 100
            };
            bascetService.AddToBasket(itemEntity1, 1);
            bascetService.AddToBasket(itemEntity2, 3);
            bascetService.Empty();
            Assert.AreEqual(bascetService.Basket.Items.Count, 0);
        }
    }
    #endregion

    #region CategoryServiceTest
    [TestClass]
    public class CategoryServiceTest
    {
        [TestMethod]
        public void Get_all_entities_from_repository()
        {
            var mockCategoryRepository = MockRepository.GenerateMock<IRepository<CategoryEntity>>();
            var stubUnitOfWork = MockRepository.GenerateStub<IUnitOfWork>();
            IService<CategoryEntity> service = new CategoryService(stubUnitOfWork, mockCategoryRepository);

            service.GetAllEntities();

            mockCategoryRepository.AssertWasCalled(x => x.GetAll());
        }

        [TestMethod]
        public void Get_entity_by_id_from_repository()
        {
            var mockCategoryRepository = MockRepository.GenerateMock<IRepository<CategoryEntity>>();
            var stubUnitOfWork = MockRepository.GenerateStub<IUnitOfWork>();
            var category = new CategoryEntity { Id = 3, Name = "Category1" };
            mockCategoryRepository.Stub(x => x.GetById(3)).Return(category);
            IService<CategoryEntity> service = new CategoryService(stubUnitOfWork, mockCategoryRepository);

            Assert.IsNull(mockCategoryRepository.GetById(2));
            Assert.AreSame(category, mockCategoryRepository.GetById(3));
        }
    }
    #endregion
}
