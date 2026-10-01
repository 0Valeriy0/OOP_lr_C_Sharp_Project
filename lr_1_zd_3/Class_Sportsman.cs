using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3
{
    public class Sportsman
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pobatkovi { get; set; }
        public string Sport { get; set; }
        public string SportRank { get; set; }
        public DateTime BirthDate { get; set; }

        public int PrizePlaces { get; set; }


        // Конструктор без параметрів
        public Sportsman()
        {
        }
        // Конструктор з параметрами
        public Sportsman(string name, string surname, string pobatkovi, string sport, string sportRank, DateTime birthDate, int prizePlaces)
        {
            Name = name;
            Surname = surname;
            Pobatkovi = pobatkovi;
            Sport = sport;
            SportRank = sportRank;
            BirthDate = birthDate;
            PrizePlaces = prizePlaces;
        }
        public static Sportsman operator ++(Sportsman sportsman)
        {
            sportsman.PrizePlaces++;
            return sportsman;
        }
        public static Sportsman operator --(Sportsman sportsman) {
            sportsman.PrizePlaces--;
            return sportsman;

        }
        public static bool operator > (Sportsman sportsman1, Sportsman sportsman2)
        {
            return string.Compare(sportsman1.Name, sportsman2.Name) > 0;
        }
        public static bool operator <(Sportsman sportsman1, Sportsman sportsman2)
        {
            return string.Compare(sportsman1.Name, sportsman2.Name) < 0;
        }
    }
}
