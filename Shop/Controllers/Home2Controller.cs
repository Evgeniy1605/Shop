using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

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
        
        
        public async Task<IActionResult> Index()
        {
            items = await _content.AllPerchaseItems.ToListAsync();
            return View();
        }


        public IActionResult basket()
        {
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View(BasketItem);
        }
        public async Task <IActionResult> AllItems()
        {
            items = await _content.AllPerchaseItems.ToListAsync();
            return View(items);
        }

        // Montero
        public IActionResult montero()
        {
            Montero = _content.AllPerchaseItems.Single(x => x.Name == "Montero" && x.Brend == "Strike pro");
            return View(Montero);
        }
        public IActionResult BuyMontero(PerchaseModel model)
        {

            Montero = _content.AllPerchaseItems.Single(x => x.Name == "Montero" && x.Brend == "Strike pro");
            
            switch (model.Size)
            {
                case "90":
                    rerange.Price = 5;

                    break;
                case "110":
                    rerange.Price = 7;
                    break;
                case "130":
                    rerange.Price = 10;
                    break;

            }

            var basketItem = new BasketModel { Name = Montero.Name, Brend = Montero.Brend, Id = Montero.Id, Colour = model.Colour, Discount = Montero.Discount, Price = Montero.Price, Size = model.Size, Image = Montero.Image, Page = Montero.Page, Type = Montero.Type, Property = Montero.Property, MinMaxPrice = Montero.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }


        // Orbit
        public PerchaseModel orbit = new PerchaseModel();
        public IActionResult OrbitPage()
        {
            orbit = _content.AllPerchaseItems.Single(x => x.Name == "Orbit");
            return View(orbit);
        }

        public IActionResult BuyOrbit(PerchaseModel model)
        {
            orbit = _content.AllPerchaseItems.Single(x => x.Name == "Orbit");
            switch (model.Size)
            {
                case "80":
                    orbit.Price = 50;
                    break;
                case "110":
                    orbit.Price = 55;
                    break;
                case "130":
                    orbit.Price = 60;
                    break;
            }
            var basketItem = new BasketModel { Name = orbit.Name, Brend = orbit.Brend, Id = orbit.Id, Colour = model.Colour, Discount = orbit.Discount, Price = orbit.Price, Size = model.Size, Image = orbit.Image, Page = orbit.Page, Type = orbit.Type, Property = orbit.Property, MinMaxPrice = orbit.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
            
        }

       

        // Malas

        PerchaseModel malas = new PerchaseModel();
        public IActionResult Malas()
        {
            malas = _content.AllPerchaseItems.Single(x => x.Name == "Malas" && x.Brend == "Lucky Craft");
            return View(malas);
        }
        public IActionResult BuyMalas(PerchaseModel model)
        {
            malas = _content.AllPerchaseItems.Single(x => x.Name == "Malas" && x.Brend == "Lucky Craft");
            if (model.Colour == "col-1")
                malas.Price = 20;
            else
                malas.Price = 10;
            BasketModel malas1 = new BasketModel() { Brend = malas.Brend, Name = malas.Name, Colour = model.Colour, Discount = malas.Discount, Id = malas.Discount, Image = malas.Image, MinMaxPrice = malas.MinMaxPrice, Page = malas.Page, Price = malas.Price, Property = malas.Property, Size = malas.Size, Type = malas.Type};
            BasketItem.Add(malas1);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);
        }

        // Rigge
        PerchaseModel rigge = new PerchaseModel();
        //{ Brend = "Zipbaits", Name = "Rigge", Page = "Rigge" };

        public IActionResult Rigge()
        {
            rigge = _content.AllPerchaseItems.Single(x => x.Name == "Rigge" && x.Brend == "Zipbaits");
            return View(rigge);
        }
        public IActionResult BuyRigge(ZipbaitsOrbitModel model )
        {

            rigge = _content.AllPerchaseItems.Single(x => x.Name == "Rigge" && x.Brend == "Zipbaits");
            if (model.Size == "30")
                rigge.Price = 10;
            else if (model.Size == "90")
                rigge.Price = 15;
            else if (model.Size == "110")
                rigge.Price = 17;

            BasketModel rigge1 = new BasketModel() { Brend = rigge.Brend, Name = rigge.Name, Type = rigge.Type, Colour = model.Colour, Discount = rigge.Discount, Id = rigge.Id, Image = rigge.Image, Price = rigge.Price, MinMaxPrice = rigge.MinMaxPrice, Page = rigge.Page, Size = model.Size, Property = rigge.Property };
            
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

        // save data
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

        // Inquisitor
        private PerchaseModel inquisitor = new PerchaseModel();
        public IActionResult Inquisitor()
        {
            inquisitor = _content.AllPerchaseItems.Single(x => x.Name == "Inquisitor" && x.Brend == "Strike pro");
            return View(inquisitor);
        }
        public IActionResult BuyInquisitor(PerchaseModel perchase)
        {
            inquisitor = _content.AllPerchaseItems.Single(x => x.Name == "Inquisitor" && x.Brend == "Strike pro");
            if (perchase.Size == "80")
                perchase.Price = 5;
            if (perchase.Size == "110")
                perchase.Price = 7;
            else if (perchase.Size == "130")
                perchase.Price = 10;
            BasketModel basketModel = new BasketModel() { Name = inquisitor.Name, Brend = inquisitor.Brend, Colour = perchase.Colour, Discount = inquisitor.Discount, Image = inquisitor.Image, Page = "Inquisitor", Price = perchase.Price, Size = perchase.Size, Type = inquisitor.Type, Id = inquisitor.Id, Property = inquisitor.Property, MinMaxPrice = inquisitor.MinMaxPrice };
            BasketItem.Add(basketModel);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);


        }
        // Magallon
        public PerchaseModel magallon = new PerchaseModel();
        
        public IActionResult Magallon()
        {
            magallon = _content.AllPerchaseItems.Single(x => x.Name == "Tiny Magallon");
            return View(magallon);
        }
        public IActionResult BuyMagallon(PerchaseModel model)
        {
            magallon = _content.AllPerchaseItems.Single(x => x.Name == "Tiny Magallon");
            var basketModel = new BasketModel() { Name = magallon.Name, Colour = model.Colour, Brend = magallon.Brend, Discount = magallon.Discount, Image = magallon.Image, Page = magallon.Page, Price = magallon.Price, Size = magallon.Size, Type = magallon.Type, MinMaxPrice = magallon.MinMaxPrice, Id = magallon.Id };
            BasketItem.Add(basketModel);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();

            return View("basket", BasketItem);
        }


        // Archback
        public new PerchaseModel archback = new PerchaseModel();
        public IActionResult Archback()
        {
            archback = _content.AllPerchaseItems.Single(x => x.Name == "Archback" && x.Brend == "Strike pro");
            return View(archback);
        }

        public IActionResult BuyArchback(PerchaseModel model)
        {
            archback = archback = _content.AllPerchaseItems.Single(x => x.Name == "Archback" && x.Brend == "Strike pro");
            archback.Colour = model.Colour;

            var basketItem = new BasketModel { Name = archback.Name, Brend = archback.Brend, Id = archback.Id, Colour = archback.Colour, Discount = archback.Discount, Price = archback.Price, Size = archback.Size, Image = archback.Image, Page = archback.Page, Type = archback.Type };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }

        // Submit
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


        // Voblers
        public IActionResult Voblers()
        {
            items = _content.AllPerchaseItems.ToList();
            var result = new List<PerchaseModel>();
           
            result = items.Where(x => x.Type == "Vobler").ToList();
            return View(result);
            
        }


        // Tiro
        public PerchaseModel tiro = new PerchaseModel();
        public IActionResult Tiro()
        {
            tiro = _content.AllPerchaseItems.Single(x => x.Name == "Tiro" && x.Brend == "Graphiteleader");
            return View(tiro);
        }
        public IActionResult BuyTiro(PerchaseModel model)
        {
            tiro = _content.AllPerchaseItems.Single(x => x.Name == "Tiro" && x.Brend == "Graphiteleader");
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

        // Vivo
        public PerchaseModel vivo_nuovo  = new PerchaseModel();
        public IActionResult VivoNuovo()
        {
            vivo_nuovo = _content.AllPerchaseItems.Single(x => x.Name == "Vivo Nuovo" && x.Brend == "Graphiteleader");
            return View(vivo_nuovo);
        }
        public IActionResult BuyVivoNuovo(PerchaseModel model)
        {
            vivo_nuovo = _content.AllPerchaseItems.Single(x => x.Name == "Vivo Nuovo" && x.Brend == "Graphiteleader");
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

        /// <summary>
        /// !!!!!!!!!!!!!
        /// </summary>

        //IceCube
        public PerchaseModel icecube = new PerchaseModel();
        public IActionResult IceCube()
        {
            icecube = _content.AllPerchaseItems.Single(x => x.Name == "Ice Cube" && x.Brend == "Tict");
            return View(icecube);
        }
        public IActionResult BuyIceCube(PerchaseModel model)
        {
            icecube = _content.AllPerchaseItems.Single(x => x.Name == "Ice Cube" && x.Brend == "Tict");
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


        // Sram 
        public PerchaseModel sram = new PerchaseModel();
        public IActionResult Sram()
        {
            sram = _content.AllPerchaseItems.Single(x => x.Name == "Sram" && x.Brend == "Tict");
            return View(sram);
        }
        public IActionResult BuySram(PerchaseModel model)
        {
            sram = _content.AllPerchaseItems.Single(x => x.Name == "Sram" && x.Brend == "Tict");
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



        // Inbite
        public PerchaseModel inbite = new PerchaseModel();
        public IActionResult Inbite()
        {
            inbite = _content.AllPerchaseItems.Single(x => x.Name == "Inbite" && x.Brend == "Tict");
            return View(inbite);
        }
        public IActionResult BuyInbite(PerchaseModel model)
        {
            inbite = _content.AllPerchaseItems.Single(x => x.Name == "Inbite" && x.Brend == "Tict");
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
            finezza = _content.AllPerchaseItems.Single(x => x.Name == "Finezza" && x.Brend == "Graphiteleader");
            return View(finezza);
        }
        public IActionResult BuyFinezza(PerchaseModel model)
        {
            finezza = _content.AllPerchaseItems.Single(x => x.Name == "Finezza" && x.Brend == "Graphiteleader");
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
            calamaretti = _content.AllPerchaseItems.Single(x => x.Name == "Calamaretti" && x.Brend == "Graphiteleader");
            return View(calamaretti);
        }
        public IActionResult BuyCalamaretti(PerchaseModel model)
        {
            calamaretti = _content.AllPerchaseItems.Single(x => x.Name == "Calamaretti" && x.Brend == "Graphiteleader");
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
            silverado = _content.AllPerchaseItems.Single(x => x.Name == "Silverado" && x.Brend == "Graphiteleader");
            return View(silverado);
        }
        public IActionResult BuySilverado(PerchaseModel model)
        {
            silverado = _content.AllPerchaseItems.Single(x => x.Name == "Silverado" && x.Brend == "Graphiteleader");
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
            poseidon = _content.AllPerchaseItems.Single(x => x.Name == "Poseidon Salty Sensation" && x.Brend == "Ever Green");
            return View(poseidon);
        }
        public IActionResult BuyPoseidon(PerchaseModel model)
        {
            poseidon = _content.AllPerchaseItems.Single(x => x.Name == "Poseidon Salty Sensation" && x.Brend == "Ever Green");
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
            squidlaw = _content.AllPerchaseItems.Single(x => x.Name == "Squidlaw Imperial" && x.Brend == "Ever Green");
            return View(squidlaw);
        }
        public IActionResult BuySquidlaw(PerchaseModel model)
        {
            squidlaw = _content.AllPerchaseItems.Single(x => x.Name == "Squidlaw Imperial" && x.Brend == "Ever Green");
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
            rerange = _content.AllPerchaseItems.Single(x => x.Name == "Rerange" && x.Brend == "Jackall");
            return View(rerange);
        }
        public IActionResult BuyRerange(PerchaseModel model)
        {
            rerange = _content.AllPerchaseItems.Single(x => x.Name == "Rerange" && x.Brend == "Jackall");
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
            stradic = _content.AllPerchaseItems.Single(x => x.Name == "Stradic" && x.Brend == "Shimano");
            return View(stradic);
        }
        public IActionResult BuyStradic(PerchaseModel model)
        {
            stradic = _content.AllPerchaseItems.Single(x => x.Name == "Stradic" && x.Brend == "Shimano");
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
            ultegra = _content.AllPerchaseItems.Single(x => x.Name == "Ultegra" && x.Brend == "Shimano");
            return View(ultegra);
        }
        public IActionResult BuyUltegra(PerchaseModel model)
        {
            ultegra = _content.AllPerchaseItems.Single(x => x.Name == "Ultegra" && x.Brend == "Shimano");
            switch (model.Size)
            {
                case "1000":
                    ultegra.Price = 150;

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
            twinpower = _content.AllPerchaseItems.Single(x => x.Name == "Twin Power" && x.Brend == "Shimano");
            return View(twinpower);
        }
        public IActionResult BuyTwinPower(PerchaseModel model)
        {
            twinpower = _content.AllPerchaseItems.Single(x => x.Name == "Twin Power" && x.Brend == "Shimano");
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
            stella = _content.AllPerchaseItems.Single(x => x.Name == "Stella" && x.Brend == "Shimano");
            return View(stella);
        }
        public IActionResult BuyStella(PerchaseModel model)
        {
            stella = _content.AllPerchaseItems.Single(x => x.Name == "Stella" && x.Brend == "Shimano");
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
            caldia = _content.AllPerchaseItems.Single(x => x.Name == "Caldia LT" && x.Brend == "Daiwa");
            return View(caldia);
        }
        public IActionResult BuyCaldia(PerchaseModel model)
        {
            caldia = _content.AllPerchaseItems.Single(x => x.Name == "Caldia LT" && x.Brend == "Daiwa");
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
            exist = _content.AllPerchaseItems.Single(x => x.Name == "Exist LT");
            return View(exist);
        }
        public IActionResult BuyExist(PerchaseModel model)
        {
            exist = _content.AllPerchaseItems.Single(x => x.Name == "Exist LT");
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
            
            luvias = _content.AllPerchaseItems.Single(x => x.Name == "Luvias LT");
            return View(luvias);
        }
        public IActionResult BuyLuvias(PerchaseModel model)
        {
            luvias = _content.AllPerchaseItems.Single(x => x.Name == "Luvias LT");
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
        // Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            BasketItem.RemoveRange(0, BasketItem.Count);
            return Redirect("/");
        }

        // Lucky Craft Pointer

        public PerchaseModel pointer = new PerchaseModel();
        public IActionResult Pointer()
        {
            pointer = _content.AllPerchaseItems.Single(x => x.Name == "Pointer" && x.Brend == "Lucky Craft");
            return View(pointer);
        }
        public IActionResult BuyPointer(PerchaseModel model)
        {
            pointer = _content.AllPerchaseItems.Single(x => x.Name == "Pointer" && x.Brend == "Lucky Craft");
            switch (model.Size)
            {
                case "43":
                    pointer.Price = 20;
                    break;
                case "110":
                    pointer.Price = 35;
                    break;
                case "128":
                    pointer.Price = 38;
                    break;
            }

            var basketItem = new BasketModel { Name = pointer.Name, Brend = pointer.Brend, Id = pointer.Id, Colour = model.Colour, Discount = pointer.Discount, Price = pointer.Price, Size = model.Size, Image = pointer.Image, Page = pointer.Page, Type = pointer.Type, Property = pointer.Property, MinMaxPrice = pointer.MinMaxPrice };
            BasketItem.Add(basketItem);
            FN = BasketItem.Sum(x => x.Price);
            int x = (int)FN;
            ViewData["Message"] = x.ToString();
            return View("basket", BasketItem);
        }


    }
}
