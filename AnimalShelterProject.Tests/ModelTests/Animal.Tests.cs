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
            newAnimal.Type = "dog";
            newAnimal.Breed = "boxer";
            newAnimal.Gender = "male";
            newAnimal.Name = "zeke";
            newAnimal.AdmittanceDate = new DateTime(2018, 07, 10);
            newAnimal.Id = 1;

            Assert.AreEqual("dog", newAnimal.Type);
            Assert.AreEqual("boxer", newAnimal.Breed);
            Assert.AreEqual("male", newAnimal.Gender);
            Assert.AreEqual("zeke", newAnimal.Name);
            Assert.AreEqual(dateResult, newAnimal.AdmittanceDate);
            Assert.AreEqual(1, newAnimal.Id);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Animal.GetAll().Count;
            Assert.AreEqual(0, result);
        }
    }
}
