using System.Linq.Expressions;

namespace ProductsChona.Repository.IRepository
{
    // interfaz de repositorio generico! a medida que agregamos modelos , todos pueden usarlo

    // con el T y where T : class. significa que esta interfaz recibe cualquier entidad con la q vayamos a trabajar 
    public interface IRepository<T> where T : class
    {
        Task Create(T entidad);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null);
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked=true); // ver tema tracking. si usa o no asNoTracking();

        Task Remove(T entidad);

        Task Save();
    }
}