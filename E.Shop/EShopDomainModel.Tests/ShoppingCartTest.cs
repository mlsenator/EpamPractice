using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using EShopDomainModel.Entity;
using EShopDomainModel.Service.Interface;
using EShopDomainModel.Payment.Interface;
using EShopDomainModel.Discount;
using EShopDomainModel.Logic;
using Rhino.Mocks;
using Rhino.Mocks.Exceptions;

namespace EShopDomainModel.Tests
{
    [TestClass]
    public class ShoppingCartTests
    {
        private readonly IService<CartEntity> cartService;
        private readonly IService<OrderEntity> orderService;
        private readonly IService<OrderItemEntity> orderItemService;
        private ShoppingCart shoppingCart;
        public ShoppingCartTests ()
        {
            cartService = MockRepository.GenerateStub<IService<CartEntity>>();
            orderItemService = MockRepository.GenerateStub<IService<OrderItemEntity>>();
            orderService = MockRepository.GenerateStub<IService<OrderEntity>>();
        }

    #region AddItemToCart
        
        [TestMethod]
        public void When_Add_To_Cart_New_Item_Should_Save_New_Cart()
        {
            //Arrange
            cartService.Stub(x => x.GetAll()).Return(new List<CartEntity>());
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);
            var Item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };

            //Act
            shoppingCart.AddToCart(Item);

            //Assert
            cartService.AssertWasCalled(x => x.Add(Arg<CartEntity>.Matches(y => y.Item == Item)));
        }

        [TestMethod]
        public void When_Add_To_Cart_Existing_Item_Should_Edit_Existing_Cart()
        {
            //Arrange
            var item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };
            var cartEntity = new CartEntity
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = item,
                ItemCount = 1,
                ShoppingCartId = "CartId"
            };
            var list = new List<CartEntity>();
            list.Add(cartEntity);
            cartService.Stub(x => x.GetAll()).Return(list);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.AddToCart(item);

            //Assert
            cartService.AssertWasCalled(x => x.Edit(Arg<CartEntity>.Matches(y => y.Item.Id == item.Id)));
        }

        [TestMethod]
        public void Adding_To_Cart_Existing_Item_Increments_Quantity()
        {
            //Arrange
            var item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };
            var cartEntity = new CartEntity
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = item,
                ItemCount = 1,
                ShoppingCartId = "CartId"
            };
            var list = new List<CartEntity>();
            list.Add(cartEntity);
            cartService.Stub(x => x.GetAll()).Return(list);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.AddToCart(item);

            //Assert
            cartService.AssertWasCalled(x => x.Edit(Arg<CartEntity>.Matches(y => y.ItemCount == 2)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_To_Cart_Null_Item()
        {
            //Arrange
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.AddToCart(null);
        }

    #endregion

    #region DeleteItemFromCart
        [TestMethod]
        public void When_Remove_From_Cart_Last_Item_Should_Remove_Cart()
        {
            //Arrange
            var item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };
            var cartEntity = new CartEntity
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = item,
                ItemCount = 1,
                ShoppingCartId = "CartId"
            };
            var list = new List<CartEntity>();
            list.Add(cartEntity);
            cartService.Stub(x => x.GetAll()).Return(list);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.RemoveFromCart(item);

            //Assert
            cartService.AssertWasCalled(x => x.Remove(Arg<CartEntity>.Matches(y => y.Item.Id == item.Id)));
        }

        [TestMethod]
        public void When_Remove_From_Cart_Not_Last_Item_Should_Edit_Existing_Cart()
        {
            //Arrange
            var item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };
            var cartEntity = new CartEntity
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = item,
                ItemCount = 3,
                ShoppingCartId = "CartId"
            };
            var list = new List<CartEntity>();
            list.Add(cartEntity);
            cartService.Stub(x => x.GetAll()).Return(list);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.RemoveFromCart(item);

            //Assert
            cartService.AssertWasCalled(x => x.Edit(Arg<CartEntity>.Matches(y => y.Item.Id == item.Id)));
        }

        [TestMethod]
        public void Removing_From_Cart_Not_Last_Item_Decrements_Quantity()
        {
            //Arrange
            var item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };
            var cartEntity = new CartEntity
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = item,
                ItemCount = 3,
                ShoppingCartId = "CartId"
            };
            var list = new List<CartEntity>();
            list.Add(cartEntity);
            cartService.Stub(x => x.GetAll()).Return(list);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.RemoveFromCart(item);

            //Assert
            cartService.AssertWasCalled(x => x.Edit(Arg<CartEntity>.Matches(y => y.ItemCount == 2)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Remove_From_Cart_Item_Which_Cart_Doesnt_Contain()
        {
            cartService.Stub(x => x.GetAll()).Return(new List<CartEntity>());
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);
            var Item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };

            //Act
            shoppingCart.RemoveFromCart(Item);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Remove_From_Cart_Null_Item()
        {
            //Arrange
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.RemoveFromCart(null);
        }
    #endregion

    #region EmptyCart

    #endregion
    #region GetTotal

    #endregion

    #region CreateOrder

        [TestMethod]
        public void When_Order_Creating_Should_Add_New_OrderItems()
        {
            //Arrange
            var item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };
            var cartEntity = new CartEntity
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = item,
                ItemCount = 1,
                ShoppingCartId = "CartId"
            };
            var list = new List<CartEntity>();
            list.Add(cartEntity);
            cartService.Stub(x => x.GetAll()).Return(list);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.CreateOrder();

            //Assert
            orderItemService.AssertWasCalled(x => x.Add(Arg<OrderItemEntity>.Matches(y => y.Item == cartEntity.Item)));
        }

        [TestMethod]
        public void When_Order_Creating_Adding_New_Order_With_Status_Ordered()
        {
            //Arrange
            var item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };
            var cartEntity = new CartEntity
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = item,
                ItemCount = 1,
                ShoppingCartId = "CartId"
            };
            var list = new List<CartEntity>();
            list.Add(cartEntity);
            cartService.Stub(x => x.GetAll()).Return(list);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.CreateOrder();

            //Assert
            orderService.AssertWasCalled(x => x.Add(Arg<OrderEntity>.Matches(y => y.Status == Status.Ordered)));
        }

        [TestMethod]
        public void When_Order_Created_Should_Empty_Shopping_Cart()
        {
            //Arrange
            var item = new ItemEntity
            {
                Id = Guid.NewGuid(),
                Name = "LenovoY510p",
                Description = "intelcore i7, geforce 755m",
                Price = (decimal)800
            };
            var cartEntity = new CartEntity
            {
                DateCreated = DateTime.Now,
                Id = Guid.NewGuid(),
                Item = item,
                ItemCount = 1,
                ShoppingCartId = "CartId"
            };
            var list = new List<CartEntity>();
            list.Add(cartEntity);
            cartService.Stub(x => x.GetAll()).Return(list);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);

            //Act
            shoppingCart.CreateOrder();

            //Assert
            cartService.AssertWasCalled(x => x.Remove(Arg<CartEntity>.Matches(y => y.ShoppingCartId == "CartId")));
        }

    #endregion

    #region PayOrder

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Error_In_Payment_system_While_Paying() //Will fail after writing Payment logic
        {
            //Arrange

            var paymentManager = MockRepository.GenerateStub<IPaymentManager>();
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);
            var order = new OrderEntity
                {
                    CreationDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    TotalPrice = (decimal)100,
                    Status = Status.Ordered
                };

            //Act

            shoppingCart.PayOrder(paymentManager, order, "Visa");

        }

        [TestMethod]
        public void PayOrder_Method_Should_Call_Pay_Method()
        {
            //Arrange

            var paymentManager = MockRepository.GenerateStub<IPaymentManager>();
            var paymentService = MockRepository.GenerateStub<IPaymentService>();
            paymentManager.Stub(x => x.GetPaymentService("Visa")).Return(paymentService);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);
            var order = new OrderEntity
            {
                CreationDate = DateTime.Now,
                Id = Guid.NewGuid(),
                TotalPrice = (decimal)100,
                Status = Status.Ordered
            };

            //Act

            shoppingCart.PayOrder(paymentManager, order, "Visa");

            //Assert

            paymentService.AssertWasCalled(x => x.Pay(Arg<Decimal>.Matches(y => y == order.TotalPrice)));
        }

        [TestMethod]
        public void PayOrder_Method_Should_Change_Order_Status()
        {
            //Arrange

            var paymentManager = MockRepository.GenerateStub<IPaymentManager>();
            var paymentService = MockRepository.GenerateStub<IPaymentService>();
            paymentManager.Stub(x => x.GetPaymentService("Visa")).Return(paymentService);
            shoppingCart = new ShoppingCart("CartId", cartService, orderService, orderItemService);
            var order = new OrderEntity
            {
                CreationDate = DateTime.Now,
                Id = Guid.NewGuid(),
                TotalPrice = (decimal)100,
                Status = Status.Ordered
            };

            //Act

            shoppingCart.PayOrder(paymentManager, order, "Visa");

            //Assert

            orderService.AssertWasCalled(x => x.Edit(Arg<OrderEntity>.Matches(y => y.Status == Status.Payed)));
        }

    #endregion
    }
}
