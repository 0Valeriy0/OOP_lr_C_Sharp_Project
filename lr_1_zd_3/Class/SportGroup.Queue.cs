using System;
using System.Collections.Generic;
using ClassLibrary1.Class;

namespace lr_1_zd_3.Class
{
    // Частковий клас: друга частина SportGroup — черга очікування
    internal partial class SportGroup
    {
        // Queue<T>: спортсмени, які чекають на вільне місце
        public Queue<Sportsman> WaitingList { get; set; } = new Queue<Sportsman>();


        public bool AddToWaitingList(Sportsman sportsman)
        {
            if (sportsman == null || HasSportsman(sportsman)) return false;

            WaitingList.Enqueue(sportsman);
            CreateMembersChangedEvent($"До списку очікування групи '{GroupName}' додано: {sportsman.Surname} {sportsman.Name}");
            return true;
        }

        // Чи є вже такий спортсмен 
        private bool HasSportsman(Sportsman sportsman)
        {
            foreach (Sportsman member in Members)
                if (IsSamePerson(member, sportsman)) return true;

            foreach (Sportsman waiting in WaitingList)
                if (IsSamePerson(waiting, sportsman)) return true;

            return false;
        }

        private static bool IsSamePerson(Sportsman a, Sportsman b)
        {
            return string.Equals(a.Surname, b.Surname, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
        }
        // ... сюди вставити (Ctrl+V) AddToWaitingList, HasSportsman, IsSamePerson
    }
}