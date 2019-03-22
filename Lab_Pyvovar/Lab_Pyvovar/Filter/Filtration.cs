using System;
using System.Collections.Generic;
using System.Linq;
using Lab_Pyvovar.Models;
using Lab_Pyvovar.Tools.Managers;

namespace Lab_Pyvovar.Filter
{
    internal static class Filtration
    {
        internal static List<Person> ToFilter(List<Person> listPeople, string firstNameFilter, string lastNameFilter, 
            string emailFilter, DateTime dateFromFilter, DateTime dateToFilter)
        {
            var res = from p in listPeople
                      where p.FirstName.ToLower().Contains(firstNameFilter.ToLower()) 
                      && p.LastName.ToLower().Contains(lastNameFilter.ToLower())
                      && p.Email.ToLower().Contains(emailFilter.ToLower())
                      && DateTime.Compare(p.Birthday, dateFromFilter) >= 0
                      && DateTime.Compare(p.Birthday, dateToFilter) <= 0
                      select p;
            return res.ToList();
        }

        internal static List<Person> ToFilterBySunSign(List<Person> listPeople, string sunSignFilter)
        {
            var res = from p in listPeople
                where p.SunSign.ToLower().Equals(sunSignFilter.ToLower())
                      select p;
            return res.ToList();
        }

        internal static List<Person> ToFilterByChinese(List<Person> listPeople, string chineseSignFilter)
        {
            var res = from p in listPeople
                where p.ChineseSing.ToLower().Equals(chineseSignFilter.ToLower())
                      select p;
            return res.ToList();
        }

        internal static List<Person> ToFilterByIsAdult(List<Person> listPeople, bool isAdultFilter)
        {
            var res = from p in listPeople
                      where p.IsAdult == isAdultFilter
                select p;
            return res.ToList();
        }

        internal static List<Person> ToFilterByIsBirthday(List<Person> listPeople, bool isBirthdayFilter)
        {
            var res = from p in listPeople
                where p.IsBirthday == isBirthdayFilter
                select p;
            return res.ToList();
        }
    }
}