using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using ProjektSki.Models;

namespace ProjektSki.DAL
{
    public class ProductDB
    {
        public static List<Category> LoadCategory()
        {
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            SqlCommand cmd = new SqlCommand("sp_categoryGet", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            List<Category> categoryList = new List<Category>();
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Category c = new Category();
                c.Id = Int32.Parse(reader[0].ToString());
                c.ShortName = reader[1].ToString();
                c.LongName = reader[2].ToString();

                categoryList.Add(c);
            }

            reader.Close();
            con.Close();
            return categoryList;
        }
        public static void Create(Category p)
        {
            //dorobic dodawanie do kategorii !!!!!!!!!!!!!!!!!!!!!!!
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            SqlCommand cmd = new SqlCommand("sp_categoryDelete", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ShortName", p.ShortName);
            cmd.Parameters.AddWithValue("@LongName", p.LongName);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();

            con.Close();
        }
        public static void Delete(int Id)
        {
            Category product = new Category();
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            SqlCommand cmd = new SqlCommand("sp_categoryDelete", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static Category Edit(Category p)
        {
            // edit ma problem z FOREIGN KEY 
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            SqlCommand cmd = new SqlCommand("sp_categoryEdit", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@ShortName", p.ShortName);
            cmd.Parameters.AddWithValue("@LongName", p.LongName);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return p;
        }
        public static Category GetCategory(int id)
        {
            Category category = new Category();
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            SqlCommand cmd = new SqlCommand("sp_categoryGetProduct", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                category.Id = Int32.Parse(reader[0].ToString());
                category.ShortName = reader[1].ToString();
                category.LongName = reader[2].ToString();
                /*
                category.name = reader[1].ToString();
                category.price = (decimal)Convert.ToDecimal(reader[2].ToString());
                category.CategoryId = Int32.Parse(reader[3].ToString());
                */
            }
            reader.Close();
            con.Close();


            return category;
        }

        //--------------producent---------------- ADO
        public static List<Producer> LoadProducer()
        {
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            var sql = "SELECT * FROM Producer_1";
            var cmd = new SqlCommand(sql, con);
            con.Open();
            var data = cmd.ExecuteReader();
            List<Producer> producerList = new List<Producer>();
            while (data.Read())
            {
                Producer producer = new Producer();
                producer.Id = int.Parse(data["Id"].ToString());
                producer.Name = data["Name"].ToString();
                producer.Country = data["Country"].ToString();
                producerList.Add(producer);
            }
            data.Close();
            con.Close();
            return producerList;
        }

        public static void ProducerCreate(Producer p)
        {
            //var conString = MyAppData.Configuration.GetConnectionString("MyCompanyDB");
            //var conn = new SqlConnection(conString);
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            var sql = "INSERT INTO Producer_1 (Name, Country) VALUES (@Name, @Country) ";
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", p.Name);
            cmd.Parameters.AddWithValue("@Country", p.Country);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void ProducerDelete(int Id)
        {
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            var sql = "DELETE FROM Producer_1 WHERE Id = @Id";
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static Producer ProducerEdit(Producer p)
        {
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            var sql = "UPDATE Producer_1 SET Name = @Name, Country = @Country WHERE Id = @Id";
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", p.Id);
            cmd.Parameters.AddWithValue("@Name", p.Name);
            cmd.Parameters.AddWithValue("@Country", p.Country);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return p;
        }

    }
}
