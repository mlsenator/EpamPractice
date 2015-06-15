using System;
using System.Collections.Generic;
using System.Linq;
using EShopDomainModel.Entity;
using EShopDomainModel.Service.Interface;
using EShopDomainModel.Payment.Interface;
using EShopDomainModel.Discount;

namespace EShopDomainModel.Logic
{
    public class ShoppingCart
    {
        private string Id { get; set; }
        private readonly IService<CartEntity> cartService;
        private readonly IService<OrderEntity> orderService;
        private readonly IService<OrderItemEntity> orderItemService;
        public ShoppingCart (   string shoppingCartId, //for example user login, to bind concrete user to shopping cart
                                IService<CartEntity> cartService, 
                                IService<OrderEntity> orderService,
                                IService<OrderItemEntity> orderItemService)
        {
            Id = shoppingCartId;
            this.cartService = cartService;
            this.orderItemService = orderItemService;
            this.orderService = orderService;
        }

        public List<CartEntity> GetCartItems ()
        {
            return cartService.GetAll().Where(cart => cart.ShoppingCartId == Id).ToList();
        }
        public int GetCount()
        {
            return GetCartItems().Count;
        }
        private CartEntity GetCartByItem(ItemEntity item)
        {
            return GetCartItems().SingleOrDefault(cart => cart.Item.Id == item.Id);
        }
        public void AddToCart (ItemEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("Trying to add null item to the shopping cart");
            var cartItem = GetCartByItem(item);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartEntity
                {
                    DateCreated = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Item = item,
                    ItemCount = 1,
                    ShoppingCartId = Id
                };
                cartService.Add(cartItem);
            }
            else
            {
                // If item exists in the cart, 
                // then increment the quantity
                cartItem.ItemCount++;
                cartService.Edit(cartItem);
            }
        }
        public void RemoveFromCart (ItemEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("Trying to remove null item from the shopping cart");
            var cartItem = GetCartByItem(item);

            if (cartItem != null)
            {
                if (cartItem.ItemCount > 1)
                {
                    cartItem.ItemCount--;
                    cartService.Edit(cartItem);
                }
                else
                {
                    cartService.Remove(cartItem);
                }
            }
            else throw new ArgumentException("There is no such item int the shopping cart");
        }
        public void EmptyCart()
        {
            var cartItems = GetCartItems();

            foreach (var cartItem in cartItems)
            {
                cartService.Remove(cartItem);
            }
        }
        public decimal GetTotal ()
        {
            var discountPolicy = new DiscountPolicy();
            return discountPolicy.UseDiscounts(GetCartItems());
        }
        public void CreateOrder()
        {
            var order = new OrderEntity
                {
                    CreationDate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    TotalPrice = GetTotal(),
                    Status = Status.Ordered
                };

            var cartItems = GetCartItems();
            foreach (var cart in cartItems)
            {
                var orderItem = new OrderItemEntity
                {
                    Id = Guid.NewGuid(),
                    Item = cart.Item,
                    Order = order,
                    Price = cart.Item.Price,
                    Quantity = cart.ItemCount
                };

                orderItemService.Add(orderItem);
            }
            orderService.Add(order);
            EmptyCart();
        }
        public void PayOrder(IPaymentManager paymentManager, OrderEntity order, string paymentService)
        {
            try
            {
                paymentManager.GetPaymentService(paymentService).Pay(order.TotalPrice);
                order.Status = Status.Payed;
                orderService.Edit(order);
            }
            catch (Exception e)
            {
                throw new Exception("Failed in payment system", e);
            }
        }

    }
}
