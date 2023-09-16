
using Microsoft.EntityFrameworkCore;
using ProductsChona.Models;

namespace ProductsChona.Datos

// ApplicationDbContext es una clase que actúa como un contexto de base de datos. Un contexto de base de datos es una clase que permite
//  interactuar con una base de datos SQL utilizando Entity Framework Core.
// El constructor ApplicationDbContext toma un parámetro de tipo DbContextOptions<ApplicationDbContext> options. 
// Este parámetro es proporcionado por la inyección de dependencias en ASP.NET 
// Core y se utiliza para configurar la conexión a la base de datos y otras opciones relacionadas con el contexto.
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) // :base llama al constructor de la clase base DbContext con las opciones proporcionadas. Esto inicializa el contexto de la base de datos con la configuración adecuada.
        {
            
        }  
        public DbSet<Product> Products {get; set;}
    }
}