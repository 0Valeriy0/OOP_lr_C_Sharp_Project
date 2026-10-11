using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3
{
    public class Person
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


    }
}
