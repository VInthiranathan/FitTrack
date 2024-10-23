using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class AdminUser : Person
    {
        public AdminUser(string username, string password) : base(username, password)
        {

        }

        public override void SignIn()
        {
            
        }
    }
}
