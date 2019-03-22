using System.Collections.Generic;
using Lab_Pyvovar.Models;

namespace Lab_Pyvovar.Tools.DataStorage
{
    internal interface IDataStorage
    {
        void AddPerson(Person person);
        void RemovePerson(Person person);
        void Sort(SortBy sort);
        List<Person> PeopleList { get; set; }
    }
}
