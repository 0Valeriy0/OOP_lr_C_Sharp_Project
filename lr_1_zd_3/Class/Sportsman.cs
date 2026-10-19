using lr_1_zd_3.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace lr_1_zd_3.Class
{
    public class Sportsman : Person, ITrainingBooking
    {
        public string Sport { get; set; }
        public string SportRank { get; set; }
        public int PrizePlaces { get; set; }
        
        public MedicalCard MedicalCard { get; set; }

        // Конструктор без параметрів
        public Sportsman()
        {
            MedicalCard = new MedicalCard();
        }
        // Конструктор з параметрами
        public Sportsman(string name, string surname, string pobatkovi, string sport, string sportRank, DateTime birthDate, int prizePlaces) :base(name,surname,pobatkovi,birthDate, sport)
        {
            SportRank = sportRank;
            PrizePlaces = prizePlaces;
            MedicalCard = new MedicalCard();
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





















        private Coach[] coaches = new Coach[100];
        private DateTime[] trainingDates = new DateTime[100];
        private int bookingsCount = 0;

        public bool SignUpForTraining(Coach coach, DateTime date)
        {
            if (coach == null)
            {
                return false;
            }

            // Перевіряємо, чи збігаються види спорту
            if (Sport != coach.Sport)
            {
                return false;
            }

            // Перевіряємо дату
            if (date.Date < DateTime.Today)
            {
                return false;
            }

            // Перевіряємо повторний запис
            for (int i = 0; i < bookingsCount; i++)
            {
                if (coaches[i] == coach &&
                    trainingDates[i].Date == date.Date)
                {
                    return false;
                }
            }

            // Перевіряємо вільне місце в масиві
            if (bookingsCount >= coaches.Length)
            {
                return false;
            }

            // Зберігаємо запис
            coaches[bookingsCount] = coach;
            trainingDates[bookingsCount] = date;
            bookingsCount++;

            return true;
        }
        public override void ShowInfo()
        {
            string message = $"Спортсмен: {Surname} {Name} {Pobatkovi}\n" +
                             $"Дата народження: {BirthDate:dd.MM.yyyy}\n" +
                             $"Вік: {CalculateAge()} р.\n" +
                             $"Вид спорту: {Sport}\n" +
                             $"Спортивний розряд: {SportRank}\n" +
                             $"Кількість призових місць: {PrizePlaces}\n" +
                             $"--- Медична картка ---\n" +
                   $"Група крові: {MedicalCard.BloodGroup}\n" +
                   $"Допуск до змагань: {(MedicalCard.HasAccessToCompete ? "Допущено" : "Не допущено")}\n" +
                   $"Останній медогляд: {MedicalCard.InspectationDate:dd.MM.yyyy}";
            MessageBox.Show(message, "Картка спортсмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }




    }
}
