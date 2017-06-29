using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupAssigment.ViewModels
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }
        public string JobTitle { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string References { get; set; }
        public DateTime Date { get; set; }
    }
}