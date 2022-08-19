namespace Shop.Models
{
    public class BasketModel:PurchaseModel
    {
        public string Name { get; set; }
        public string Brend { get; set; }
        public float Price { get; set; }
        public int Discount { get; set; }
        public string Image { get; set; }

    }
}
