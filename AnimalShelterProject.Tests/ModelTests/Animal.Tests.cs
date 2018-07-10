using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using AnimalShelterProject.Models;

namespace AnimalShelterProject.Tests
{
    [TestClass]
    public class AnimalTest : IDisposable
    {
        public void Dispose()
        {
            Animal.DeleteAll();
        }

        public void AnimalTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=shelter_test;";
        }

        [TestMethod]
        public void GetSetProperties_GetsAndSetsProperties_EqualValue()
        {
            DateTime dateResult = new DateTime(2018, 07, 10);
            Animal newAnimal = new Animal("cat", "persian", "female", "tidus", dateResult, 2);
            // newAnimal.Type = "dog";
            // newAnimal.Breed = "boxer";
            // newAnimal.Gender = "male";
            // newAnimal.Name = "zeke";
            // newAnimal.AdmittanceDate = new DateTime(2018, 07, 10);
            // newAnimal.Id = 1;

            Assert.AreEqual("cat", newAnimal.Type);
            Assert.AreEqual("persian", newAnimal.Breed);
            Assert.AreEqual("female", newAnimal.Gender);
            Assert.AreEqual("tidus", newAnimal.Name);
            Assert.AreEqual(dateResult, newAnimal.AdmittanceDate);
            Assert.AreEqual(2, newAnimal.Id);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Animal.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Animal()
        {
            DateTime dateResult = new DateTime(2018, 07, 10);
            DateTime secondDateResult = new DateTime(2018, 07, 09);
            Animal newAnimal = new Animal();
            newAnimal.Type = "dog";
            newAnimal.Breed = "boxer";
            newAnimal.Gender = "male";
            newAnimal.Name = "zeke";
            Animal newAnimal2 = new Animal();
            newAnimal2.Type = "dog";
            newAnimal2.Breed = "boxer";
            newAnimal2.Gender = "male";
            newAnimal2.Name = "zeke";
            newAnimal2.AdmittanceDate = new DateTime(2018, 07, 10);
            // Animal firstAnimal = new Animal("cat", "p", "male", "jack", dateResult);
            // Animal secondAnimal = new Animal("dog", "persian", "female", "tidus", secondDateResult);
            // Console.WriteLine("Name:" + firstAnimal.Name);
            // Console.WriteLine("Equality check:" + firstAnimal.Equals(secondAnimal));
            Assert.AreEqual(newAnimal, newAnimal2);
        }

        // [TestMethod]
        // public void Save_SavesToDatabase_AnimalList("cat", "persian", "female", "tidus", dateResult)
        // {
        //     Animal testAnimal = new Animal()
        //     testAnimal.Save();
        //     List<Animal> result = Animal.GetAll();
        //     List<Animal> testList = new List<Animal>{testAnimal};
        // }
    }
}
