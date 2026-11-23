using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1.Class
{
    public class Coach : Person
    {
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
        public override string ShowInfo()
        {
            string message = $"Тренер: {Surname} {Name} {Pobatkovi}\n" +
                             $"Дата народження: {BirthDate:dd.MM.yyyy}\n" +
                             $"Вік: {CalculateAge()} р.\n" +
                             $"Вид спорту: {Sport}\n" +
                             $"Звання тренера: {CoachRank}";
            return message;
        }
    }
}
