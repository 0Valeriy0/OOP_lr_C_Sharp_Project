using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3
{
    public class Coach : Person
    {
        public string Sport { get; set; }
        public string CoachRank { get; set; }

        // Конструктор без параметрів
        public Coach()
        {
        }

        // Конструктор з параметрами
        public Coach( string name, string surname, string pobatkovi,  DateTime birthDate,string sport, string coachRank)  : base(name, surname, pobatkovi, birthDate, sport)
        {
            CoachRank = coachRank;
        }
    }
}
