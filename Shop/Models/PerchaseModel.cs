namespace Shop.Models
{
   
    public class PurchaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brend { get; set; }
        public float Price { get; set; }
        public int Discount { get; set; }
        public string Image { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public string Page { get; set; }
        public string Type { get; set; }
        public string Property { get; set; }
        public string MinMaxPrice { get; set; }




    }

    public class ZipbaitsOrbitModel : PurchaseModel
    {
        public bool Size80 { get; set; } = false;
        public bool Size110 { get; set; } = false;
        public bool Size130 { get; set; } = false;



    }
}
