using System;
using ReHouse.Utils.DataBase.Security;
using System.Collections.Generic;

namespace ReHouse.FrontEnd.Models
{
    public class SessionModel
    {
        public User User { get; set; }
        public String Adress { get; set; }
        public String Name { get; set; }
        public String SecondName { get; set; }
        public String FatherName { get; set; }
        public String Email { get; set; }
        public String TokenHash { get; set; }
        public Role Role { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Authority> Authorities { get; set; }        
    }
}