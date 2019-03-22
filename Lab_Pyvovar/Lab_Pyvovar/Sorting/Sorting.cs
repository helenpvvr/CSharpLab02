using System.Collections.Generic;
using System.Linq;
using Lab_Pyvovar.Models;
using Lab_Pyvovar.Tools.Managers;

namespace Lab_Pyvovar
{
    internal static class Sorting
    {
        internal static List<Person> ToSortBy(SortBy property)
        {
            switch (property)
            {
                case SortBy.SortingByFirstName:
                    return StationManager.DataStorage.PeopleList.OrderBy(p => p.FirstName).ToList();
                case SortBy.SortingByLastName:
                    return StationManager.DataStorage.PeopleList.OrderBy(p => p.LastName).ToList();
                case SortBy.SortingByEmail:
                    return StationManager.DataStorage.PeopleList.OrderBy(p => p.Email).ToList();
                case SortBy.SortingByBirthday:
                    return StationManager.DataStorage.PeopleList.OrderBy(p => p.Birthday).ToList();
                case SortBy.SortingByIsAdult:
                    return StationManager.DataStorage.PeopleList.OrderBy(p => p.IsAdult).ToList();
                case SortBy.SortingBySunSign:
                    return StationManager.DataStorage.PeopleList.OrderBy(p => p.SunSign).ToList();
                case SortBy.SortingByChineseSign:
                    return StationManager.DataStorage.PeopleList.OrderBy(p => p.ChineseSing).ToList();
                case SortBy.SortingByIsBirthday:
                    return StationManager.DataStorage.PeopleList.OrderBy(p => p.IsBirthday).ToList();
                default:
                    return StationManager.DataStorage.PeopleList;
            }
        }
    }
}