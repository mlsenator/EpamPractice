using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Discount.Interface;
using EShopDomainModel.Entity;

namespace EShopDomainModel.Discount
{
    public class UserDiscount : IDiscount
    {
        public string Name { get; set; }
        public UserDiscount (string discountName)
        {
            this.Name = discountName;
        }
        public decimal GetDiscount(List<CartEntity> cartItems)
        {
            var userName = cartItems.First().ShoppingCartId;
            throw new NotImplementedException();
        }
    }
}
