using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AnimalShelterProject;

namespace AnimalShelterProject.Models
{
    public class Animal
    {
        private string _type;
        private string _breed;
        private string _gender;
        private string _name;
        private DateTime _admittanceDate;
        private int _id;

        public string Type { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public DateTime AdmittanceDate { get; set; }
        public int Id { get; set; }

        public Animal(string type, string breed, string gender, string name, DateTime admittanceDate, int id)
        {
            _type = type;
            _breed = breed;
            _gender = gender;
            _name = name;
            _admittanceDate = admittanceDate;
            _id = id;
        }

        public static void DeleteAll()
        {
           MySqlConnection conn = DB.Connection();
           conn.Open();
           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"DELETE FROM animals;";
           cmd.ExecuteNonQuery();
           conn.Close();
           if (conn != null)
           {
               conn.Dispose();
           }
        }

        public static List<Animal> GetAll()
        {
            List<Animal> allAnimals = new List<Animal>() {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM animals;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                string animalType = rdr.GetString(0);
                string animalBreed = rdr.GetString(1);
                string animalGender = rdr.GetString(2);
                string animalName = rdr.GetString(3);
                DateTime animalAdmittanceDate = rdr.GetDateTime(4);
                int animalId = rdr.GetInt32(5);
                Animal newAnimal = new Animal(animalType, animalBreed, animalGender, animalName, animalAdmittanceDate, animalId);
                allAnimals.Add(newAnimal);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAnimals;
        }
    }
}
