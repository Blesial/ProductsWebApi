using System.Linq.Expressions;

namespace ProductsChona.Repository.IRepository
{
    // interfaz de repositorio generico! a medida que agregamos modelos , todos pueden usarlo
// para abstraer el acceso a la base de datos y proporcionar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) genéricas
//  para cualquier entidad de la base de datos
    // con el T y where T : class. significa que esta interfaz recibe cualquier entidad con la q vayamos a trabajar 
    public interface IRepository<T> where T : class
    {
        Task Create(T entidad);
        Task<List<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null); // Este método se utiliza para recuperar una lista de todas las entidades de tipo T desde la base de datos. Puede aceptar un filtro opcional, que es una expresión lambda que se utiliza para filtrar las entidades recuperadas. Por ejemplo, si proporcionas un filtro, el método podría recuperar solo las entidades que cumplen con ese filtro específico. Esto es útil para realizar consultas más específicas.
        Task<T> Obtener(Expression<Func<T, bool>> filtro = null, bool tracked=true); // ver tema tracking. si usa o no asNoTracking();

        Task Remove(T entidad);

        Task Save();
    }
}

//El uso de una interfaz genérica como esta permite que otras partes de tu aplicación implementen fácilmente un repositorio
// específico para una entidad dada y utilicen estos métodos genéricos para interactuar con la base de datos sin tener que 
//escribir código de acceso a la base de datos personalizado para cada entidad.

//Por ejemplo, si tienes una entidad llamada Product, puedes crear un repositorio ProductRepository que implemente IRepository<Product>. 
//Luego, puedes usar ProductRepository para realizar operaciones CRUD en la tabla de productos de tu base de datos.