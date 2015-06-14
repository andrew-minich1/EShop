using EShop.Entities;
using EShop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Concrete
{
    public class CategoryService: IService<CategoryEntity>
    {
        private readonly IUnitOfWork uow;

        private readonly IRepository<CategoryEntity> categoryRepository;

        public CategoryService(IUnitOfWork uow, IRepository<CategoryEntity> categoryRepository)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            this.uow = uow;

            if (categoryRepository == null)
            {
                throw new ArgumentNullException("categoryRepository");
            }
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<CategoryEntity> GetAllEntities()
        {
            return categoryRepository.GetAll();
        }

        public CategoryEntity GetById(int id)
        {
            return categoryRepository.GetById(id);
        }

        public void Create(CategoryEntity entity)
        {
            if (null == entity)
            {
                throw new ArgumentNullException("Category Entity is null");
            }
            categoryRepository.Create(entity);
            uow.Commit();
        }

        public void Edit(CategoryEntity entity)
        {
            if (null == entity)
            {
                throw new ArgumentNullException("Category Entity is null");
            }
            categoryRepository.Update(entity);
            uow.Commit();
        }

        public void Delete(CategoryEntity entity)
        {
            if (null == entity)
            {
                throw new ArgumentNullException("Category Entity is null");
            }
            categoryRepository.Delete(entity);
            uow.Commit();
        }
    }
}
