namespace Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sername { get; set; }
        public string PhoneNumber { get; set; }
        public string BasketList { get; set; }
        public int ChosenId { get; set; }
        public int Price { get; set; }

    }
}
