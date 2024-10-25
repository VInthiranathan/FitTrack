using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.Model
{
    public class AdminUser : Person
    {
        public AdminUser(string username, string password) : base(username, password)
        {

        }

        public override void SignIn()
        {
            MessageBox.Show("You are a Admin user", "User");
        }
    }
}
