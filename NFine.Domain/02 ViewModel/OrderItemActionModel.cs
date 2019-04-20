using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.ViewModel
{
    public class OrderItemActionModel
    {
        public string FID { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string OrderStatus { get; set; }
        public decimal FAmount { get; set; }
        public string CreateTime { get; set; }
    }
}
