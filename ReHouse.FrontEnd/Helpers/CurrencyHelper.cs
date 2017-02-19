using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReHouse.FrontEnd.Helpers
{
    public class CurrencyHelper
    {
        public Valuta EUR { get; set; }
        public Valuta RUB { get; set; }
        public Valuta USD { get; set; }
    }
    public class Valuta
    {
        public Bank interbank { get; set; }
        public Bank nbu { get; set; }
    }

    public class Bank
    {
        public double buy { get; set; }
        public double sell { get; set; }
    }
}