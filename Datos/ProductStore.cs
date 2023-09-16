using ProductsChona.Models.Dto;

namespace ProductsChona.Datos
{
    public static class ProductStore
    {
        public static List<ProductDto> ProductList = new List<ProductDto>
        {
            new() {Id=1, Name="Patin", Price=100},
            new() {Id=2, Name="Media", Price=50},
        };
    }
}