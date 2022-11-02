using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeShop.Service.DTO.Order.Details
{
    public class BillingDetailsPostDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
