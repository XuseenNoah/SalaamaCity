using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupAssigment.ViewModels
{
    public class Paasengers
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Addres { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public double Price { get; set; }
        public int BusNumber { get; set; }
        public DateTime Date { get; set; }

    }
}