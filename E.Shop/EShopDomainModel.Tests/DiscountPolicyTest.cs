using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using EShopDomainModel.Entity;
using EShopDomainModel.Service.Interface;
using EShopDomainModel.Payment.Interface;
using EShopDomainModel.Discount;


namespace EShopDomainModel.Tests
{
    [TestClass]
    public class DiscountPolicyTest
    {
        [TestMethod]
        public void TotalPrice_Less_Then_MinTotalForDiscount()
        {
            List<CartEntity> cartItems = new List<CartEntity>();
            cartItems.Add(
                new CartEntity
                {
                    DateCreated = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Item = new ItemEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "LenovoY510p",
                        Description = "intelcore i7, geforce 755m",
                        Price = (decimal)800
                    },
                    ItemCount = 1,
                    ShoppingCartId = "CartId"
                }
            );

            DiscountPolicy discountPolicy = new DiscountPolicy();

            var totalExpected = cartItems.Sum<CartEntity>(x => x.Item.Price * x.ItemCount);

            var totalActual = discountPolicy.UseDiscounts(cartItems);

            Assert.AreEqual(totalExpected, totalActual);
        }

        [TestMethod]
        public void Discount_Cant_Be_Greater_Then_MaxDiscount()
        {
            List<CartEntity> cartItems = new List<CartEntity>();
            cartItems.Add(
                new CartEntity
                {
                    DateCreated = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Item = new ItemEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "LenovoY510p",
                        Description = "intelcore i7, geforce 755m",
                        Price = (decimal)800
                    },
                    ItemCount = 100,
                    ShoppingCartId = "CartId"
                }
            ); 
            cartItems.Add(
                 new CartEntity
                 {
                     DateCreated = DateTime.Now,
                     Id = Guid.NewGuid(),
                     Item = new ItemEntity
                     {
                         Id = Guid.NewGuid(),
                         Name = "LenovoY810p",
                         Description = "intelcore i7, geforce 860m",
                         Price = (decimal)1200
                     },
                     ItemCount = 100,
                     ShoppingCartId = "CartId"
                 }
             );

            DiscountPolicy discountPolicy = new DiscountPolicy();
            var totalExpected = cartItems.Sum<CartEntity>(x => x.Item.Price * x.ItemCount) * (decimal) 0.7;

            var totalActual = discountPolicy.UseDiscounts(cartItems);

            Assert.AreEqual(totalExpected, totalActual);
        }

        [TestMethod]
        public void Discount_Raising_Test()
        {
            List<CartEntity> cartItems = new List<CartEntity>();
            cartItems.Add(
                new CartEntity
                {
                    DateCreated = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Item = new ItemEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = "Visual Studio Ultimate Key",
                        Description = "for 1 year",
                        Price = (decimal)100
                    },
                    ItemCount = 10,
                    ShoppingCartId = "CartId"
                }
            );
            cartItems.Add(
                 new CartEntity
                 {
                     DateCreated = DateTime.Now,
                     Id = Guid.NewGuid(),
                     Item = new ItemEntity
                     {
                         Id = Guid.NewGuid(),
                         Name = "Kaspersky Internet Security Key",
                         Description = "for 1 year",
                         Price = (decimal)50
                     },
                     ItemCount = 11,
                     ShoppingCartId = "CartId"
                 }
             );

            DiscountPolicy discountPolicy = new DiscountPolicy();
            var totalExpected = cartItems.Sum<CartEntity>(x => x.Item.Price * x.ItemCount) * (decimal)0.9;

            var totalActual = discountPolicy.UseDiscounts(cartItems);

            Assert.AreEqual(totalExpected, totalActual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null_Item_In_Cart()
        {
            List<CartEntity> cartItems = null;
            DiscountPolicy discountPolicy = new DiscountPolicy();

            var totalActual = discountPolicy.UseDiscounts(cartItems);
        }
    }
}
