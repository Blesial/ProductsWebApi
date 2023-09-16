using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsChona.Datos;
using ProductsChona.Models;
using ProductsChona.Models.Dto;

namespace ProductsChona.Controllers

{
    [ApiController]
    [Route("products/Controller")]
    public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly ApplicationDbContext _db;
    public ProductController(ILogger<ProductController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    
        [HttpGet]
        [ProducesResponseType(200)]

// actionresult es un wrapper que nos permite devolver un valor de cualquier tipo. asi podemos usar el ok que devulve un objecto con status code!
// el action result se usa para acciones que son manejadas por el controller y devuelven un response. 
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            _logger.LogInformation("Obteniendo productos");
            return Ok(_db.Products.ToList());
        }
        
        [HttpGet("id", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]


        public ActionResult<ProductDto> GetProduct(int Id)
        {
            if (Id == 0) 
            {
                _logger.LogError("Error a traer producto con id " + Id);
                return BadRequest();
            }
            var product = _db.Products.FirstOrDefault(v=>v.Id == Id);
    
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProductDto> CreateProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(productDto);
            }
            if (productDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // productDto.Id = _db.Products.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
            Product modelo = new()
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Thumbnail = productDto.Thumbnail
            };

            _db.Products.Add(modelo); // INSERT
            _db.SaveChanges(); // GUARDAR EN BASE DE DATOS
            
            return CreatedAtRoute("GetProduct", new {id=productDto.Id}, productDto);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // aca no es necesario el action result, la interfaz no va a necesitar del modelo. siempre cuando trabajamos con deletes
        // se debe retornar un NO CONTENT. 
        public IActionResult DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var productSearch = _db.Products.FirstOrDefault(p => p.Id == id);
            if (productSearch == null)
            {
                return NotFound();
            } 

            _db.Products.Remove(productSearch);
             _db.SaveChanges(); // Guardar los cambios en la base de datos

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]


        public IActionResult UpdateProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }
            
            // var productToUpdate = _db.Products.FirstOrDefault(p => p.Id == productDto.Id);
            // productToUpdate.Name = productDto.Name;
            // productToUpdate.Price = productDto.Price;

            Product modelo = new()
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                Thumbnail = productDto.Thumbnail

            };

            _db.Products.Update(modelo);
            _db.SaveChanges();
         

            return NoContent();
        }

        [HttpPatch("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
             public IActionResult ParcialUpdateProduct(int id, JsonPatchDocument<ProductDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            // antes de modificarlo necesitaymos el registro que se va a modificar
            // el asnotracking es para que no trackee el id. porque mas abajo volvemos a hacer una copia con el mismo id. 
            var product = _db.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);


            // antes de actualizarlo lo guardamos temporalemnte antes que haya un cambio. 
            ProductDto modeloProductDto= new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Thumbnail = product.Thumbnail

            };

            if (product == null) return BadRequest();

            patchDto.ApplyTo(modeloProductDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // este ya contiene lo que se modifico, y es el que le podemos enviar al metodo update q esta dentro del dbcontext
            // porque? porque lo q tiene q ver con le db set (q hace referencia a product, solo le puedo mandar con el mismo modelo algo a actualizar)
            Product modelo = new()
            {
                Id = modeloProductDto.Id,
                Name = modeloProductDto.Name,
                Price = modeloProductDto.Price,
                Description = modeloProductDto.Description,
                Thumbnail = modeloProductDto.Thumbnail
            };

            _db.Products.Update(modelo);
            _db.SaveChanges();

            return NoContent();
        } 
    }
} 