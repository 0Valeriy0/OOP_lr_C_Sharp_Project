using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3
{
    public class Sportsman : Person
    {
        public string Sport { get; set; }
        public string SportRank { get; set; }
        public int PrizePlaces { get; set; }


        // Конструктор без параметрів
        public Sportsman()
        {
        }
        // Конструктор з параметрами
        public Sportsman(string name, string surname, string pobatkovi, string sport, string sportRank, DateTime birthDate, int prizePlaces) :base(name,surname,pobatkovi,birthDate, sport)
        {
            SportRank = sportRank;
            PrizePlaces = prizePlaces;
        }
        public static Sportsman operator ++(Sportsman sportsman)
        {
            sportsman.PrizePlaces++;
            return sportsman;
        }
        public static Sportsman operator --(Sportsman sportsman) {
            if (sportsman.PrizePlaces > 0)
            {
                sportsman.PrizePlaces--;
            }
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
