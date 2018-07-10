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
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO animals (type, breed, gender, name, admittance_date) VALUES (@AnimalType, @AnimalBreed, @AnimalGender, @AnimalName, @AnimalAdmittanceDate);";

            MySqlParameter type = new MySqlParameter();
            cmd.Parameters.AddWithValue("@AnimalType", this.Type);
            MySqlParameter breed = new MySqlParameter();
            cmd.Parameters.AddWithValue("@AnimalBreed", this.Breed);
            MySqlParameter gender = new MySqlParameter();
            cmd.Parameters.AddWithValue("@AnimalGender", this.Gender);
            MySqlParameter name = new MySqlParameter();
            cmd.Parameters.AddWithValue("@AnimalName", this.Name);
            MySqlParameter admittanceDate = new MySqlParameter();
            cmd.Parameters.AddWithValue("@AnimalAdmittanceDate", this.AdmittanceDate);

            cmd.ExecuteNonQuery();
            this.Id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Animal Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM animals WHERE id = @findId;";
            MySqlParameter findId = new MySqlParameter();
            cmd.Parameters.AddWithValue("@findId", id);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int animalId = 0;
            string animalType = "";
            string animalBreed = "";
            string animalGender = "";
            string animalName = "";
            DateTime animalDate = new DateTime();

            while (rdr.Read())
            {
                animalType = rdr.GetString(0);
                animalBreed = rdr.GetString(1);
                animalGender = rdr.GetString(2);
                animalName = rdr.GetString(3);
                animalDate = rdr.GetDateTime(4);
                animalId = rdr.GetInt32(5);
            }

            Animal foundAnimal = new Animal(animalType, animalBreed, animalGender, animalName, animalDate, animalId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundAnimal;
        }

        public static Animal Find(string record, string columnName)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM animals WHERE " + columnName + " = @findRecord;";
            MySqlParameter findRecord = new MySqlParameter();
            cmd.Parameters.AddWithValue("@findRecord", record);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int animalId = 0;
            string animalType = "";
            string animalBreed = "";
            string animalGender = "";
            string animalName = "";
            DateTime animalDate = new DateTime();

            while (rdr.Read())
            {
                animalType = rdr.GetString(0);
                animalBreed = rdr.GetString(1);
                animalGender = rdr.GetString(2);
                animalName = rdr.GetString(3);
                animalDate = rdr.GetDateTime(4);
                animalId = rdr.GetInt32(5);
            }

            Animal foundAnimal = new Animal(animalType, animalBreed, animalGender, animalName, animalDate, animalId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundAnimal;
        }
    }
}
