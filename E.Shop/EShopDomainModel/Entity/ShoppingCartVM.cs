using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Entity.Interface;

namespace EShopDomainModel.Entity
{
    public class ShoppingCartVM
    {
        public string Id { get; set; }
        public List<CartEntity> CartItems { get; set; }
        //public decimal TotalPrice { get; set; }
    }
}
