using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopDomainModel.Payment.Interface
{
    public interface IPaymentService
    {
        bool Pay(decimal price);
    }
}
