﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    public class User : Person
    {
        public string Country { get; set; }
        public string SecurityQ { get; set; }
        public string SecurityA {  get; set; }
        public User(string username, string password) : base(username, password)
        {

        }
        public void SecurityCheck(string securityQ, string securityA)
        {
            SecurityQ = securityQ;
            SecurityA = securityA;
        }
        public override void SignIn()
        {
            
        }
    }
}
