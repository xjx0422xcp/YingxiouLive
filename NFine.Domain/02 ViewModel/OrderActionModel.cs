using NFine.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain.ViewModel
{
   public class OrderActionModel
    {
        private ICollection<OrderItemEntity> itemList = new List<OrderItemEntity>();
        public virtual ICollection<OrderItemEntity> ItemList
        {
            get
            {
                return itemList;
            }

            set
            {
                itemList = value;
            }
        }


        public virtual OrderEntity orderEntity { get; set; }

    }
}
