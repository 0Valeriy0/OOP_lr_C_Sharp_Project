using System;
using System.Text;

namespace lr_1_zd_3.Class
{
    internal class SportGroup
    {
        public string GroupName { get; set; }      
        public string Sport { get; set; }          
        public int MaxMembers { get; set; }

        // Асоціація: група знає про свого куратора/тренера і звертається до нього,
        // але НЕ володіє ним (тренер існує автономно).
        public Coach? CuratorCoach { get; set; }

        // Склад групи: спортсмени (агрегація / колекція членів групи)
        private Sportsman[] members = new Sportsman[50];
        private int membersCount = 0;

        public Sportsman[] Members => members;
        public int MembersCount => membersCount;

        public SportGroup()
        {
            GroupName = " ";
            Sport = " ";
            MaxMembers = 20;
            CuratorCoach = null;
            members = new Sportsman[50];
            membersCount = 0;
        }

        public SportGroup(string groupName, string sport, int maxMembers, Coach? curatorCoach = null)
        {
            GroupName = groupName;
            Sport = sport;
            MaxMembers = maxMembers;
            CuratorCoach = curatorCoach;
            members = new Sportsman[50];
            membersCount = 0;
        }

        public bool AddSportsman(Sportsman sportsman)
        {
            if (sportsman == null) return false;
            if (membersCount >= MaxMembers || membersCount >= members.Length) return false;

            // Перевірка на дублювання за ПІБ
            for (int i = 0; i < membersCount; i++)
            {
                if (members[i] != null &&
                    string.Equals(members[i].Surname, sportsman.Surname, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(members[i].Name, sportsman.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            members[membersCount] = sportsman;
            membersCount++;
            return true;
        }

        public bool RemoveSportsman(int index)
        {
            if (index < 0 || index >= membersCount) return false;

            for (int i = index; i < membersCount - 1; i++)
            {
                members[i] = members[i + 1];
            }
            members[membersCount - 1] = null!;
            membersCount--;
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
            string coachName = CuratorCoach != null
                ? CuratorCoach.Surname + " " + CuratorCoach.Name
                : "Не призначено";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Назва групи: " + GroupName);
            sb.AppendLine("Вид спорту: " + Sport);
            sb.AppendLine($"Кількість учасників: {membersCount} з {MaxMembers} осіб");
            sb.AppendLine("Тренер-куратор: " + coachName);
            sb.AppendLine();
            sb.AppendLine("--- Склад групи (спортсмени) ---");

            if (membersCount == 0)
            {
                sb.AppendLine("У групі наразі немає спортсменів.");
            }
            else
            {
                for (int i = 0; i < membersCount; i++)
                {
                    if (members[i] != null)
                    {
                        string rank = string.IsNullOrWhiteSpace(members[i].SportRank) ? "Без розряду" : members[i].SportRank;
                        sb.AppendLine($"{i}. {members[i].Surname} {members[i].Name} {members[i].Pobatkovi} ({rank})");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
