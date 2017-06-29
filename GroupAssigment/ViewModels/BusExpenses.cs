using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupAssigment.ViewModels
{
    public class BusExpenses
    {
        public int Id { get; set; }
        public int BusNumber { get; set; }
        public string BusDriverName { get; set; }
        public double AmountExpenses { get; set; }
        public DateTime Date { get; set; }
    }
}