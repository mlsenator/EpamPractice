using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Entity.Interface;

namespace EShopDomainModel.Entity
{
    public class CategoryEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
