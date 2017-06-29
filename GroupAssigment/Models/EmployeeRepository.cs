using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupAssigment.ViewModels;
using System.Data.SqlClient;

namespace GroupAssigment.Models
{
    public class EmployeeRepository
    {
        public string Connection = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        internal void CreateEmployee(Employee emp)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Insert into Employee 
Values(@fn,@ln,@ad,@ph,@jt,@age,@gender,@ref,getdate())";
                cmd.Parameters.AddWithValue("@fn", emp.Firstname);
                cmd.Parameters.AddWithValue("@ln", emp.Lastname);
                cmd.Parameters.AddWithValue("@ad", emp.Addres);
                cmd.Parameters.AddWithValue("@ph", emp.Phone);
                cmd.Parameters.AddWithValue("@jt", emp.JobTitle);
                cmd.Parameters.AddWithValue("@age", emp.Age);
                cmd.Parameters.AddWithValue("@gender", emp.Gender);
                cmd.Parameters.AddWithValue("@ref", emp.References);

                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        internal Employee GetUpdate(string id)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Employee Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                var reader = cmd.ExecuteReader();
                Employee emp = null;
                while (reader.Read())
                {
                    emp = new Employee();
                    emp.Id = (int)reader["Id"];
                    emp.Firstname = reader["Firstname"] as string;
                    emp.Lastname = reader["Lastname"] as string;
                    emp.Addres = reader["Addres"] as string;
                    emp.Phone = reader["Phone"] as string;
                    emp.JobTitle = reader["JobTitle"] as string;
                    emp.Age = (int)reader["Age"];
                    emp.Gender = reader["Gender"] as string;
                    emp.References = reader["Reference"] as string;

                }
                return emp;
            }
        }

        internal List<Sallery> ListSellary(string first,string last,DateTime?datefrom=null,DateTime?dateto=null)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Sallery Where (Firstname Like @first and Lastname Like @last) ";
                if(datefrom.HasValue&& dateto.HasValue)
                {
                    cmd.CommandText += @"And Date between @datef and @datet";
                    cmd.Parameters.AddWithValue("@datef", datefrom);
                    cmd.Parameters.AddWithValue("@datet", dateto);
                }
                cmd.Parameters.AddWithValue("@first", first+"%%");
                cmd.Parameters.AddWithValue("@last", last+"%%");

                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<Sallery>();
                while (reader.Read())
                {
                    Sallery sell = new Sallery();
                    sell.Id = (int)reader["Id"];
                    sell.Firstname = reader["Firstname"] as string;
                    sell.Lastname = reader["Lastname"] as string;
                    sell.Addres = reader["Addres"] as string;
                    sell.Phone = reader["Phone"] as string;
                    sell.Email = reader["Email"] as string;
                    sell.JobTitle = reader["JobTitle"] as string;
                    sell.Salery = (double)reader["Sallery"];
                    sell.Gender = reader["Gender"] as string;
                    list.Add(sell);
                }
                return list;
            }
        }

        internal void DeleteSallery(string id)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Delete from Sallery Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal void UpdateSallery(Sallery sell)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Update Sallery Set 
                                                Firstname=@first,Lastname=@last,
                                                Addres=@add,Phone=@Phone,
                                                Email=@em,JobTitle=@jt,Sallery=@sel
                                                        Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", sell.Id);
                cmd.Parameters.AddWithValue("@first", sell.Firstname);
                cmd.Parameters.AddWithValue("@last", sell.Lastname);
                cmd.Parameters.AddWithValue("@add", sell.Addres);
                cmd.Parameters.AddWithValue("@Phone", sell.Phone);
                cmd.Parameters.AddWithValue("@em", sell.Email);
                cmd.Parameters.AddWithValue("@jt", sell.JobTitle);
                cmd.Parameters.AddWithValue("@sel", sell.Salery);

                conn.Open();
                cmd.ExecuteNonQuery();


            }
        }

        internal Sallery GetUpdateSallery(string id)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Sallery Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                var reader = cmd.ExecuteReader();
                Sallery sell = null;
                if (reader.Read())
                {
                    sell = new Sallery();
                    sell.Id = (int)reader["Id"];
                    sell.Firstname = reader["Firstname"] as string;
                    sell.Lastname = reader["Lastname"] as string;
                    sell.Addres = reader["Addres"] as string;
                    sell.Phone = reader["Phone"] as string;
                    sell.Email = reader["Email"] as string;
                    sell.JobTitle = reader["JobTitle"] as string;
                    sell.Salery = (double)reader["Sallery"];
                    sell.Gender = reader["Gender"] as string;

                }
                return sell;
            }
        }

        internal void CreateSallery(Sallery sell)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Insert Into Sallery Values(@fn,@ln,@ad,@pho,@em,@jt,@sall,@gender,getdate())";
                cmd.Parameters.AddWithValue("@fn", sell.Firstname);
                cmd.Parameters.AddWithValue("@ln", sell.Lastname);
                cmd.Parameters.AddWithValue("@ad", sell.Addres);
                cmd.Parameters.AddWithValue("@pho", sell.Phone);
                cmd.Parameters.AddWithValue("@em", sell.Email);
                cmd.Parameters.AddWithValue("@jt", sell.JobTitle);
                cmd.Parameters.AddWithValue("@sall", sell.Salery);
                cmd.Parameters.AddWithValue("@gender", sell.Gender);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal void Delete(Employee emp)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Delete from Employee where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        internal void Update(Employee emp)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Update Employee Set 
Firstname=@fn,Lastname=@ln,
Addres=@ad,Phone=@ph,
Age=@age,Reference=@ref
Where Id=@Id";
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.Parameters.AddWithValue("@fn", emp.Firstname);
                cmd.Parameters.AddWithValue("@ln", emp.Lastname);
                cmd.Parameters.AddWithValue("@ad", emp.Addres);
                cmd.Parameters.AddWithValue("@ph", emp.Phone);
                cmd.Parameters.AddWithValue("@age", emp.Age);
                cmd.Parameters.AddWithValue("@ref", emp.References);

                conn.Open();
                cmd.ExecuteNonQuery();
                
            }
        }

        internal List<Employee> ListEmployee(string name,string phone,DateTime?datefrom=null,DateTime?dateto=null)
        {
            using (var conn = new SqlConnection(Connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select *from Employee Where (Firstname Like @name And Phone Like @phone)";
                if(datefrom.HasValue && dateto.HasValue)
                {
                    cmd.CommandText += @"And Date between @datefrom and @dateto";
                    cmd.Parameters.AddWithValue("@datefrom", datefrom);
                    cmd.Parameters.AddWithValue("@dateto", dateto);
                }
                cmd.Parameters.AddWithValue("@name", name+"%%");
                cmd.Parameters.AddWithValue("@phone", phone + "%%");
                conn.Open();
                var reader = cmd.ExecuteReader();
                var list = new List<Employee>();
                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.Id = (int)reader["Id"];
                    emp.Firstname = reader["Firstname"] as string;
                    emp.Lastname = reader["Lastname"] as string;
                    emp.Addres = reader["Addres"] as string;
                    emp.Phone = reader["Phone"] as string;
                    emp.JobTitle = reader["JobTitle"] as string;
                    emp.Age = (int)reader["Age"];
                    emp.Gender = reader["Gender"] as string;
                    emp.References = reader["Reference"] as string;
                    emp.Date = (DateTime)reader["Date"];
                    list.Add(emp);
                }
                return list;
            }
        }
    }
}