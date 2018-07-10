using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimalShelterProject.Models;

namespace AnimalShelterProject.Controllers
{
    public class AnimalsController : Controller
    {
        [HttpGet("/animals")]
        public ActionResult Index()
        {
            List<Animal> allAnimals = Animal.GetAll();
            return View(allAnimals);
        }

        [HttpPost("/animals")]
        public ActionResult CreateAnimal(string type, string breed, string gender, string name)
        {
            DateTime dateToday = DateTime.Now;
            Animal newAnimal = new Animal(type, breed, gender, name, dateToday);
            newAnimal.Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/animals/{id}")]
        public ActionResult UpdateAnimalForm(int id)
        {
            Animal newAnimal = new Animal();
            newAnimal = Animal.Find(id);
            return View(newAnimal);
        }

        [HttpPost("/animals/{id}/update")]
        public ActionResult UpdateAnimal(string type, string breed, string gender, string name, int id)
        {
            Animal newAnimal = Animal.Find(id);
            newAnimal.Type = type;
            newAnimal.Breed = breed;
            newAnimal.Gender = gender;
            newAnimal.Name = name;
            newAnimal.Update();
            return RedirectToAction("Index");
        }

        [HttpPost("/animals/{id}/delete")]
        public ActionResult DeleteAnimal(int id)
        {
            Animal newAnimal = Animal.Find(id);
            newAnimal.Delete();
            return RedirectToAction("Index");
        }

        [HttpGet("/animals/new")]
        public ActionResult NewAnimal()
        {
            return View();
        }
    }
}
