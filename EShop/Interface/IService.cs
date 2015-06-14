using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Interface
{

    public interface IService<TEntity>
    {
        IEnumerable<TEntity> GetAllEntities();

        TEntity GetById(int id);

        void Create(TEntity entity);

        void Edit(TEntity entity);

        void Delete(TEntity entity);

    }
}

