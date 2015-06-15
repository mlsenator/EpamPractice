using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopDomainModel.Entity.Interface;
namespace EShopDomainModel.Service.Interface
{
    public interface IService<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetById(Guid Id);

        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Remove(TEntity entity);
    }
}
