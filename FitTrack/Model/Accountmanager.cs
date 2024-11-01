using FitTrack.Model;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

namespace FitTrack.ViewModel
{
    public class Accountmanager
    {
        public static List<Person> Users = new List<Person>
        {
            new User("user1", "password1", "Lewis", "Hamilton", "What comes after A", "B"),
            new User("user2", "password1", "George", "Russell","What comes after A", "B"),
            new AdminUser("admin", "password1", "Hassan", "Hussin","What comes after A", "B")
        };

        public Person CurrentUser { get; set; }

        public Workout TemplateWorkout { get; set; }

        // Förskapad träningspass
        public static List<Workout> Workouts = new List<Workout>
        {
            new CardioWorkout
            {
                Date = DateTime.Today,
                Type = "Running",
                Duration = TimeSpan.FromMinutes(20),
                Owner = Users[0] 
            },
            new CardioWorkout
            {
                Date = DateTime.Today.AddDays(-3),
                Type = "Cycling",
                Duration = TimeSpan.FromMinutes(30),
                Owner = Users[1]
            },
            new StrengthWorkout
            {
                Date = DateTime.Today.AddDays(-7),
                Type = "Push-ups",
                Repetitions = 33,
                Owner = Users[0]
            }
        };

        static Accountmanager()
        {
            // Träningspass till ägaresns lista
            foreach (var workout in Workouts)
            {
                if (workout.Owner is User ownerUser)
                {
                    ownerUser.Workouts.Add(workout);
                }
            }
        }
        public static IEnumerable<Workout> GetAllWorkouts()
        {
            return Workouts;
        }

        public static IEnumerable<Workout> GetUserWorkouts(User user)
        {
            return user.Workouts;
        }

        // Kontroll för inloggning
        public bool ValidateLogin(string username, string password)
        {
            var user = Users.OfType<Person>().FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Spara user som loggar in
                CurrentUser = user;
                return true;
            }
            return false;
        }
        // Lägger till user
        public bool RegisterUser(string username, string password, string firstName, string lastName, string email, string country, string secQ, string secA)
        {
            if (Users.Any(u => u.Username == username))
            {
                return false; // Användarnamnet är upptaget
            }
            Person user = new User(username, password, firstName, lastName, email, country, secQ, secA);
            Users.Add(user);
            return true; // Registrering lyckades
        }

        // Returnera alla användare
        public static List<Person> GetAllUsers()
        {
            return Users;
        }
        public static void RemoveWorkout(Person currentUser, Workout workout)
        {
            if (currentUser is AdminUser)
            {
                Workouts.Remove(workout);
                foreach (var user in Users.OfType<User>())
                {
                    if (user.Workouts.Contains(workout))
                    {
                        user.Workouts.Remove(workout);
                        break;
                    }
                }
            }
            else if (currentUser is User user)
            {
                user.Workouts.Remove(workout);
                Workouts.Remove(workout);
            }

        }
        public bool UpdateUser(string username, string password, string firstName, string lastName, string email, string country)
        {
            CurrentUser.Username = username;
            CurrentUser.Password = password;
            CurrentUser.FirstName = firstName;
            CurrentUser.LastName = lastName;
            CurrentUser.Email = email;
            CurrentUser.Country = country;
            return true;
            // Uppdaterar användarens värde
        }

        public bool GetPassword(string username)
        {
            var user = Users.OfType<Person>().FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                // Sätter anvädare till CurrentUser (för forgotpassword)
                CurrentUser = user;
                return true;
            }
            return false;
        }
    }
}