using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupAssigment.ViewModels;
using System.Data.SqlClient;

namespace GroupAssigment.Models
{
    public class BusExpensesRepositroy
    {
        public string Connection = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        internal void Create(BusExpenses busexp)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Insert into BusExpensive Values(@busn,@bdn,@amount,getdate())";
                cmd.Parameters.AddWithValue("@busn", busexp.BusNumber);
                cmd.Parameters.AddWithValue("@bdn", busexp.BusDriverName);
                cmd.Parameters.AddWithValue("@amount", busexp.AmountExpenses);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal List<BusExpenses> List(string busnumber,string busdrivername,DateTime? Datefrom=null,DateTime? Dateto=null)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from BusExpensive Where (BusNumber Like @bn And BusDriverName Like @Bdn) ";
                if(Datefrom.HasValue && Dateto.HasValue)
                {
                    cmd.CommandText += @"And Date between @Datefrom And @Dateto";
                    cmd.Parameters.AddWithValue("@Datefrom", Datefrom);
                    cmd.Parameters.AddWithValue("@Dateto", Dateto);
                }
                cmd.Parameters.AddWithValue("@bn", busnumber + "%%");
                cmd.Parameters.AddWithValue("@Bdn", busdrivername + "%%");
                conn.Open();

                var reader = cmd.ExecuteReader();
                var list = new List<BusExpenses>();
                while (reader.Read())
                {
                    BusExpenses bus = new BusExpenses();
                    bus.Id = (int)reader["Id"];
                    bus.BusNumber = (int)reader["BusNumber"];
                    bus.BusDriverName = reader["BusDriverName"] as string;
                    bus.AmountExpenses = (double)reader["AmountExpensive"];
                    bus.Date = (DateTime)reader["Date"];
                    list.Add(bus);

                }
                return list;
            }
        }

        internal void Delete(BusExpenses bus)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Delete from BusExpensive Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", bus.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal void Update(BusExpenses bus)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Update BusExpensive set 
BusNumber=@bus,BusDriverName=@bdn,AmountExpensive=@am where Id=@Id";
                cmd.Parameters.AddWithValue("@bus", bus.BusNumber);
                cmd.Parameters.AddWithValue("@bdn", bus.BusDriverName);
                cmd.Parameters.AddWithValue("@am", bus.AmountExpenses);
                cmd.Parameters.AddWithValue("@Id", bus.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal List<BusExpenses> GetReport(DateTime? datefrom, DateTime? dateto)
        {
            using (var conn = new SqlConnection(Connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from BusExpensive ";
                if (datefrom.HasValue && dateto.HasValue)
                {
                    cmd.CommandText += @"Where Date between @Datefrom And @Dateto";
                    cmd.Parameters.AddWithValue("@Datefrom", datefrom);
                    cmd.Parameters.AddWithValue("@Dateto", dateto);
                }

                conn.Open();

                var reader = cmd.ExecuteReader();
                var list = new List<BusExpenses>();
                while (reader.Read())
                {
                    BusExpenses bus = new BusExpenses();
                    bus.Id = (int)reader["Id"];
                    bus.BusNumber = (int)reader["BusNumber"];
                    bus.BusDriverName = reader["BusDriverName"] as string;
                    bus.AmountExpenses = (double)reader["AmountExpensive"];
                    bus.Date = (DateTime)reader["Date"];
                    list.Add(bus);

                }
                return list;
            }
        }

        internal BusExpenses GetUpdate(string id)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from BusExpensive Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                var reader = cmd.ExecuteReader();
                BusExpenses bus = null;
                while (reader.Read())
                {
                    bus = new BusExpenses();
                    bus.Id = (int)reader["Id"];
                    bus.BusNumber = (int)reader["BusNumber"];
                    bus.BusDriverName = reader["BusDriverName"] as string;
                    bus.AmountExpenses = (double)reader["AmountExpensive"];
                    
                }
                return bus;

            }
            
        }
    }
}