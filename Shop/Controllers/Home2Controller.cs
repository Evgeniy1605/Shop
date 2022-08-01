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
            items = _content.AllPerchaseItems.ToList();
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
                LOfItem += item.Brend + " " + item.Name + "Colour: " + item.Colour + "Size: " + item.Size + "  Model:"+ item.Property + "|";
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
            BasketItem.RemoveRange(0,BasketItem.Count);
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
            var AllItemsList = _content.AllPerchaseItems.Where(x => x.Name == "Tiny Magallon").ToList();

            magallon = AllItemsList[0];
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
                if (SunOfAllPesheses < 10)
                    order.Discount = 0;

                else if (SunOfAllPesheses > 50 && SunOfAllPesheses < 200)
                {
                    order.Discount = 3;
                }

                else if (SunOfAllPesheses > 200 && SunOfAllPesheses < 1000)
                {
                    order.Discount = 10;
                }


                else if (SunOfAllPesheses > 1000)
                {
                    order.Discount = 20;
                }



                return RedirectToAction("SaveData",order);
            }
            return RedirectToAction("InputPersonalData");

        }
        [HttpPost("FindItem")]
        public IActionResult FindItem2(string Find)
        {
            items = _content.AllPerchaseItems.ToList();
            var result = new List<PerchaseModel>();
            string Find2 = "?";
            if (Find !=null)
            {
                Find2 = Find.ToString();
            }
                

            result = items.Where(x => x.Name.Contains(Find2)).ToList();
            return View(result);
            
        }
        
        public IActionResult Voblers()
        {
            items = _content.AllPerchaseItems.ToList();
            var result = new List<PerchaseModel>();
           
            result = items.Where(x => x.Type == "Vobler").ToList();
            return View(result);
            
        }
        public PerchaseModel tiro = new PerchaseModel();
        public IActionResult Tiro()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 16).ToList();
            tiro = i[0];
            return View(tiro);
        }
        public IActionResult BuyTiro(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 16).ToList();
            tiro = i[0];
            switch (model.Property)
            {
                case "GONTS 762L":
                    tiro.Price = 250;
                    tiro.Size = "76";
                    break;
                case "GONTS-762M":
                    tiro.Price = 300;
                    tiro.Size = "76";
                    break;
                case "GONTS-792ML":
                    tiro.Price = 320;
                    tiro.Size = "79";
                    break;
            }

            var basketItem = new BasketModel { Name = tiro.Name, Brend = tiro.Brend, Id = tiro.Id, Colour = tiro.Colour, Discount = tiro.Discount, Price = tiro.Price, Size = tiro.Size, Image = tiro.Image, Page = tiro.Page, Type = tiro.Type, Property = model.Property };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }

        public PerchaseModel vivo_nuovo  = new PerchaseModel();
        public IActionResult VivoNuovo()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 17).ToList();
            vivo_nuovo = i[0];
            return View(vivo_nuovo);
        }
        public IActionResult BuyVivoNuovo(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 17).ToList();
            vivo_nuovo = i[0];
            switch (model.Property)
            {
                case "GNOVS-742L":
                    vivo_nuovo.Price = 650;
                    vivo_nuovo.Size = "74";
                    break;
                case "GNOVS-762ML":
                    vivo_nuovo.Price = 660;
                    vivo_nuovo.Size = "76";
                    break;
                case "GNOVS-762M":
                    vivo_nuovo.Price = 680;
                    vivo_nuovo.Size = "76";
                    break;
                case "GNOVS-762MH":
                    vivo_nuovo.Price = 700;
                    vivo_nuovo.Size = "76";
                    break;
                case "GNOVS-842M":
                    vivo_nuovo.Price = 720;
                    vivo_nuovo.Size = "84";
                    break;
                case "GNOVS-842H":
                    vivo_nuovo.Price = 740;
                    vivo_nuovo.Size = "84";
                    break;

            }
            var basketItem = new BasketModel { Brend = vivo_nuovo.Brend, Colour = vivo_nuovo.Colour, Discount = vivo_nuovo.Discount, Id = vivo_nuovo.Id, Property = model.Property, Image = vivo_nuovo.Image, MinMaxPrice = vivo_nuovo.MinMaxPrice, Name = vivo_nuovo.Name, Page = vivo_nuovo.Page, Price = vivo_nuovo.Price, Size = vivo_nuovo.Size, Type = vivo_nuovo.Type };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        
        public PerchaseModel icecube = new PerchaseModel();
        public IActionResult IceCube()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 18).ToList();
            icecube = i[0];
            return View(icecube);
        }
        public IActionResult BuyIceCube(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 18).ToList();
            icecube = i[0];
            switch (model.Property)
            {
                case "IC-69F-Sis":
                    icecube.Price = 300;
                    icecube.Size = "69";
                    break;
                case "IC-69P-Sis":
                    icecube.Price = 320;
                    icecube.Size = "69";
                    break;
                case "IC-90TG-Sis":
                    icecube.Price = 380;
                    icecube.Size = "9";
                    break;
            }

            var basketItem = new BasketModel { Name = icecube.Name, Brend = icecube.Brend, Id = icecube.Id, Colour = icecube.Colour, Discount = icecube.Discount, Price = icecube.Price, Size = icecube.Size, Image = icecube.Image, Page = icecube.Page, Type = icecube.Type, Property = model.Property, MinMaxPrice = icecube.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        
        public PerchaseModel sram = new PerchaseModel();
        public IActionResult Sram()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 19).ToList();
            sram = i[0];
            return View(sram);
        }
        public IActionResult BuySram(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 19).ToList();
            sram = i[0];
            switch (model.Property)
            {
                case "UTR-68-TOR":
                    sram.Price = 280;
                    sram.Size = "68";
                    break;
                case "EXR-73S-Sis":
                    sram.Price = 300;
                    sram.Size = "73";
                    break;
                case "TCR-84S":
                    sram.Price = 350;
                    sram.Size = "84";
                    break;
            }

            var basketItem = new BasketModel { Name = sram.Name, Brend = sram.Brend, Id = sram.Id, Colour = sram.Colour, Discount = sram.Discount, Price = sram.Price, Size = sram.Size, Image = sram.Image, Page = sram.Page, Type = sram.Type, Property = model.Property, MinMaxPrice = sram.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        
        public PerchaseModel inbite = new PerchaseModel();
        public IActionResult Inbite()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 20).ToList();
            inbite = i[0];
            return View(inbite);
        }
        public IActionResult BuyInbite(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 20).ToList();
            inbite = i[0];
            switch (model.Property)
            {
                case "IB710-TB":
                    inbite.Price = 380;
                    inbite.Size = "71";
                    break;
                case "IB73-CS":
                    inbite.Price = 350;
                    inbite.Size = "73";
                    break;
                ;
            }

            var basketItem = new BasketModel { Name = inbite.Name, Brend = inbite.Brend, Id = inbite.Id, Colour = inbite.Colour, Discount = inbite.Discount, Price = inbite.Price, Size = inbite.Size, Image = inbite.Image, Page = inbite.Page, Type = inbite.Type, Property = model.Property, MinMaxPrice = inbite.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        //Finezza

        public PerchaseModel finezza = new PerchaseModel();
        public IActionResult Finezza()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 21).ToList();
            finezza = i[0];
            return View(finezza);
        }
        public IActionResult BuyFinezza(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 21).ToList();
            finezza = i[0];
            switch (model.Property)
            {
                case "GLFS-752L-T":
                    finezza.Price = 390;
                    finezza.Size = "75";
                    break;
                case "GLFS-7112ML-T":
                    finezza.Price = 450;
                    finezza.Size = "81";
                    break;
                case "742ML-T":
                    finezza.Price = 485;
                    finezza.Size = "74";
                    break;
            }

            var basketItem = new BasketModel { Name = finezza.Name, Brend = finezza.Brend, Id = finezza.Id, Colour = finezza.Colour, Discount = finezza.Discount, Price = finezza.Price, Size = finezza.Size, Image = finezza.Image, Page = finezza.Page, Type = finezza.Type, Property = model.Property, MinMaxPrice = finezza.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        //Calamaretti 
        public PerchaseModel calamaretti = new PerchaseModel();
        public IActionResult Calamaretti()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 22).ToList();
            calamaretti = i[0];
            return View(calamaretti);
        }
        public IActionResult BuyCalamaretti(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 22).ToList();
            calamaretti = i[0];
            switch (model.Property)
            {
                case "20GCALS-7102M":
                    calamaretti.Price = 480;
                    calamaretti.Size = "8";
                    break;
                case "GNCPRS-802ML-S":
                    calamaretti.Price = 650;
                    calamaretti.Size = "8";
                    break;
                case "GNCPRS-8102MH":
                    calamaretti.Price = 680;
                    calamaretti.Size = "9";
                    break;
            }

            var basketItem = new BasketModel { Name = calamaretti.Name, Brend = calamaretti.Brend, Id = calamaretti.Id, Colour = calamaretti.Colour, Discount = calamaretti.Discount, Price = calamaretti.Price, Size = calamaretti.Size, Image = calamaretti.Image, Page = calamaretti.Page, Type = calamaretti.Type, Property = model.Property, MinMaxPrice = calamaretti.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        //Silverado
        public PerchaseModel silverado = new PerchaseModel();
        public IActionResult Silverado()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 23).ToList();
            silverado = i[0];
            return View(silverado);
        }
        public IActionResult BuySilverado(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 23).ToList();
            silverado = i[0];
            switch (model.Property)
            {
                case "GSIS-742LML-HS":
                    silverado.Price = 500;
                    silverado.Size = "74";
                    break;
                case "GSIS-742ML-LE":
                    silverado.Price = 550;
                    silverado.Size = "76";
                    break;
                case "20GSILPS-762ML":
                    silverado.Price = 560;
                    silverado.Size = "76";
                    break;
            }

            var basketItem = new BasketModel { Name = silverado.Name, Brend = silverado.Brend, Id = silverado.Id, Colour = silverado.Colour, Discount = silverado.Discount, Price = silverado.Price, Size = silverado.Size, Image = silverado.Image, Page = silverado.Page, Type = silverado.Type, Property = model.Property, MinMaxPrice = silverado.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        //Poseidon 
        public PerchaseModel poseidon = new PerchaseModel();
        public IActionResult Poseidon()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 24).ToList();
            poseidon = i[0];
            return View(poseidon);
        }
        public IActionResult BuyPoseidon(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 24).ToList();
            poseidon = i[0];
            switch (model.Property)
            {
                case "PSSS-67S":
                    poseidon.Price = 800;
                    poseidon.Size = "67";
                    break;
                case "PSSS-73T":
                    poseidon.Price = 900;
                    poseidon.Size = "73";
                    break;
                case "PSSS-78T":
                    poseidon.Price = 960;
                    poseidon.Size = "78";
                    break;
            }

            var basketItem = new BasketModel { Name = poseidon.Name, Brend = poseidon.Brend, Id = poseidon.Id, Colour = poseidon.Colour, Discount = poseidon.Discount, Price = poseidon.Price, Size = poseidon.Size, Image = poseidon.Image, Page = poseidon.Page, Type = poseidon.Type, Property = model.Property, MinMaxPrice = poseidon.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        // Squidlaw 
        public PerchaseModel squidlaw = new PerchaseModel();
        public IActionResult Squidlaw()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 25).ToList();
            squidlaw = i[0];
            return View(squidlaw);
        }
        public IActionResult BuySquidlaw(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 25).ToList();
            squidlaw = i[0];
            switch (model.Property)
            {
                case "NIMS-73M":
                    squidlaw.Price = 900;
                    squidlaw.Size = "73";
                    break;
                case "NIMS-86M":
                    squidlaw.Price = 950;
                    squidlaw.Size = "86";
                    break;
                case "NIMS-90L":
                    squidlaw.Price = 980;
                    squidlaw.Size = "9";
                    break;
            }

            var basketItem = new BasketModel { Name = squidlaw.Name, Brend = squidlaw.Brend, Id = squidlaw.Id, Colour = squidlaw.Colour, Discount = squidlaw.Discount, Price = squidlaw.Price, Size = squidlaw.Size, Image = squidlaw.Image, Page = squidlaw.Page, Type = squidlaw.Type, Property = model.Property, MinMaxPrice = squidlaw.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        // Jackall Rerange
        public PerchaseModel rerange = new PerchaseModel();
        public IActionResult Rerange()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 26).ToList();
            rerange = i[0];
            return View(rerange);
        }
        public IActionResult BuyRerange(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 26).ToList();
            rerange = i[0];
            switch (model.Size)
            {
                case "110":
                    rerange.Price = 30;

                    break;
                case "130":
                    rerange.Price = 35;
                    break;
                
            }

            var basketItem = new BasketModel { Name = rerange.Name, Brend = rerange.Brend, Id = rerange.Id, Colour = model.Colour, Discount = rerange.Discount, Price = rerange.Price, Size = model.Size, Image = rerange.Image, Page = rerange.Page, Type = rerange.Type, Property = rerange.Property, MinMaxPrice = rerange.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        // Shimano Stradic 
        public PerchaseModel stradic = new PerchaseModel();
        public IActionResult Stradic()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 27).ToList();
            stradic = i[0];
            return View(stradic);
        }
        public IActionResult BuyStradic(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 27).ToList();
            stradic = i[0];
            switch (model.Size)
            {
                case "1000":
                    stradic.Price = 200;

                    break;
                case "2500":
                    stradic.Price = 250;
                    break;
                case "c3000":
                    stradic.Price = 250;
                    break;
                case "4000":
                    stradic.Price = 280;
                    break;
            }

            var basketItem = new BasketModel { Name = stradic.Name, Brend = stradic.Brend, Id = stradic.Id, Colour = stradic.Colour, Discount = stradic.Discount, Price = stradic.Price, Size = model.Size, Image = stradic.Image, Page = stradic.Page, Type = stradic.Type, Property = stradic.Property, MinMaxPrice = stradic.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        //Shimano Ultegra
        public PerchaseModel ultegra = new PerchaseModel();
        public IActionResult Ultegra()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 28).ToList();
            ultegra = i[0];
            return View(ultegra);
        }
        public IActionResult BuyUltegra(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 28).ToList();
            ultegra = i[0];
            switch (model.Size)
            {
                case "1000":
                    ultegra.Price = 140;

                    break;
                case "2500":
                    ultegra.Price = 200;
                    break;
                case "c3000":
                    ultegra.Price = 200;
                    break;
                case "4000":
                    ultegra.Price = 220;
                    break;
            }

            var basketItem = new BasketModel { Name = ultegra.Name, Brend = ultegra.Brend, Id = ultegra.Id, Colour = ultegra.Colour, Discount = ultegra.Discount, Price = ultegra.Price, Size = model.Size, Image = ultegra.Image, Page = ultegra.Page, Type = ultegra.Type, Property = ultegra.Property, MinMaxPrice = ultegra.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        //Shimano Twin Power 
        public PerchaseModel twinpower = new PerchaseModel();
        public IActionResult TwinPower()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 29).ToList();
            twinpower = i[0];
            return View(twinpower);
        }
        public IActionResult BuyTwinPower(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 29).ToList();
            twinpower = i[0];
            switch (model.Size)
            {
                case "1000":
                    twinpower.Price = 620;

                    break;
                case "2500":
                    twinpower.Price = 700;
                    break;
                case "c3000":
                    twinpower.Price = 700;
                    break;
                case "4000":
                    twinpower.Price = 750;
                    break;
            }

            var basketItem = new BasketModel { Name = twinpower.Name, Brend = twinpower.Brend, Id = twinpower.Id, Colour = twinpower.Colour, Discount = twinpower.Discount, Price = twinpower.Price, Size = model.Size, Image = twinpower.Image, Page = twinpower.Page, Type = twinpower.Type, Property = twinpower.Property, MinMaxPrice = twinpower.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        //Shimano Stella
        public PerchaseModel stella = new PerchaseModel();
        public IActionResult Stella()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 30).ToList();
            stella = i[0];
            return View(stella);
        }
        public IActionResult BuyStella(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 30).ToList();
            stella = i[0];
            switch (model.Size)
            {
                case "1000":
                    stella.Price = 980;

                    break;
                case "2500":
                    stella.Price = 1000;
                    break;
                case "c3000":
                    stella.Price = 1000;
                    break;
                case "4000":
                    stella.Price = 1050;
                    break;
            }

            var basketItem = new BasketModel { Name = stella.Name, Brend = stella.Brend, Id = stella.Id, Colour = stella.Colour, Discount = stella.Discount, Price = stella.Price, Size = model.Size, Image = stella.Image, Page = stella.Page, Type = stella.Type, Property = stella.Property, MinMaxPrice = stella.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }
        //Daiwa Caldia LT
        public PerchaseModel caldia = new PerchaseModel();
        public IActionResult Caldia()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 31).ToList();
            caldia = i[0];
            return View(caldia);
        }
        public IActionResult BuyCaldia(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 31).ToList();
            caldia = i[0];
            switch (model.Size)
            {
                case "1000":
                    caldia.Price = 190;

                    break;
                case "2500":
                    caldia.Price = 200;
                    break;
                case "c3000":
                    caldia.Price = 200;
                    break;
                case "4000":
                    caldia.Price = 220;
                    break;
            }

            var basketItem = new BasketModel { Name = caldia.Name, Brend = caldia.Brend, Id = caldia.Id, Colour = caldia.Colour, Discount = caldia.Discount, Price = caldia.Price, Size = model.Size, Image = caldia.Image, Page = caldia.Page, Type = caldia.Type, Property = caldia.Property, MinMaxPrice = caldia.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }

        // Daiwa Exist LT
        public PerchaseModel exist = new PerchaseModel();
        public IActionResult Exist()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 32).ToList();
            exist = i[0];
            return View(exist);
        }
        public IActionResult BuyExist(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 32).ToList();
            exist = i[0];
            switch (model.Size)
            {
                case "1000":
                    exist.Price = 850;

                    break;
                case "2500":
                    exist.Price = 900;
                    break;
                case "c3000":
                    exist.Price = 900;
                    break;
                case "4000":
                    exist.Price = 920;
                    break;
            }

            var basketItem = new BasketModel { Name = exist.Name, Brend = exist.Brend, Id = exist.Id, Colour = exist.Colour, Discount = exist.Discount, Price = exist.Price, Size = model.Size, Image = exist.Image, Page = exist.Page, Type = exist.Type, Property = exist.Property, MinMaxPrice = exist.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }

        // DAIWA Luvias LT 
        public PerchaseModel luvias = new PerchaseModel();
        public IActionResult Luvias()
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 33).ToList();
            luvias = i[0];
            return View(luvias);
        }
        public IActionResult BuyLuvias(PerchaseModel model)
        {
            var i = _content.AllPerchaseItems.Where(x => x.Id == 33).ToList();
            luvias = i[0];
            switch (model.Size)
            {
                case "1000":
                    luvias.Price = 650;

                    break;
                case "2500":
                    luvias.Price = 700;
                    break;
                case "c3000":
                    luvias.Price = 700;
                    break;
                case "4000":
                    luvias.Price = 720;
                    break;
            }

            var basketItem = new BasketModel { Name = luvias.Name, Brend = luvias.Brend, Id = luvias.Id, Colour = luvias.Colour, Discount = luvias.Discount, Price = luvias.Price, Size = model.Size, Image = luvias.Image, Page = luvias.Page, Type = luvias.Type, Property = luvias.Property, MinMaxPrice = luvias.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }

    }
}
