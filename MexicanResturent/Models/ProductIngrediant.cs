namespace MexicanResturent.Models
{
    public class ProductIngrediant
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int IngrediantId { get; set; }
        public Ingrediant? Ingrediant { get; set; }
    }
}