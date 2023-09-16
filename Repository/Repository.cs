// clase que implementara toda la interfaz Irepositorio

// "Repositorio Genérico". Este patrón se utiliza comúnmente en aplicaciones que utilizan una capa de acceso a datos
//  para abstraer la lógica de acceso a la base de datos y proporcionar una interfaz consistente para interactuar con diferentes entidades.
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductsChona.Datos;
using ProductsChona.Repository.IRepository;

namespace ProductsChona.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); // inicializa dbSet para que haga referencia al DbSet correspondiente a la entidad T
        }
        public async Task Create(T entidad)
        {
            await dbSet.AddAsync(entidad);
            await Save();
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro, bool tracked)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> filtro)
        {
            IQueryable<T> query = dbSet;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            return await query.ToListAsync();
        }

        public async Task Remove(T entidad)
        {
            dbSet.Remove(entidad);
            await Save();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}

// En resumen, esta clase Repository<T> es una implementación genérica que servirá como base para crear repositorios específicos
// para cada entidad en tu base de datos. Los repositorios específicos implementarán los métodos de acceso a datos con la lógica real 
// para cada entidad, mientras que esta clase genérica proporciona la estructura común y la configuración de 
// DbContext necesaria para todos los repositorios.

// el update se debe separar en el repositorio de cada entidad, xq ellas tienen sus propias propiedades. 