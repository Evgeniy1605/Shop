namespace Shop.Models
{
    public class BasketModel:PerchaseModel
    {
        public string Name { get; set; }
        public string Brend { get; set; }
        public float Price { get; set; }
        public int Discount { get; set; }
        public string Image { get; set; }

    }
}
