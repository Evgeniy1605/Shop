using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Microsoft.EntityFrameworkCore;
using Shop.Data;

namespace Shop.Controllers
{
    public class Home2Controller : Controller
    {
        public List<PerchaseModel> items = new List<PerchaseModel>();

        public static List<BasketModel> BasketItem = new List<BasketModel>() { };
        public float FN { get; set; }


        /// <summary>
        /// ////////
        public List<Order> FackeData = new List<Order>();
        /// </summary>


        public PerchaseModel Montero = new PerchaseModel() { Name = "Montero", Brend = "StrikePro", Price = 20, Image = "", Discount = 0, Page = "BuyMontero" };
        public ZipbaitsOrbitModel orbit = new ZipbaitsOrbitModel() { Name = "Orbit", Brend = "Zipbaits", Image = "Malas", Discount = 0, Page = "OrbitPage" };


        public IActionResult Index()
        {

            return View();
        }


        public IActionResult basket()
        {
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

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

            FN = BasketItem.Sum(x => x.Price);
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
                FN = BasketItem.Sum(x => x.Price);
                int x = (int)FN;
                ViewData["Message"] = x.ToString();
                return View("basket", BasketItem);
            }
        }
        /// <summary>
        /// ////////////////////
        /// </summary>
        PerchaseModel malas = new PerchaseModel() { Brend = "Lucky Craft", Name = "Malas", Price = 30, Image = "Malas", Page = "Malas", Size = "90" };
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
            malas1.Price = malas.Price;
            malas1.Colour = malas.Colour;
            malas1.Name = this.malas.Name;
            malas1.Size = this.malas.Size;
            BasketItem.Add(malas1);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);
        }
        ZipbaitsOrbitModel rigge = new ZipbaitsOrbitModel() { Brend = "Zipbaits", Name = "Rigge", Page = "Rigge" };

        public IActionResult Rigge()
        {
            return View(rigge);
        }
        public IActionResult BuyRigge(ZipbaitsOrbitModel rigge)
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
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);
        }
        public IActionResult Search()
        {
            SearchModel model = new SearchModel();
            return View(model);

        }
        public IActionResult FindItem(SearchModel search)
        {
            items = _content.AllPerchaseItems.ToList();
            var result = new List<PerchaseModel>();
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
        public IActionResult SaveData(Order or)
        {
            if (or.Email ==null)
            {
                or.Email = "_";
            }
            //
            
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
            FN = BasketItem.Sum(x => x.Price);
            or.Price = (int)FN;
            

            //
            double discount  = (FN / 100) * or.Discount;

            //
            or.PriceForPerchase = FN - discount;
            _content.Add(or);
            //
            if (User.Identity.IsAuthenticated)
            {
                var cl = User.Claims.ToList();
                double NewSum = double.Parse(cl[5].ToString().Split(':')[1].Trim()) + or.PriceForPerchase;
                UserModel user = new UserModel() { Id = int.Parse(cl[0].ToString().Split(':')[1]), SumOfAllPurchases = NewSum, Email = or.Email, Name = or.Name, PhoneNumber = or.PhoneNumber, Surname = or.Sername, PassWord = cl[8].ToString().Split(':')[1].Trim() };
                _content.Update(user);
            }
            //
            //
            
            
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
        public IActionResult BuyInquisitor(PerchaseModel perchase)
        {
            if (perchase.Size == "80")
                perchase.Price = 5;
            if (perchase.Size == "110")
                perchase.Price = 7;
            else if (perchase.Size == "130")
                perchase.Price = 10;
            BasketModel basketModel = new BasketModel() { Name = inquisitor.Name, Brend = inquisitor.Brend, Colour = perchase.Colour, Discount = inquisitor.Discount, Image = inquisitor.Image, Page = "Inquisitor", Price = perchase.Price, Size = perchase.Size };
            BasketItem.Add(basketModel);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);


        }
        public PerchaseModel magallon = new PerchaseModel();
        
        public IActionResult Magallon()
        {
            var AllItemsList = _content.AllPerchaseItems.ToList();

            foreach (var item in AllItemsList)
            {
                if (item.Id == 14)
                {
                    magallon = item;
                }
            }



            return View(magallon);
        }
        public IActionResult BuyMagallon(PerchaseModel model)
        {
            var AllItemsList = _content.AllPerchaseItems.ToList();

            foreach (var item in AllItemsList)
            {
                if (item.Id == 14)
                {
                    magallon = item;
                }
            }
            var basketModel = new BasketModel() { Name = magallon.Name, Colour = model.Colour, Brend = magallon.Brend, Discount = magallon.Discount, Image = magallon.Image, Page = magallon.Page, Price = magallon.Price, Size = magallon.Size };
            BasketItem.Add(basketModel);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);
        }
        public new PerchaseModel archback = new PerchaseModel();
        public IActionResult Archback()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 15).ToList();
            archback = i[0];
            return View(archback);
        }

        public IActionResult BuyArchback(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 15).ToList();
            archback = i[0];
            archback.Colour = model.Colour;

            var basketItem = new BasketModel { Name = archback.Name, Brend = archback.Brend, Id = archback.Id, Colour = archback.Colour, Discount = archback.Discount, Price = archback.Price, Size = archback.Size, Image = archback.Image, Page = archback.Page, Type = archback.Type };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }

        public IActionResult SubmitPerchase()
        {
            if (User.Identity.IsAuthenticated)
            {
                Order order = new Order();
                order.Name = User.Identity.Name;
                var cl = User.Claims.ToList();
                order.Sername = cl[7].ToString().Split(':')[1];
                order.PhoneNumber = cl[4].ToString().Split(':')[1];
                order.Email = cl[3].ToString().Split(':')[1];
                
                double SunOfAllPesheses = double.Parse(cl[5].ToString().Split(':')[1]);
                if (SunOfAllPesheses > 50)
                    order.Discount = 3;
                else if (SunOfAllPesheses > 200)
                    order.Discount = 10;
                else if (SunOfAllPesheses > 1000)
                    order.Discount = 20;
                


                return RedirectToAction("SaveData",order);
            }
            return RedirectToAction("InputPersonalData");

        }

    }
}
