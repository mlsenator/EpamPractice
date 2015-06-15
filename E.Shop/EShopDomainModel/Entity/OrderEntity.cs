using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Entity.Interface;

namespace EShopDomainModel.Entity
{
    public class OrderEntity : IEntity
    {
        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public Status Status { get; set; }
    }
}
