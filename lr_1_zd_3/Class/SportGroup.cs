using System;
using System.Text;
using System.Collections.Generic;

namespace lr_1_zd_3.Class
{
    // 1. Делегат задає сигнатуру обробників
    internal delegate void MembersChangedDelegate(SportGroup group, string message);
    internal class SportGroup
    {
        public string GroupName { get; set; }      
        public string Sport { get; set; }          
        public int MaxMembers { get; set; }

        // Асоціація: група знає про свого куратора/тренера і звертається до нього,
        // але НЕ володіє ним (тренер існує автономно).
        public Coach? CuratorCoach { get; set; }

        // Склад групи: спортсмени (агрегація / колекція членів групи)
        //  private Sportsman[] members = new Sportsman[50];
        // List<T>: потрібен доступ за індексом і видалення будь-якого спортсмена
        public List<Sportsman> Members { get; set; } = new List<Sportsman>();

        // Queue<T>: спортсмени, які чекають на вільне місце 
        public Queue<Sportsman> WaitingList { get; set; } = new Queue<Sportsman>();

        //private int membersCount = 0;

        // public Sportsman[] Members => members;
        public int MembersCount => Members.Count;
        public bool IsFull => MembersCount >= MaxMembers;
        // 2. Події — поля класу-джерела
        public event MembersChangedDelegate MembersChanged;
        public event EventHandler GroupFilled;
        // 3. Метод, який створює подію
        private void CreateMembersChangedEvent(string message)
        {
            MembersChanged?.Invoke(this, message);
        }

        public SportGroup()
        {
            GroupName = " ";
            Sport = " ";
            MaxMembers = 20;
            CuratorCoach = null;
           // members = new Sportsman[50];
        }

        public SportGroup(string groupName, string sport, int maxMembers, Coach? curatorCoach = null)
        {
            GroupName = groupName;
            Sport = sport;
            MaxMembers = maxMembers;
            CuratorCoach = curatorCoach;
          //  members = new Sportsman[50];

        }

        public bool AddSportsman(Sportsman sportsman)
        {
            if (sportsman == null || IsFull || HasSportsman(sportsman)) return false;

            Members.Add(sportsman);
            CreateMembersChangedEvent($"До групи '{GroupName}' додано: {sportsman.Surname} {sportsman.Name}");
            if (IsFull) GroupFilled?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool RemoveSportsman(int index)
        {
            if (index < 0 || index >= Members.Count) return false;

           Sportsman removed = Members[index];
           Members.RemoveAt(index);
            string message = $"З групи '{GroupName}' вилучено: {removed.Surname} {removed.Name}";

            if (WaitingList.Count > 0)
            {
                Sportsman next = WaitingList.Dequeue();
                Members.Add(next);
                message += $". Із черги додано: {next.Surname} {next.Name}";
            }

            CreateMembersChangedEvent(message);
           return true;
        }

        public void AssignCoach(Coach newCoach)
        {
            if (newCoach != null && string.Equals(newCoach.Sport, this.Sport, StringComparison.OrdinalIgnoreCase))
            {
                CuratorCoach = newCoach;
            }
        }

        public string GetInfo()
        {
            string coachName = CuratorCoach != null  ? CuratorCoach.Surname + " " + CuratorCoach.Name: "Не призначено";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Назва групи: " + GroupName);
            sb.AppendLine("Вид спорту: " + Sport);
            sb.AppendLine($"Кількість учасників: {Members.Count} з {MaxMembers} осіб");
            sb.AppendLine("Тренер-куратор: " + coachName);
            sb.AppendLine();
            sb.AppendLine("--- Склад групи (спортсмени) ---");
                
            if (Members.Count == 0)
            {
                sb.AppendLine("У групі наразі немає спортсменів.");
            }
            else
            {
                for (int i = 0; i < Members.Count; i++)
                {
                    if (Members[i] != null)
                    {
                        string rank = string.IsNullOrWhiteSpace(Members[i].SportRank) ? "Без розряду" : Members[i].SportRank;
                        sb.AppendLine($"{i}. {Members[i].Surname} {Members[i].Name} {Members[i].Pobatkovi} ({rank})");
                    }
                }
            }
            sb.AppendLine();
            sb.AppendLine($"--- Черга очікування({WaitingList.Count}) ---");
            int position = 1;
            foreach (var waitingSportsman in WaitingList)
            {
                sb.AppendLine($"{position}. {waitingSportsman.Surname} {waitingSportsman.Name} {waitingSportsman.Pobatkovi}");
                position++;
            }
            return sb.ToString();
        }

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


    }
}
