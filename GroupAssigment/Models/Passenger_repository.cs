using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupAssigment.ViewModels;
using System.Data.SqlClient;

namespace GroupAssigment.Models
{
    public class Passenger_repository
    {
        public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        internal void CreatePassenger(Paasengers pass,string gencode)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Insert into Passengers Values(@fn,@ln,@phone,@add,@to,@from,@price,@busn,Getdate(),@gencode)";
                cmd.Parameters.AddWithValue("@fn", pass.Firstname);
                cmd.Parameters.AddWithValue("@ln", pass.Lastname);
                cmd.Parameters.AddWithValue("@phone", pass.Phone);
                cmd.Parameters.AddWithValue("@add", pass.Addres);
                cmd.Parameters.AddWithValue("@to", pass.To);
                cmd.Parameters.AddWithValue("@from", pass.From);
                cmd.Parameters.AddWithValue("@price", pass.Price);
                cmd.Parameters.AddWithValue("@busn", pass.BusNumber);
                cmd.Parameters.AddWithValue("@gencode", gencode);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal List<Paasengers> GetPrint(string gencode)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Passengers Where Gencode=@gen";
                cmd.Parameters.AddWithValue("@gen", gencode);

                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<Paasengers>();
                if (reader.Read())
                {
                   Paasengers pass = new Paasengers();
                    pass.Firstname = reader["Firstname"] as string;
                    pass.To = reader["Tos"] as string;
                    pass.From = reader["Froms"] as string;
                    pass.Price = (double)reader["Price"];
                    pass.BusNumber = (int)reader["BusNumber"];
                    pass.Date = (DateTime)reader["Date"];
                    list.Add(pass);
                }
                return list;

            }
        }

        internal void DeletePassenger(string id)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Delete from Passengers Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        internal void UpdatePassenger(Paasengers pass)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Update Passengers set 
                                        Firstname=@fn,Lastname=@ln,
                                        Phone=@pho,Addres=@add,
                                        Tos=@to,Froms=@from,Price=@price,
                                        BusNumber=@bn Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", pass.Id);
                cmd.Parameters.AddWithValue("@fn", pass.Firstname);
                cmd.Parameters.AddWithValue("@ln", pass.Lastname);
                cmd.Parameters.AddWithValue("@pho", pass.Phone);
                cmd.Parameters.AddWithValue("@add", pass.Addres);
                cmd.Parameters.AddWithValue("@to", pass.To);
                cmd.Parameters.AddWithValue("@from", pass.From);
                cmd.Parameters.AddWithValue("@price", pass.Price);
                cmd.Parameters.AddWithValue("@bn", pass.BusNumber);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal Paasengers GetUpdatePasenger(string id)
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Passengers Where Id=@gen";
                cmd.Parameters.AddWithValue("@gen", id);

                conn.Open();
                var reader = cmd.ExecuteReader();
                Paasengers pass = null;
                if (reader.Read())
                {
                    pass = new Paasengers();
                    pass.Id = (int)reader["Id"];
                    pass.Firstname = reader["Firstname"] as string;
                    pass.Lastname = reader["Lastname"] as string;
                    pass.Phone = reader["Phone"] as string;
                    pass.Addres = reader["Addres"] as string;
                    pass.To = reader["Tos"] as string;
                    pass.From = reader["Froms"] as string;
                    pass.Price = (double)reader["Price"];
                    pass.BusNumber = (int)reader["BusNumber"];
                }
                return pass;

            }
        }

        internal List<Paasengers> ListPassenger(string name,string from,DateTime?datefrom=null,DateTime?dateto=null)
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Passengers Where (Firstname Like @name and Froms Like @from) ";
            
                if(datefrom.HasValue && dateto.HasValue)
                {
                    cmd.CommandText +=@"And Date between @datefrom and @dateto";
                    cmd.Parameters.AddWithValue("@datefrom", datefrom);
                    cmd.Parameters.AddWithValue("@dateto", dateto);
                }
                cmd.Parameters.AddWithValue("@name", name+"%%");
                cmd.Parameters.AddWithValue("@from", from+"%%");

                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<Paasengers>();
                if (reader.Read())
                {
                    Paasengers pass = new Paasengers();
                    pass.Id = (int)reader["Id"];
                    pass.Firstname = reader["Firstname"] as string;
                    pass.Lastname = reader["Lastname"] as string;
                    pass.Phone = reader["Phone"] as string;
                    pass.Addres = reader["Addres"] as string;
                    pass.To = reader["Tos"] as string;
                    pass.From = reader["Froms"] as string;
                    pass.Price = (double)reader["Price"];
                    pass.BusNumber = (int)reader["BusNumber"];
                    pass.Date = (DateTime)reader["Date"];
                    list.Add(pass);
                }
                return list;

            }
        }
    }
}