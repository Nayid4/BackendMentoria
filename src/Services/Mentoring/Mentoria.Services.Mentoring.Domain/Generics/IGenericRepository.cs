
namespace Mentoria.Services.Mentoring.Domain.Generics
{
    public interface IGenericRepository<TID, T>
        where TID : IIdGeneric
        where T : EntityGeneric<TID>
    {
        IQueryable<T> GetAll();
        Task<T?> GetById(TID id);
        void Create(T entidad);
        void Update(T entidad);
        void Delete(T id);
    }
}
