using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ItemEntity> Items { get; set; }
    }
}
