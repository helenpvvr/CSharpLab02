using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab_Pyvovar.Models;
using Lab_Pyvovar.Tools.Managers;

namespace Lab_Pyvovar.Tools.DataStorage
{
    internal class SerializedDataStorage:IDataStorage
    {
        private List<Person> _people;

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

        public List<Person> PeopleList
        {
            get { return _people.ToList(); }
            set { _people = value; }
        }

        public void Sort(SortBy sort)
        {
            PeopleList = Sorting.ToSortBy(sort);
            SaveChanges();
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_people, FileFolderHelper.StorageFilePath);
        }
    }
}