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

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Animal()
        {
            DateTime dateResult = new DateTime(2018, 07, 10);
            DateTime secondDateResult = new DateTime(2018, 07, 09);
            Animal firstAnimal = new Animal("cat", "persian", "female", "tidus", dateResult);
            Animal secondAnimal = new Animal("cat", "persian", "female", "tidus", dateResult);
            Assert.AreEqual(firstAnimal, secondAnimal);
        }

        [TestMethod]
        public void Save_SavesToDatabase_AnimalList()
        {
            DateTime dateResult = new DateTime(2018, 07, 10);
            Animal testAnimal = new Animal("cat", "persian", "female", "tidus", dateResult);
            testAnimal.Save();
            List<Animal> result = Animal.GetAll();
            List<Animal> testList = new List<Animal>{testAnimal};
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Find_FindsAnimalInDatabaseById_Animal()
        {
            DateTime dateResult = new DateTime(2077, 07, 10);
            Animal newAnimal = new Animal("cat", "persian", "female", "petunia", dateResult);
            newAnimal.Save();
            Animal foundAnimal = Animal.Find(newAnimal.Id);
            Assert.AreEqual(newAnimal, foundAnimal);
        }

        [TestMethod]
        public void Find_FindsAnimalInDatabaseByPropertyName_Animal()
        {
            DateTime dateResult = new DateTime(2077, 07, 10);
            Animal newAnimal = new Animal("cat", "persian", "female", "petunia", dateResult);
            newAnimal.Save();
            Animal foundAnimal = Animal.Find(newAnimal.Name, "name");
            Assert.AreEqual(newAnimal, foundAnimal);
        }
    }
}
