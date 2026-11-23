using lr_1_zd_3.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Class
{
    public abstract class Person: IPerson, ISortableBySurname
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pobatkovi { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sport { get; set; }

        public Person()
        { }
        public Person( string name,  string surname,string pobatkovi,   DateTime birthDate, string sport )
        {
            Name = name;
            Surname = surname;
            Pobatkovi = pobatkovi;
            BirthDate = birthDate;
            Sport = sport;


        }
        public void SortBySurname<T>(List<T> people) where T : Person
        {
            people.Sort((x, y) => String.Compare(x.Surname, y.Surname));
        
        }
        public int CalculateAge()
        {
            return BirthDate.Age();
        }
        public virtual string ShowInfo()
        {
            string message = $"ПІБ: {Surname} {Name} {Pobatkovi}\n" +
                             $"Дата народження: {BirthDate:dd.MM.yyyy}\n" +
                             $"Повний вік: {CalculateAge()} р.\n" +
                             $"Вид спорту: {Sport}";
            return message;
        }

    }
}
