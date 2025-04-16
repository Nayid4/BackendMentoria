using Mentoria.Services.Mentoring.Domain.Generics;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Repositories
{
    public class GenericRepository<TID, T> : IGenericRepository<TID, T>
        where TID : IIdGeneric
        where T : EntityGeneric<TID>
    {
        protected readonly ApplicationDbContext _contexto;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _dbSet = _contexto.Set<T>();
        }

        public void Update(T entidad) => _dbSet.Update(entidad);

        public async void Create(T entidad) => await _dbSet.AddAsync(entidad);

        public void Delete(T id) => _dbSet.Remove(id);

        public async Task<T?> GetById(TID id) => await _dbSet.FindAsync(id);

        public IQueryable<T> GetAll() => _dbSet.OrderBy(t => t.CreatedAt);
    }
}
