using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3.Class
{
    internal class SportGroup
    {
        public string GroupName { get; set; }      
        public string Sport { get; set; }          
        public int MaxMembers { get; set; }

        // Група знає про свого куратора/тренера і звертається до нього,
        // але НЕ володіє ним (тренер існує абсолютно автономно).

        public Coach CuratorCoach { get; set; }
        public SportGroup()
        {
            GroupName = " ";
            Sport = " ";
            MaxMembers = 20;
            CuratorCoach = null;
        }
        public SportGroup(string groupName, string sport, int maxMembers, Coach curatorCoach = null)
        {
            GroupName = groupName;
            Sport = sport;
            MaxMembers = maxMembers;
            CuratorCoach = curatorCoach;
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
            // 1. Оголошуємо змінну для збереження ПІБ тренера
            string coachName;
            // 2. Перевіряємо: чи прив'язаний об'єкт тренера (чи не дорівнює він null)
            if (CuratorCoach != null)
            {
                // Якщо тренер призначений, беремо його прізвище та ім'я
                coachName = CuratorCoach.Surname + " " + CuratorCoach.Name;
            }
            else
            {
                // Якщо тренера немає (посилання порожнє)
                coachName = "Не призначено";
            }
            // 3. Формуємо повний результуючий текст
            string result = "Назва групи: " + GroupName + "\n" +
                            "Вид спорту: " + Sport + "\n" +
                            "Ліміт учасників: " + MaxMembers + " осіб\n" +
                            "Тренер-куратор: " + coachName;
            // 4. Повертаємо готовий текст
            return result;
        }


    }
}
