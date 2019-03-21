using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_Pyvovar.Models;

namespace Lab_Pyvovar.Tools.DataStorage
{
    internal interface IDataStorage
    {
        // bool UserExists(string firstName);

        Person GetUserByLogin(string firstName);

        void AddPerson(Person person);
        void RemovePerson(Person person);
        List<Person> UsersList { get; set; }
    }
}
