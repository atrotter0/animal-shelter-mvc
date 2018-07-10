using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimalShelterProject.Models;

namespace AnimalShelterProject.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            List<Animal> allAnimals = Animal.GetAll();
            return View(allAnimals);
        }
    }
}
