using System;
using System.Windows.Forms;
using Lab_Pyvovar.Models;
using Lab_Pyvovar.Tools.DataStorage;

namespace Lab_Pyvovar.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;

        private static IDataStorage _dataStorage;

        internal static Person CurrentPerson { get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            if (_dataStorage.PeopleList.Count == 0)
                GenerateRandomPeople();
        }

        internal static void CloseApp()
        {
            //MessageBox.Show("Close Window");
            //StopThreads?.Invoke();
            Environment.Exit(1);
        }

        private static void GenerateRandomPeople()
        {
            String[] firstNames =
            {
                "Amelia", "Olivia", "Sophie", "Mia",  "Helen", "Ella", "Alice",
                "Lucy", "Rosie", "Anna", "Sarah", "Oscar", "William", "Henry",
                "Leo", "Max", "Harrison", "Jake", "David", "Tommy", "Frankie"
            };
            String[] lastNames =
            {
                "Smith", "Johnson", "Wilson", "Miller", "Davis", "Brown", "Jones",
                "Williams", "Adams", "Green", "Baker", "Roberts", "Allen", "Parker"
            };

            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                String firstName = firstNames[random.Next(firstNames.Length)];
                String lastName = lastNames[random.Next(lastNames.Length)];
                String email = firstName + lastName + random.Next(1000) + "@gmail.com";
                int year = random.Next(DateTime.Today.Year - 134, DateTime.Today.Year);
                int month = random.Next(1, 13);
                int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
                try
                {
                    Person person = new Person(firstName, lastName, email, new DateTime(year, month, day));
                    DataStorage.AddPerson(person);
                    CurrentPerson = person;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
    }

}