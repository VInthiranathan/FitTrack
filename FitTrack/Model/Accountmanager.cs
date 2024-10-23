using FitTrack.Model;

namespace FitTrack.ViewModel
{
    internal class Accountmanager
    {
        private List<Person> users = new List<Person>
        {
            new User("User1", "password1"),
            new AdminUser("admin", "password1")
        };


        // Kontrollerar användare för inloggning
        public bool ValidateLogin(string username, string password)
        {
            return users.Any(u => u.Username == username && u.Password == password);
        }
        // Lägger till användare (Register)
        public bool RegisterUser(string username, string password)
        {
            if (users.Any(u => u.Username == username))
            {
                return false; // Användarnamnet finns redan
            }

            users.Add(new User(username, password));
            return true; // Registrering lyckades
        }
    }
}