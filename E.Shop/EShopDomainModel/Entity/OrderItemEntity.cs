using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Entity.Interface;

namespace EShopDomainModel.Entity
{
    public class OrderItemEntity : IEntity
    {
        public Guid Id { get; set; }
        public OrderEntity Order { get; set; }
        public ItemEntity Item { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
