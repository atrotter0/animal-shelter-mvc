using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AnimalShelterProject;

namespace AnimalShelterProject.Models
{
    public class Animal
    {
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public DateTime AdmittanceDate { get; set; }
        public int Id { get; set; }

        public Animal(string testtype, string breed, string gender, string name, DateTime admittanceDate, int id = 0)
        {
            this.Type = testtype;
            this.Breed = breed;
            this.Gender = gender;
            this.Name = name;
            this.AdmittanceDate = admittanceDate;
            this.Id = id;
        }

        public override bool Equals(System.Object otherAnimal)
        {
            if(!(otherAnimal is Animal))
            {
                return false;
            }
            else
            {
                Animal newAnimal = (Animal) otherAnimal;
                bool descriptionEquality = (this.Type == newAnimal.Type && this.Breed == newAnimal.Breed && this.Gender == newAnimal.Gender && this.Name == newAnimal.Name && this.AdmittanceDate == newAnimal.AdmittanceDate);
                return (descriptionEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
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

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand as MySqlCommand;
            cmd.CommandText = @"INSERT INTO animals (type, breed, gender, name, admittanceDate) VALUES (@AnimalType, @AnimalBreed, @AnimalGender, @AnimalName, @AnimalAdmittanceDate);";

            MySqlParameter type = new MySqlParameter();
            type.ParameterName = "@AnimalType";
            type.Value = this.Type;
            MySqlParameter breed = new MySqlParameter();
            breed.ParameterName = "@AnimalBreed";
            breed.Value = this.Breed;
            MySqlParameter gender = new MySqlParameter();
            gender.ParameterName = "@AnimalGender";
            gender.Value = this.Gender;
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@AnimalName";
            name.Value = this.Name;
            MySqlParameter admittanceDate = new MySqlParameter();
            admittanceDate.ParameterName = "@AnimalAdmittanceDate";
            admittanceDate.Value = this.admittanceDate;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

    }
}
