using System.ComponentModel.DataAnnotations;

namespace ProductsChona.Models.Dto
{

    // SON UNA CAPA ENTRE EL MODELO Y LO QUE DEVUELVE LA API. PARA EXPONER UNICAMENTE LO QUE SE QUIERE, POR EJ NO EXPONER FECHA DE CREACION. 
// SOLO PROPS QUE QUIERO. 
    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]

        public decimal Price { get;set; }
        [Required]

        public string Description {get; set;}

        public string Thumbnail {get; set;}

    }
} 