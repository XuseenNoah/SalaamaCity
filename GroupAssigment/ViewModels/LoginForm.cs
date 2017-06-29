using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroupAssigment.ViewModels
{
    public class LoginForm
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public Permissions CurrentPermissions { get; set; }


        public enum Permissions
        {
            None = 0,
            Update = 1 << 0,
            Delete = 1 << 1,
            Add = 1 << 2,
            View = 1 << 3 | Add,
            Super = (Update | View),
            Admin = (Update | Delete | Add | View)
        }
    }
}