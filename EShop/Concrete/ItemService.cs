using EShop.Entities;
using EShop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Concrete
{
    public class ItemService: IService<ItemEntity>
    {
        private readonly IUnitOfWork uow;

        private readonly IRepository<ItemEntity> itemRepository;

        public ItemService(IUnitOfWork uow, IRepository<ItemEntity> itemRepository)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            this.uow = uow;

            if (itemRepository == null)
            {
                throw new ArgumentNullException("itemRepository");
            }
            this.itemRepository = itemRepository;
        }

        public IEnumerable<ItemEntity> GetAllEntities()
        {
            return itemRepository.GetAll();
        }

        public ItemEntity GetById(int id)
        {
            return itemRepository.GetById(id);
        }

        public void Create(ItemEntity entity)
        {
            if (null == entity)
            {
                throw new ArgumentNullException("ItemEntity is null");
            }
            itemRepository.Create(entity);
            uow.Commit();
        }

        public void Edit(ItemEntity entity)
        {
            if (null == entity)
            {
                throw new ArgumentNullException("ItemEntity is null");
            }
            itemRepository.Update(entity);
            uow.Commit();
        }

        public void Delete(ItemEntity entity)
        {
            if (null == entity)
            {
                throw new ArgumentNullException("ItemEntity is null");
            }
            itemRepository.Delete(entity);
            uow.Commit();
        }
    }
}
