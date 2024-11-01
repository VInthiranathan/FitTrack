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
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; internal set; }
        public string Country { get; internal set; }
        public string SecQ { get; set; }
        public string SecA { get; set; }

        public Person(string username, string password, string firstName, string lastName, string secQ, string secA)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            SecQ = secQ;
            SecA = secA;

        }
        public string GetSecQ()
        {
            return SecQ;
        }
        public abstract void SignIn();
        public abstract string GetRole();
    }
}
