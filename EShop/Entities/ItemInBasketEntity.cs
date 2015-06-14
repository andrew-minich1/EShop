using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Entities
{
    public class ItemInBacket
    {
        public ItemEntity Item { get; set; }

        public int Quantity { get; set; }

        public States state { get; set; }
    }

    public enum States
    {
        Odered,
        Delivered,
        InProgress
    }
}
