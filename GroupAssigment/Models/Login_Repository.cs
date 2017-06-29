using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GroupAssigment.ViewModels;
using System.Data.SqlClient;

namespace GroupAssigment.Models
{
    public class Login_Repository
    {
        public string connection = System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        internal LoginForm Authenticate(string username, string password)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Select * From Users Where Username=@user and Password=@pass";
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);

                conn.Open();
                var reader = cmd.ExecuteReader();
                LoginForm log = null;
                if (reader.Read())
                {
                    log = new LoginForm();
                    log.Username = reader["Username"]as string;
                    log.Password = reader["Password"] as string;
                    if (reader["CurrentPermision"] != DBNull.Value)
                    {
                        log.CurrentPermissions = (LoginForm.Permissions)reader["CurrentPermision"];
                    }


                }
                return log;
            }
        }

        internal void CreateUser(LoginForm login)
        {
            using (var conn = new SqlConnection(connection))
                using(var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"Insert Into Users Values(@fn,@ln,@add,@em,@phone,@user,@pass,@current,Getdate())";
                cmd.Parameters.AddWithValue("@fn", login.Firstname);
                cmd.Parameters.AddWithValue("@ln", login.Lastname);
                cmd.Parameters.AddWithValue("@add", login.Addres);
                cmd.Parameters.AddWithValue("@em", login.Email);
                cmd.Parameters.AddWithValue("@phone", login.Phone);
                cmd.Parameters.AddWithValue("@user", login.Username);
                cmd.Parameters.AddWithValue("@pass", login.Password);
                cmd.Parameters.AddWithValue("@current", login.CurrentPermissions);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}