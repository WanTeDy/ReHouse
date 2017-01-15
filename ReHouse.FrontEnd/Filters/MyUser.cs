using System;
using System.Security.Principal;


namespace ReHouse.FrontEnd.Filters
{
    public class MyUser : IPrincipal
    {
        public String Role { get; set; }
        public bool IsInRole(string role)
        {
            return Role == role;
        }
        public IIdentity Identity { get; private set; }

        public MyUser(string role, string name, string authenticationType, bool isAuthenticated)
        {
            Role = role;
            Identity = new MyIdentity(name, authenticationType, isAuthenticated);
        }
    }
}