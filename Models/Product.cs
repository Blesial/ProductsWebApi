using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsChona.Models
{
    public class Product
    {
        [Key]
        public int Id {get;set;}
        public string  Name { get; set; }       

        [Required]
        public decimal Price { get;set; }

        [Required]
        public int Stock {get; set;}

        public string Description {get; set;}

        public string Thumbnail {get; set;}

        public DateTime FechaCreacion { get; set; }      
    }
}