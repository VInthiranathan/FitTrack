using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public class User : Person
    {
        public string EMail { get; set; }
        public string Country { get; set; }
        public string SecurityQ { get; set; }
        public string SecurityA {  get; set; }
        public User(string username, string password) : base(username, password)
        {

        }
        public User(string username, string password, string email, string country, string securityQ, string securityA) : base(username, password)
        {
            EMail = email;
            Country = country;
            SecurityQ = securityQ;
            SecurityA = securityA;
        }
        public void SecurityCheck(string securityQ, string securityA)
        {
            SecurityQ = securityQ;
            SecurityA = securityA;
        }
        public override void SignIn()
        {
            MessageBox.Show("You are a User", "User");
        }
    }
}
