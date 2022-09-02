using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Payment
{
    public class PaymentPostDTO
    {
        public IEnumerable<PaymentItemPostDTO> Items { get; set; }
    }
}
