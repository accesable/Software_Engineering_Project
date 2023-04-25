﻿using MobilePhoneDistributor_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobilePhoneDistributor_Web.Controllers
{
    public class OrderCartController : Controller
    {
        
        public ActionResult Index()
        {
            var cart = Session["cart"] as Cart;

            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return View(cart.GetItems());
        }
        [HttpGet]
        public ActionResult AddItem(string id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddItem(string id,OrderDetailCreateViewModel model)
        {
            var cart = Session["cart"] as Cart;

            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }

            cart.AddItem(new OrderDetail { PhoneModelId = id as string ,Quantity=model.Quantity});

            return RedirectToAction("Index");
        }

        public ActionResult RemoveItem(string id)
        {
            var cart = Session["cart"] as Cart;

            if (cart != null)
            {
                cart.RemoveItem(id);
            }

            return RedirectToAction("Index");
        }
    }
}