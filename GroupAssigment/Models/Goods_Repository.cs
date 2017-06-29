using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupAssigment.ViewModels;
using System.Data.SqlClient;

namespace GroupAssigment.Models
{
    public class Goods_Repository
    {
        public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        internal void CreateGoods(GoodsFrom goods)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Insert into GoodsFrom Values(@ns,@pho,@to,@from,@tyg,@nf,@phn,@price,getdate(),@bn)";
                cmd.Parameters.AddWithValue("@ns", goods.NameSender);
                cmd.Parameters.AddWithValue("@pho", goods.Phoneof);
                cmd.Parameters.AddWithValue("@to", goods.To);
                cmd.Parameters.AddWithValue("@from", goods.From);
                cmd.Parameters.AddWithValue("@tyg", goods.TypesofGood);
                cmd.Parameters.AddWithValue("@nf", goods.NameFrom);
                cmd.Parameters.AddWithValue("@phn", goods.PhoneNumber);
                cmd.Parameters.AddWithValue("@price", goods.Price);
                cmd.Parameters.AddWithValue("@bn", goods.BusNumber);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        internal GoodsFrom GetUpdateGoods(string id)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from GoodsFrom Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                var reader = cmd.ExecuteReader();
                GoodsFrom goods = null;
                if (reader.Read())
                {
                    goods = new GoodsFrom();
                    goods.Id = (int)reader["Id"];
                    goods.NameSender = reader["NameSender"] as string;
                    goods.Phoneof = reader["PhoneOf"] as string;
                    goods.To = reader["Tos"] as string;
                    goods.From = reader["Froms"] as string;
                    goods.TypesofGood = reader["TypeofGoods"] as string;
                    goods.NameFrom = reader["Namefrom"] as string;
                    goods.PhoneNumber = reader["PhoneNumber"] as string;
                    goods.Price = (double)reader["Price"];
                    goods.Date = (DateTime)reader["Date"];
                    goods.BusNumber = (int)reader["BusNumber"];

                }
                return goods;

            }
        }

        internal List<GoodsFrom> GetReportGoods(DateTime? datefrom, DateTime? dateto)
        {
            using (var conn = new SqlConnection(connection))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from GoodsFrom ";
                if (datefrom.HasValue && dateto.HasValue)
                {
                    cmd.CommandText += @"Where Date between @datefrom and @dateto";
                    cmd.Parameters.AddWithValue("@datefrom", datefrom);
                    cmd.Parameters.AddWithValue("@dateto", dateto);
                }

                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<GoodsFrom>();
                while (reader.Read())
                {
                    GoodsFrom goods = new GoodsFrom();
                    goods.Id = (int)reader["Id"];
                    goods.NameSender = reader["NameSender"] as string;
                    goods.Phoneof = reader["PhoneOf"] as string;
                    goods.To = reader["Tos"] as string;
                    goods.From = reader["Froms"] as string;
                    goods.TypesofGood = reader["TypeofGoods"] as string;
                    goods.NameFrom = reader["NameFrom"] as string;
                    goods.PhoneNumber = reader["PhoneNumber"] as string;
                    goods.Price = (double)reader["Price"];
                    goods.Date = (DateTime)reader["Date"];
                    goods.BusNumber = (int)reader["BusNumber"];

                    list.Add(goods);
                }
                return list;
            }
        }

        internal void UpdateGoods(GoodsFrom goods)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Update GoodsFrom Set 
                                        NameSender=@ns,PhoneOf=@pho,
                                        Tos=@to,Froms=@from,TypeofGoods=@tog,
                                        NameFrom=@nf,PhoneNumber=@phn,Price=@price,
                                        BusNumber=@bn Where Id=@Id";
                cmd.Parameters.AddWithValue("@ns", goods.NameSender);
                cmd.Parameters.AddWithValue("@pho", goods.Phoneof);
                cmd.Parameters.AddWithValue("@to", goods.To);
                cmd.Parameters.AddWithValue("@from", goods.From);
                cmd.Parameters.AddWithValue("@tog", goods.TypesofGood);
                cmd.Parameters.AddWithValue("@nf", goods.NameFrom);
                cmd.Parameters.AddWithValue("@phn", goods.PhoneNumber);
                cmd.Parameters.AddWithValue("@price", goods.Price);
                cmd.Parameters.AddWithValue("@bn", goods.BusNumber);
                cmd.Parameters.AddWithValue("@Id", goods.Id);

                conn.Open();
                cmd.ExecuteNonQuery();


            }
        }

        internal List<GoodsFrom> LIstGoods(string names,string phone,DateTime?Datefrom=null,DateTime?Dateto=null)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from GoodsFrom Where (NameSender Like @ns And PhoneOf Like @phone)";
                if(Datefrom.HasValue && Dateto.HasValue)
                {
                    cmd.CommandText += @"And Date between @datefrom and @dateto";
                    cmd.Parameters.AddWithValue("@datefrom", Datefrom);
                    cmd.Parameters.AddWithValue("@dateto", Dateto);
                }
                cmd.Parameters.AddWithValue("@ns", names+"%%");
                cmd.Parameters.AddWithValue("@phone", phone+"%%");
                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<GoodsFrom>();
                while (reader.Read())
                {
                    GoodsFrom goods = new GoodsFrom();
                    goods.Id = (int)reader["Id"];
                    goods.NameSender = reader["NameSender"] as string;
                    goods.Phoneof = reader["PhoneOf"] as string;
                    goods.To = reader["Tos"] as string;
                    goods.From = reader["Froms"] as string;
                    goods.TypesofGood = reader["TypeofGoods"] as string;
                    goods.NameFrom=reader["NameFrom"]as string;
                    goods.PhoneNumber = reader["PhoneNumber"] as string;
                    goods.Price = (double)reader["Price"];
                    goods.Date = (DateTime)reader["Date"];
                    goods.BusNumber = (int)reader["BusNumber"];

                    list.Add(goods);
                }
                return list;
            }
        }
    }
}