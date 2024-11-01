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

        public List<Workout> Workouts { get; set; } = new List<Workout>();
        public User(string username, string password,string firstName, string lastName, string secQ, string secA)
            : base(username, password, firstName, lastName, secQ, secA)
        {

        }
        public User(string username, string password, string firstName, string lastName, string secQ, string secA, string email, string country)
            : base(username, password, firstName, lastName, secQ, secA)
        {
            Email = email;
            Country = country;
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
