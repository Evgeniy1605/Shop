﻿using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    // passcod is 666
    public class ExtraController : Controller
    {
        /*private readonly OrderDbConenction _content2;
        public ExtraController(OrderDbConenction content)
        {
            _content2 = content;
        }*/

        public IActionResult ChoooseTypeOfAccount()
        {
            return View();
        }
        public IActionResult LogAsAdministador()
        {
            InputModel input = new InputModel();

            return View();
        }
        public IActionResult TryLogAsAdmin(InputModel model)
        {
            if (model.IntInput == 666)
                return View("AdminPage");
            else 
                return View("LogError");
        }
        public static List<Order> orders = new List<Order>();
        private readonly OrderDbConenction _content;
        public ExtraController(OrderDbConenction content)
        {
            _content = content;
        }
        public IActionResult LookOrders()
        {
            orders = _content.Orders.ToList();
            return View(orders);
        }
        public IActionResult DeleteOrder()
        {
            InputModel input = new InputModel();
            return View(input);
        }
        public IActionResult Dlete(InputModel input)
        {
            if (input.IntInput == 0)
                return View("NoId");
            else
            {
                Order order = new Order() { Id = input.IntInput};
                _content.Remove(order);
                _content.SaveChanges();
                return View("AdminPage");

            }

        }
        public IActionResult AddNewPerchse()
        {
            InputModel input = new InputModel();
            return View(input);
        }
        public IActionResult SubminNewPerchas(InputModel input)
        {
            if (input.Brend == null || input.Name == null || input.NameOfPage == null || input.InformatinOfThePage == null || input.Image == null)
            {
                return View("EmptyGAp");
            }
            
           
            else
            {
                PerchaseModel NewPerchase = new PerchaseModel() { Name = input.Name, Brend = input.Brend, Image = input.Image, Page = input.NameOfPage, Price = 0, Colour = "_", Discount = 0, Size = "_"};
                InformationAbautNewProdact newProdact = new InformationAbautNewProdact() { Name = input.Name, Brend = input.Brend, Image = input.Image, AditionalInformation = input.InformatinOfThePage, Page = input.NameOfPage };
                _content.Add(NewPerchase);
                _content.SaveChanges();
                _content.Add(newProdact);
                _content.SaveChanges();
                return View("AdminPage");

            }
        }
    }
}
