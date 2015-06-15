using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Payment.Interface;

namespace EShopDomainModel.Payment
{
    public class PaymentManager : IPaymentManager
    {
        public IPaymentService GetPaymentService(string paymentService)
        {
            throw new NotImplementedException();
        }
    }
}
