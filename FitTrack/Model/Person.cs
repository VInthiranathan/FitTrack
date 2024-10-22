using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public abstract class Person
    {
        string Username { get; set; }
        string Password { get; set; }

        public Person(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public abstract void SignIn();
    }
}
