using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab_Pyvovar.Models;
using Lab_Pyvovar.Tools.Managers;

namespace Lab_Pyvovar.Tools.DataStorage
{
    internal class SerializedDataStorage:IDataStorage
    {
        private List<Person> _people; // readOnly

        internal SerializedDataStorage()
        {
            try
            {
                _people = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _people = new List<Person>();
            }
        }

        public Person GetUserByLogin(string firstName)
        {
            return _people.FirstOrDefault(u => u.FirstName == firstName);
        }

        public void AddPerson(Person user)
        {
            _people.Add(user);
            SaveChanges();
        }

        public void RemovePerson(Person user)
        {
            _people.Remove(user);
            SaveChanges();
        }

        public List<Person> UsersList
        {
            get { return _people.ToList(); }
            set { _people = value; }
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_people, FileFolderHelper.StorageFilePath);
        }
    }
}