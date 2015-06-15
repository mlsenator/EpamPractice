﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Discount.Interface;
using EShopDomainModel.Entity;

namespace EShopDomainModel.Discount
{
    public class QuantityDiscount : IDiscount
    {
        public string Name { get; set; }
        public QuantityDiscount(string discountName)
        {
            this.Name = discountName;
        }
        public decimal GetDiscount(List<CartEntity> cartItems)
        {
            throw new NotImplementedException();
        }
    }
}
