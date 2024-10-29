using FitTrack.Model;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace FitTrack.ViewModel
{
    public class Accountmanager
    {
        private static List<Person> users = new List<Person>
        {
            new User("user1", "password1", "Lewis", "Hamilton"),
            new User("user2", "password1", "George", "Russell"),
            new AdminUser("admin", "password1", "Hassan", "Hussin")
        };
        public Person CurrentUser { get; set; }

        // Kontrollerar användare för inloggning
        public bool ValidateLogin(string username, string password)
        {
            var user = users.OfType<Person>().FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Sparar användare som loggar in
                CurrentUser = user;
                return true;
            }
            return false;
        }
        // Lägger till användare (Register)
        public bool RegisterUser(string username, string password, string firstName, string lastName, string email, string country, string secQ, string secA)
        {
            if (users.Any(u => u.Username == username))
            {
                return false; // Användarnamnet är upptaget
            }
            Person user = new User(username, password, firstName, lastName, email, country, secQ, secA);
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