using lr_1_zd_3.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3.Class
{
    public class Person: IPerson, ISortableBySurname
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
        public void SortBySurname(Person[] people, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (string.Compare(people[j].Surname,  people[j + 1].Surname) > 0)
                    {
                        Person temp = people[j];
                        people[j] = people[j + 1];
                        people[j + 1] = temp;
                    }
                }
            }
        }
        public int CalculateAge()
        {
            int age = DateTime.Today.Year - BirthDate.Year;

            if (DateTime.Today < BirthDate.AddYears(age))
            {
                age--;
            }
            return age;
        }
        public virtual void ShowInfo()
        {
            string message = $"ПІБ: {Surname} {Name} {Pobatkovi}\n" +
                             $"Дата народження: {BirthDate:dd.MM.yyyy}\n" +
                             $"Повний вік: {CalculateAge()} р.\n" +
                             $"Вид спорту: {Sport}";
            MessageBox.Show(message, "Інформація про особу", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
