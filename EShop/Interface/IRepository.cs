using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Interface
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int key);

        void Create(TEntity e);

        void Delete(TEntity e);

        void Update(TEntity entity);
    }
}
