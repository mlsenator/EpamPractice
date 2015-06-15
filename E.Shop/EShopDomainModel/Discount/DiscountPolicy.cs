using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Discount.Interface;
using EShopDomainModel.Entity;

namespace EShopDomainModel.Discount
{
    public class DiscountPolicy
    {
        readonly decimal minTotalForDiscount;
        readonly decimal discountRaiseStep;
        readonly double minDiscount;
        readonly double maxDiscount;

        public DiscountPolicy(  decimal minTotalForDiscount = 1000,
                                decimal discountRaiseStep = 500,
                                double minDiscount = 0.05,
                                double maxDiscount = 0.3
                             )
        {
            this.minTotalForDiscount = minTotalForDiscount;
            this.discountRaiseStep = discountRaiseStep;
            this.minDiscount = minDiscount;
            this.maxDiscount = maxDiscount;
        }
        public decimal UseDiscounts(List<CartEntity> cartItems)
        {
            decimal totalPrice = 0;
            try
            {
                totalPrice = cartItems.Sum<CartEntity>(x => x.Item.Price * x.ItemCount);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("Null argument in cart items", e);
            }
            if (totalPrice > minTotalForDiscount)
            {
                int stepNumber = (int)Math.Truncate((totalPrice - minTotalForDiscount) / discountRaiseStep);
                double currentDiscount = minDiscount * (stepNumber + 1);
                if (currentDiscount > maxDiscount)
                    currentDiscount = maxDiscount;
                totalPrice = totalPrice * (decimal)(1 - currentDiscount);
            }
            return totalPrice;
        }
    }
}
