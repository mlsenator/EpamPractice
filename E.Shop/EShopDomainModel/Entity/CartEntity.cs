using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Entity.Interface;

namespace EShopDomainModel.Entity
{
    public class CartEntity : IEntity
    {
        public Guid Id { get; set; }
        public string ShoppingCartId { get; set; }
        public ItemEntity Item { get; set; }
        public int ItemCount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
