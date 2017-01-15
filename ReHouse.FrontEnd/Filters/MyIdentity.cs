using System.Security.Principal;

namespace ReHouse.FrontEnd.Filters
{
    public class MyIdentity : IIdentity
    {
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }

        public MyIdentity(string name, string authenticationType, bool isAuthenticated)
        {
            Name = name;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
        }
    }
}