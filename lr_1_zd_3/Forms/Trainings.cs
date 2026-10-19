using lr_1_zd_3.Class;
using lr_1_zd_3.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace lr_1_zd_3
{
    public partial class Trainings : Form
    {
        private Sportsman[] sportsmen = new Sportsman[100];
        private int sportsmenCount = 0;

        private Coach[] coaches = new Coach[100];
        private int coachesCount = 0;

        private Sportsman[] bookedSportsmen = new Sportsman[100];
        private Coach[] bookedCoaches = new Coach[100];
        private DateTime[] bookingDates = new DateTime[100];
        private int[] bookingDurations = new int[100];
        private int bookingsCount = 0;

        public Trainings()
        {
            InitializeComponent();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Trainings_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd.MM.yyyy HH:mm";
            dateTimePicker1.Value = DateTime.Now.AddHours(1);

            comboBox1_sportSelect.Items.Clear();
            comboBox1_sportSelect.Items.Add("Веслування");
            comboBox1_sportSelect.Items.Add("Велоспорт");
            comboBox1_sportSelect.SelectedIndex = 0;

            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
            UpdateCoachesComboBox();
        }
        private void CreateSportsmenGrid()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Index", "№");
            dataGridView1.Columns.Add("Surname", "Прізвище");
            dataGridView1.Columns.Add("Name", "Ім'я");
            dataGridView1.Columns.Add("Pobatkovi", "По батькові");
            dataGridView1.Columns.Add("Sport", "Вид спорту");
            dataGridView1.Columns.Add("Rank", "Розряд");
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void CreateTrainingsGrid()
        {
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("Index", "N");
            dataGridView2.Columns.Add("Date", "Дата та час");
            dataGridView2.Columns.Add("Sport", "Вид спорту");
            dataGridView2.Columns.Add("Coach", "Тренер");
            dataGridView2.Columns.Add("Sportsman", "Спортсмен");
            dataGridView2.Columns.Add("Duration", "Тривалість (хв)");
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Оберіть спортсмена з таблиці!");
                return;
            }
            if (comboBox1_trainerPicker.SelectedIndex < 0)
            {
                MessageBox.Show("Оберіть тренера зі списку!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DateTime selectedDate = dateTimePicker1.Value;
            if (selectedDate <= DateTime.Now)
            {
                MessageBox.Show("Дата та час тренування не можуть бути в минулому!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedRow = dataGridView1.CurrentRow.Index;
            string sportsmanSurname = dataGridView1.Rows[selectedRow].Cells["Surname"].Value.ToString();
            string sportsmanName = dataGridView1.Rows[selectedRow].Cells["Name"].Value?.ToString();

            Sportsman currentSportsman = null;
            for (int i = 0; i < sportsmenCount; i++)
            {
                if (sportsmen[i].Surname == sportsmanSurname && sportsmen[i].Name == sportsmanName)
                {
                    currentSportsman = sportsmen[i];
                    break;
                }
            }
            // Знаходимо обраного тренера
            string selectedSport = comboBox1_sportSelect.Text;
            Coach currentCoach = null;
            int coachIndexInCombo = comboBox1_trainerPicker.SelectedIndex;
            int matchIndex = 0;
            for (int i = 0; i < coachesCount; i++)
            {
                if (string.Equals(coaches[i].Sport, selectedSport, StringComparison.OrdinalIgnoreCase))
                {
                    if (matchIndex == coachIndexInCombo)
                    {
                        currentCoach = coaches[i];
                        break;
                    }
                    matchIndex++;
                }
            }

            if (currentSportsman == null || currentCoach == null)
            {
                MessageBox.Show("Не вдалося визначити спортсмена або тренера!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ITrainingBooking bookingService = currentSportsman;

            if (!bookingService.SignUpForTraining(currentCoach, selectedDate))
            {
                MessageBox.Show(
                    "Неможливо записати спортсмена!\n" +
                    "Перевірте: чи збігається вид спорту тренера і спортсмена, чи дата не в минулому, та чи немає повторного запису.",
                    "Помилка запису",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            /*            for (int i = 0; i < bookingsCount; i++)
                        {
                            if (bookedSportsmen[i] == currentSportsman && bookingDates[i] == selectedDate)
                            {
                                MessageBox.Show("Цей спортсмен вже має тренування на цей час!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }*/
            // Зберігаємо дані про нове тренування
            int duration = (int)numericUpDown1.Value;
            bookedSportsmen[bookingsCount] = currentSportsman;
            bookedCoaches[bookingsCount] = currentCoach;
            bookingDates[bookingsCount] = selectedDate;
            bookingDurations[bookingsCount] = duration;
            bookingsCount++;

            // Оновлюємо нижню таблицю тренувань
            RefreshTrainingsGrid();
            MessageBox.Show(
                $"Тренування успішно створено!\n" +
                $"Спортсмен: {currentSportsman.Surname} {currentSportsman.Name}\n" +
                $"Тренер: {currentCoach.Surname} {currentCoach.Name}\n" +
                $"Дата: {selectedDate:dd.MM.yyyy HH:mm}\n" +
                $"Тривалість: {duration} хв.",
                "Успіх",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void RefreshTrainingsGrid()
        {
            if (bookingsCount == 0)
            {
                dataGridView2.Columns.Clear();
                return;
            }
            // Створюємо колонки, якщо їх ще немає
            if (dataGridView2.Columns.Count == 0)
            {
                CreateTrainingsGrid();
            }

            dataGridView2.Rows.Clear();
            for (int i = 0; i < bookingsCount; i++)
            {
                dataGridView2.Rows.Add(
                    i,
                    bookingDates[i].ToString("dd.MM.yyyy HH:mm"),
                    bookedSportsmen[i].Sport,
                    $"{bookedCoaches[i].Surname} {bookedCoaches[i].Name}",
                    $"{bookedSportsmen[i].Surname} {bookedSportsmen[i].Name}",
                    bookingDurations[i]
                );
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Оберіть тренування з нижньої таблиці для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView2.CurrentRow.Index;
            if (index >= 0 && index < bookingsCount)
            {
                DialogResult res = MessageBox.Show("Ви дійсно хочете скасувати це тренування?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    for (int i = index; i < bookingsCount - 1; i++)
                    {
                        bookedSportsmen[i] = bookedSportsmen[i + 1];
                        bookedCoaches[i] = bookedCoaches[i + 1];
                        bookingDates[i] = bookingDates[i + 1];
                        bookingDurations[i] = bookingDurations[i + 1];
                    }
                    bookedSportsmen[bookingsCount - 1] = null;
                    bookedCoaches[bookingsCount - 1] = null;
                    bookingsCount--;
                    RefreshTrainingsGrid();
                    MessageBox.Show("Тренування успішно скасовано!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void записУФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bookingsCount == 0)
            {
                MessageBox.Show("Немає створених тренувань для запису!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            saveDialog.Title = "Зберегти тренування";
            saveDialog.FileName = "trainings.txt";
            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;
            using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
            {
                for (int i = 0; i < bookingsCount; i++)
                {
                    writer.WriteLine(
                        $"{bookingDates[i]:dd.MM.yyyy HH:mm};" +
                        $"{bookingDurations[i]};" +
                        $"{bookedSportsmen[i].Surname};" +
                        $"{bookedSportsmen[i].Name};" +
                        $"{bookedSportsmen[i].Sport};" +
                        $"{bookedCoaches[i].Surname};" +
                        $"{bookedCoaches[i].Name}"
                    );
                }
            }
            MessageBox.Show("Дані тренувань успішно збережено у файл!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void спортсмениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void тренериToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2_Trainers form = new Form2_Trainers();
            form.Show();
            this.Hide();
        }

        private void очиститиФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openDialog.Title = "Виберіть файл тренувань для видалення";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            DialogResult result = MessageBox.Show(
                "Ви дійсно хочете видалити цей файл?",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // 1. Видаляємо сам файл з диска
                File.Delete(openDialog.FileName);
                // 2. Очищаємо дані на формі (масиви та таблицю тренувань)
                for (int i = 0; i < bookingsCount; i++)
                {
                    bookedSportsmen[i] = null;
                    bookedCoaches[i] = null;
                }
                bookingsCount = 0;
                RefreshTrainingsGrid();
                MessageBox.Show("Файл успішно видалено, а таблицю тренувань очищено!", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_sportSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCoachesComboBox();
            RefreshSportsmenGrid();
        }
        private void UpdateCoachesComboBox()
        {
            // Якщо у вас ComboBox тренера називається інакше (наприклад comboBox_Coach), замініть назву
            comboBox1_trainerPicker.Items.Clear();
            string selectedSport = comboBox1_sportSelect.Text;
            for (int i = 0; i < coachesCount; i++)
            {
                if (string.Equals(coaches[i].Sport, selectedSport, StringComparison.OrdinalIgnoreCase))
                {
                    comboBox1_trainerPicker.Items.Add($"{coaches[i].Surname} {coaches[i].Name}");
                }
            }
            if (comboBox1_trainerPicker.Items.Count > 0)
                comboBox1_trainerPicker.SelectedIndex = 0;
        }
        // --- Оновлення таблиці спортсменів під обраний спорт ---
        private void RefreshSportsmenGrid()
        {
            if (sportsmenCount == 0)
            {
                dataGridView1.Columns.Clear();
                return;
            }
            // Створюємо колонки, якщо їх ще немає
            if (dataGridView1.Columns.Count == 0)
            {
                CreateSportsmenGrid();
            }
            dataGridView1.Rows.Clear();
            string selectedSport = comboBox1_sportSelect.Text;
            for (int i = 0; i < sportsmenCount; i++)
            {
                if (string.Equals(sportsmen[i].Sport, selectedSport, StringComparison.OrdinalIgnoreCase))
                {
                    dataGridView1.Rows.Add(
                        i,
                        sportsmen[i].Surname,
                        sportsmen[i].Name,
                        sportsmen[i].Pobatkovi,
                        sportsmen[i].Sport,
                        sportsmen[i].SportRank
                    );
                }
            }
        }

        private void даніПроТренерівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openDialog.Title = "Відкрити файл з тренерами";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            coachesCount = 0;
            string[] lines = File.ReadAllLines(openDialog.FileName);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(';');
                // У файлі тренерів 6 полів: Прізвище;Ім'я;По батькові;Спорт;Звання;Дата
                if (parts.Length == 6 && coachesCount < coaches.Length)
                {
                    Coach coach = new Coach();
                    coach.Surname = parts[0];
                    coach.Name = parts[1];
                    coach.Pobatkovi = parts[2];
                    coach.Sport = parts[3];
                    coach.CoachRank = parts[4];
                    coach.BirthDate = DateTime.Parse(parts[5]);
                    coaches[coachesCount] = coach;
                    coachesCount++;
                }
            }
            // Оновлюємо випадаючий список тренерів
            UpdateCoachesComboBox();
        }

        private void даніПроСпортсменівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openDialog.Title = "Відкрити файл зі спортсменами";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            sportsmenCount = 0;
            string[] lines = File.ReadAllLines(openDialog.FileName);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(';');
                // У файлі спортсменів 7 полів: Прізвище;Ім'я;По батькові;Спорт;Розряд;Дата;Призові
                if (parts.Length == 7 && sportsmenCount < sportsmen.Length)
                {
                    Sportsman sportsman = new Sportsman();
                    sportsman.Surname = parts[0];
                    sportsman.Name = parts[1];
                    sportsman.Pobatkovi = parts[2];
                    sportsman.Sport = parts[3];
                    sportsman.SportRank = parts[4];
                    sportsman.BirthDate = DateTime.Parse(parts[5]);
                    sportsman.PrizePlaces = int.Parse(parts[6]);
                    sportsmen[sportsmenCount] = sportsman;
                    sportsmenCount++;
                }
            }
            // Оновлюємо верхню таблицю зі спортсменами
            RefreshSportsmenGrid();
        }

        private void даніПроТренуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openDialog.Title = "Відкрити файл з тренуваннями";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            bookingsCount = 0;
            string[] lines = File.ReadAllLines(openDialog.FileName);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(';');
                // У файлі тренувань 7 полів: Дата;Тривалість;ПрізвищеСпортсмена;Ім'яСпортсмена;Спорт;ПрізвищеТренера;Ім'яТренера
                if (parts.Length == 7 && bookingsCount < bookedSportsmen.Length)
                {
                    DateTime date = DateTime.Parse(parts[0]);
                    int duration = int.Parse(parts[1]);
                    Sportsman sportsman = new Sportsman();
                    sportsman.Surname = parts[2];
                    sportsman.Name = parts[3];
                    sportsman.Sport = parts[4];
                    Coach coach = new Coach();
                    coach.Surname = parts[5];
                    coach.Name = parts[6];
                    coach.Sport = parts[4];
                    bookedSportsmen[bookingsCount] = sportsman;
                    bookedCoaches[bookingsCount] = coach;
                    bookingDates[bookingsCount] = date;
                    bookingDurations[bookingsCount] = duration;
                    bookingsCount++;
                }
            }
            // Оновлюємо нижню таблицю тренувань
            RefreshTrainingsGrid();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openDialog.Title = "Виберіть файл тренувань для видалення";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            DialogResult result = MessageBox.Show(
                "Ви дійсно хочете видалити цей файл?",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // 1. Видаляємо сам файл з диска
                File.Delete(openDialog.FileName);
                // 2. Очищаємо дані на формі (масиви та таблицю тренувань)
                for (int i = 0; i < bookingsCount; i++)
                {
                    bookedSportsmen[i] = null;
                    bookedCoaches[i] = null;
                }
                bookingsCount = 0;
                RefreshTrainingsGrid();
                MessageBox.Show("Файл успішно видалено, а таблицю тренувань очищено!", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

}
