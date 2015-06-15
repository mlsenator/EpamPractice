using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Entity;

namespace EShopDomainModel.Discount.Interface
{
    public interface IDiscount
    {
        string Name { get; set; }
        decimal GetDiscount(List<CartEntity> cartItems);
    }
}
