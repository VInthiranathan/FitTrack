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

        public User(string username, string password) : base(username, password)
        {

        }
        public User(string username, string password, string firstName, string lastName, string email, string country)
            : base(username, password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Country = country;
        }

        public override void SignIn()
        {
            MessageBox.Show("You are a User", "User");
        }
    }
}
