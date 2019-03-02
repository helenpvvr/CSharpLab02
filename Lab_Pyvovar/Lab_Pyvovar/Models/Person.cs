using System;

namespace Lab_Pyvovar.Models
{
    internal class Person
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthday;

        private readonly bool _isAdult;
        private readonly string _sunSign;
        private readonly string _chineseSign;
        private readonly bool _isBirthday;

        public Person(string firstName, string lastName, string email, DateTime birthday)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _birthday = birthday;

            _isAdult = Birthday.Subtract(DateTime.Today.AddYears(-18)).Days <= 0;
            _sunSign = CalculateSunSing();
            _chineseSign = Enum.GetName(typeof(ChineseSigns), Birthday.Year % 12);
            _isBirthday = (Birthday.Day == DateTime.Now.Day && Birthday.Month == DateTime.Now.Month);
        }

        public Person(string firstName, string lastName, string email) 
            : this(firstName, lastName, email, DateTime.Today)
        {
        }

        public Person(string firstName, string lastName, DateTime birthday)
            : this(firstName, lastName, "", birthday)
        {
        }

        internal string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        internal string LastName {
            get { return _lastName; }
            set { _lastName = value; }
        }

        internal string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        internal DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        internal bool IsAdult
        {
            get { return _isAdult; }
        }

        internal string SunSign
        {
            get { return _sunSign; }
        }

        internal string ChineseSing
        {
            get { return _chineseSign; }
        }

        internal bool IsBirthday
        {
            get { return _isBirthday; }
        }

        private string CalculateSunSing()
        {
            switch (Birthday.Month)
            {
                case 1:
                    if (Birthday.Day <= 19)
                        return "Capricorn";
                    return "Aquarius";
                case 2:
                    if (Birthday.Day <= 19)
                        return "Aquarius";
                    return "Pisces";
                case 3:
                    if (Birthday.Day <= 21)
                        return "Pisces";
                    return "Aries";
                case 4:
                    if (Birthday.Day <= 20)
                        return "Aries";
                    return "Taurus";
                case 5:
                    if (Birthday.Day <= 21)
                        return "Taurus";
                    return "Gemini";
                case 6:
                    if (Birthday.Day <= 21)
                        return "Gemini";
                    return "Cancer";
                case 7:
                    if (Birthday.Day <= 22)
                        return "Cancer";
                    return "Leo";
                case 8:
                    if (Birthday.Day <= 21)
                        return "Leo";
                    return "Virgo";
                case 9:
                    if (Birthday.Day <= 23)
                        return "Virgo";
                    return "Libra";
                case 10:
                    if (Birthday.Day <= 23)
                        return "Libra";
                    return "Scorpio";
                case 11:
                    if (Birthday.Day <= 22)
                        return "Scorpio";
                    return "Sagittarius";
                default:
                    if (Birthday.Day <= 22)
                        return "Sagittarius";
                    return "Capricorn";
            }
        }
    }
}
