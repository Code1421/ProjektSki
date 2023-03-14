using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ProjektSki.Models;


namespace ProjektSki.DAL
{
    public class LoginDB
    {
        public static List<SiteUser> LoadUsers()
        {   
            /*
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            var sql = "SELECT * FROM LoginData";
            var cmd = new SqlCommand(sql, con);
            con.Open();
            var data = cmd.ExecuteReader();
            List<SiteUser> usersList = new List<SiteUser>();
            while (data.Read())
            {
                SiteUser user = new SiteUser();
                user.userName = data["userName"].ToString().Replace(" ", "");
                user.password = data["password"].ToString().Replace(" ", "");
                usersList.Add(user);
            }
            data.Close();
            con.Close();
            return usersList;
            */

            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            SqlCommand cmd = new SqlCommand("sp_userGet", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            List<SiteUser> usersList = new List<SiteUser>();
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                SiteUser u = new SiteUser();
                u.userName= reader[0].ToString().Replace(" ", "");
                u.password = reader[1].ToString().Replace(" ", "");
                u.role = reader[2].ToString().Replace(" ", "");
                usersList.Add(u);
            }

            reader.Close();
            con.Close();
            return usersList;
        }
        public static SiteUser LoadUser(SiteUser p)
        {
            SiteUser us = new SiteUser();
            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            SqlCommand cmd = new SqlCommand("sp_userGetUser", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userName", p.userName);
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                us.userName = reader[0].ToString().Replace(" ", "");
                us.password = reader[1].ToString().Replace(" ", "");
                us.role = reader[2].ToString().Replace(" ", "");
            }
            reader.Close();
            con.Close();


            return us;
            
        }


        //dodawanie nowego uzytkownika- bez hashowania hasel(trzeba to dorobic)
        public static string Hasher(string password)
        {
            //Console.Write("Enter a password: ");
            //string password = Console.ReadLine();

            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            /*using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            */
            salt[0] = 198; salt[1] = 63; salt[2] = 118; salt[3] = 139; salt[4] = 224; salt[5] = 224; salt[6] = 106; salt[7] = 251; salt[8] = 192; salt[9] = 152; salt[10] = 83; salt[11] = 74; salt[12] = 36; salt[13] = 100; salt[14] = 64; salt[15] = 17;
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
            //return $"{password}.{salt}";
            //Console.WriteLine($"Hashed: {hashed}");
        }
        public static void Create(SiteUser p)
        {

            var con = new SqlConnection(MyAppData.Configuration.GetConnectionString("MyCompanyDB"));
            // do procedur
            SqlCommand cmd = new SqlCommand("sp_userAdd", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userName", p.userName);
            cmd.Parameters.AddWithValue("@password", Hasher(p.password));
            cmd.Parameters.AddWithValue("@role", p.role);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();

            con.Close();
            /*var sql = "INSERT INTO LoginData (userName, password) VALUES (@userName, @password) ";
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@userName", p.userName);
            cmd.Parameters.AddWithValue("@password", Hasher(p.password));
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            */
        }
    }
}
