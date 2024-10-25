using FitTrack.Model;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace FitTrack.ViewModel
{
    internal class Accountmanager
    {
        private static List<Person> users = new List<Person>
        {
            new User("user1", "password1"),
            new AdminUser("admin", "password1")
        };

        // Kontrollerar användare för inloggning
        public bool ValidateLogin(string username, string password)
        {
            return users.Any(u => u.Username == username && u.Password == password);
        }
        // Lägger till användare (Register)
        public bool RegisterUser(string username, string password, string firstName, string lastName, string email, string country)
        {
            if (users.Any(u => u.Username == username))
            {
                return false; // Användarnamnet är upptaget
            }
            Person user = new User(username, password, firstName, lastName, email, country);
            users.Add(user);
            return true; // Registrering lyckades
        }

        // Returnera alla användare
        public static List<Person> GetAllUsers()
        {
            return users;
        }
    }
}