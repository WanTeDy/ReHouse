using System;
using System.Collections.Generic;
using System.Threading;
using ITfamily.Utils.DataBase.Security;
using ITfamily.Utils.Helpers;

namespace ITfamily.Utils
{
    public sealed class StaticAuth
    {
        private static readonly StaticAuth Instance = new StaticAuth();
        public List<UiWindowsState> UiWindowsStates { get; set; }
        public String TokenHash { get; set; }
        //public StatusRole StatusRole { get; set; }
        public Thread Thread { get; set; }
        /// <summary>
        /// For api.brain.com.ua
        /// </summary>
        public String Login { get; set; }

        public Role Role { get; set; }
        /// <summary>
        /// For api.brain.com.ua
        /// in MD5
        /// </summary>
        public String Password { get; set; }
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static StaticAuth()
        {
            Obj.Login = "";
            Obj.Password = "";
        }

        private StaticAuth()
        {
            UiWindowsStates = new List<UiWindowsState>();
        }

        public static StaticAuth Obj
        {
            get
            {
                return Instance;
            }
        }
    }
}