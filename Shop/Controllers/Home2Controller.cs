using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Shop.Controllers
{
    public class Home2Controller : Controller
    {
        public List<PerchaseModel> items = new List<PerchaseModel>();
        
        public static List<BasketModel> BasketItem = new List<BasketModel>() {};
        public float FN { get; set; }


        /// <summary>
        /// ////////
        public List<Order> FackeData = new List<Order> ();
        /// </summary>

        
        public PerchaseModel Montero = new PerchaseModel() { Name = "Montero", Brend = "StrikePro", Price = 20, Image = "", Discount = 0, Page = "BuyMontero" };
        public ZipbaitsOrbitModel orbit = new ZipbaitsOrbitModel() { Name = "Orbit", Brend = "Zipbaits", Image = "Malas", Discount = 0, Page = "OrbitPage" };

        
        public IActionResult Index()
        {


            return View();
        }

        public IActionResult basket()
        {

            return View(BasketItem);
        }
        public IActionResult AllItems()
        {
            items = _content.AllPerchaseItems.ToList();
            return View(items);
        }
        public IActionResult BuyMontero()
        {
            BasketModel montero = new BasketModel();
            montero.Brend = Montero.Brend;
            montero.Name = Montero.Name;
            montero.Price = Montero.Price;
            montero.Discount = Montero.Discount;
            BasketItem.Add(montero);

            foreach (var item in BasketItem)
            {
                FN += item.Price;
            }
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        public IActionResult OrbitPage()
        {
            return View(orbit);
        }



        public IActionResult BuyOrbit(ZipbaitsOrbitModel orbit)
        {
            BasketModel orbit1 = new BasketModel();
            orbit1.Brend = this.orbit.Brend;
            orbit1.Name = this.orbit.Name;
            orbit1.Price = this.orbit.Price;
            orbit1.Discount = this.orbit.Discount;

            ///
            int counter = 0;
            if (orbit.Size80 == true)
                counter++;
            if (orbit.Size110 == true)
                counter++;
            if (orbit.Size130 == true)
                counter++;
            if (counter == 0)
                return View("SizeEmpty");
            else if (counter > 1)
                return View("TwoSizeError");

            else
            {
                if (orbit.Size80 = true)
                {
                    orbit1.Price = 50;
                }


                if (orbit.Size110 == true)
                {
                    orbit1.Price = 55;
                }

                else
                {
                    orbit1.Price = 60;

                }
                BasketItem.Add(orbit1);
                foreach (var item in BasketItem)
                {
                    FN += item.Price;
                }
                int x = (int)FN;
                ViewData["Message"] = x.ToString();
                return View("basket", BasketItem);
            }
        }
/// <summary>
/// ////////////////////
/// </summary>
        PerchaseModel malas = new PerchaseModel() { Brend = "Lucky Craft", Name ="Malas", Price = 30, Image = "Malas", Page = "Malas", Size = "90" };
        public IActionResult Malas()
        {
            return View(malas);
        }

        public IActionResult BuyMalas(PerchaseModel malas)
        {
            if (malas.Colour == "col-1")
                malas.Price = 20;
            else
                malas.Price = 10;
            BasketModel malas1 = new BasketModel();
            malas1.Brend = this.malas.Brend;
            malas1.Price= malas.Price;
            malas1.Colour = malas.Colour;
            malas1.Name = this.malas.Name;
            malas1.Size = this.malas.Size;
            BasketItem.Add(malas1);
            foreach (var item in BasketItem)
            {
                FN += item.Price;
            }
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);
        }
        ZipbaitsOrbitModel rigge = new ZipbaitsOrbitModel() { Brend = "Zipbaits", Name = "Rigge", Page = "Rigge" };

        public IActionResult Rigge()
        {
            return View(rigge);
        }
        public IActionResult BuyRigge (ZipbaitsOrbitModel rigge)
        {
            if (rigge.Size == "30")
                rigge.Price = 10;
            else if (rigge.Size == "90")
                rigge.Price = 15;
            else if (rigge.Size == "110")
                rigge.Price = 17;

            BasketModel rigge1 = new BasketModel();
            rigge1.Brend = this.rigge.Brend;
            rigge1.Name = this.rigge.Name;
            rigge1.Price = rigge.Price;
            rigge1.Size = rigge.Size;
            rigge1.Colour = rigge.Colour;
            BasketItem.Add(rigge1);
            foreach (var item in BasketItem)
            {
                FN += item.Price;
            }
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);
        }
        public IActionResult Search()
        {
            SearchModel model = new SearchModel();
            return View(model);

        }
        public IActionResult FindItem (SearchModel search)
        {
            items = _content.AllPerchaseItems.ToList();
            var result = new List<PerchaseModel> ();
            result = items.Where(x => x.Name.Contains(search.Input)).ToList();
            return View(result);
        }
        internal string LOfItem;
        public Order orders = new Order();
        public IActionResult InputPersonalData(Order order)
        {
            
            return View(order);
        }
        private readonly OrderDbConenction _content;
        public Home2Controller(OrderDbConenction content)
        {
            _content = content;
        }
        public IActionResult SaveData (Order or)
        {
            foreach (var item in BasketItem)
            {
                LOfItem = "";
                LOfItem += item.Brend + " " + item.Name + "Colour: " + item.Colour + "Size: " + item.Size + "|";
                or.BasketList += LOfItem;
            }

            LOfItem = or.BasketList;
            this.orders.BasketList = or.BasketList;
            this.orders.BasketList = or.BasketList;
            /////
            foreach (var item in BasketItem)
            {
                FN += item.Price;
            }
            or.Price = (int)FN;
            _content.Add(or);
            _content.SaveChanges();
            return View("ThankYou");
        }
        public IActionResult Try()
        {
            
            return View();
        }

        private PerchaseModel inquisitor = new PerchaseModel() { Brend = "Strike pro", Name = "Inquisitor", Discount = 0 };
        public IActionResult Inquisitor()
        {
            return View(inquisitor);
        }
        public IActionResult BuyInquisitor (PerchaseModel perchase)
        {
            if (perchase.Size == "80")
                perchase.Price = 5;
            if (perchase.Size == "110")
                perchase.Price = 7;
            else if (perchase.Size == "130")
                perchase.Price = 10;
            BasketModel basketModel = new BasketModel() { Name = inquisitor.Name, Brend = inquisitor.Brend, Colour = perchase.Colour, Discount = inquisitor.Discount, Image = inquisitor.Image, Page = "Inquisitor", Price = perchase.Price, Size = perchase.Size};
            BasketItem.Add(basketModel);
            foreach (var item in BasketItem)
            {
                FN += item.Price;
            }
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);

            
        }
    }

}
