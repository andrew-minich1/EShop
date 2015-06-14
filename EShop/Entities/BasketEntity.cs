using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Entities
{
    public class BasketEntity
    {
        public List<ItemInBacket> Items { get; set; }

        public BasketEntity()
        {
            Items = new List<ItemInBacket>();
        }
    }
}
