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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string SecQ { get; set; }
        public string SecA { get; set; }

        public User(string username, string password,string firstName, string lastName)
            : base(username, password, firstName, lastName)
        {

        }
        public User(string username, string password, string firstName, string lastName, string email, string country, string secQ, string secA)
            : base(username, password, firstName, lastName)
        {
            Email = email;
            Country = country;
            SecQ = secQ;
            SecA = secA;
        }
        public override string GetRole()
        {
            return "User";
        }

        public override void SignIn()
        {
            MessageBox.Show("You are a User", "User");
        }
    }
}
