using lr_1_zd_3.Interface;
using lr_1_zd_3.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Serialization;
using ClassLibrary1.Class;
using lr_1_zd_3.Aditionals;

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

        // Серіалізатор XML для читання тренерів (той самий формат, що зберігає форма «Тренери»)
        private static readonly XmlSerializer coachesSerializer = new XmlSerializer(typeof(List<Coach>));

        delegate void TrainingProcessDelegate(Sportsman sportsman);
        //  Метод 1 перевірка 
        private void CheckMedicalCard(Sportsman sportsman) {
            if (sportsman.MedicalCard == null || !sportsman.MedicalCard.HasAccessToCompete) { 
            throw new Exception($"Спортсмен {sportsman.Surname} {sportsman.Name} не має медичної довідки для участі у тренуваннях.");

            }
        
        }
        //Метод 2 оновлення статусу
        private void UpdateBookingStatus(Sportsman sportsman)
        {
           toolStripStatusLabel1.Text = $"Спортсмен {sportsman.Surname} {sportsman.Name} успішно записаний на тренування.";
        }



        public Trainings()
        {
            InitializeComponent();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Trainings_Load(object sender, EventArgs e)
        {
            dataGridView1.ApplyLargeFont();
            dataGridView2.ApplyLargeFont();
            dataGridViewSchedule.ApplyLargeFont();
            // Таблиці спортсменів і тренувань на вкладці «Запис» завжди однакової висоти
            tabPage1.Resize += (s, ev) => GridStyle.SplitEvenly(dataGridView1, label1, dataGridView2);
            GridStyle.SplitEvenly(dataGridView1, label1, dataGridView2);
            // Рядки розкладу розтягуються на всю висоту таблиці
            dataGridViewSchedule.Resize += (s, ev) => FitScheduleRows();

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
            RefreshScheduleGrid();
            toolStripStatusLabel1.Text = "Готово до роботи";

            // Автоматичне завантаження баз з папки Data
            if (File.Exists(DataFiles.Sportsmen)) LoadSportsmen(DataFiles.Sportsmen);
            if (File.Exists(DataFiles.Coaches)) LoadCoaches(DataFiles.Coaches);
            if (File.Exists(DataFiles.Trainings)) LoadTrainings(DataFiles.Trainings);
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
            dataGridView1.Columns.Add("Access", "Допуск");
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
            string sportsmanSurname = dataGridView1.Rows[selectedRow].Cells["Surname"].Value?.ToString() ?? "";
            string sportsmanName = dataGridView1.Rows[selectedRow].Cells["Name"].Value?.ToString() ?? "";

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
            for (int i = 0; i < bookingsCount; i++)
            {
                if (bookedSportsmen[i].Surname == currentSportsman.Surname &&
                    bookedSportsmen[i].Name == currentSportsman.Name &&
                    bookedCoaches[i].Surname == currentCoach.Surname &&
                    bookedCoaches[i].Name == currentCoach.Name &&
                    bookingDates[i].Date == selectedDate.Date)
                {
                    MessageBox.Show("Цей спортсмен уже записаний до цього тренера на цей день!", "Повторний запис", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (!bookingService.SignUpForTraining(currentCoach, selectedDate))
            {
                MessageBox.Show(
                    "Неможливо записати спортсмена!\n" +
                    "Перевірте: чи збігається вид спорту тренера і спортсмена та чи дата не в минулому.",
                    "Помилка запису",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Делегат для роботи із кількома методами.
            // Спершу перевіряємо медичну картку: якщо допуску немає, тренування НЕ додається
            TrainingProcessDelegate del = new TrainingProcessDelegate(CheckMedicalCard);
            del += new TrainingProcessDelegate(UpdateBookingStatus);

            foreach (TrainingProcessDelegate d in del.GetInvocationList())
            {
                try
                {
                    d(currentSportsman);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Медичне застереження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    toolStripStatusLabel1.Text = "Тренування не створено: немає допуску за медичною карткою";
                    return;
                }

            }

            // Перевірку пройдено — зберігаємо дані про нове тренування
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
            // Розклад на тиждень показує ті самі тренування, тому оновлюємо його разом зі списком
            RefreshScheduleGrid();

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
            // Показуємо лише тренування обраного виду спорту (розклад на тиждень не фільтрується)
            string selectedSport = comboBox1_sportSelect.Text;
            for (int i = 0; i < bookingsCount; i++)
            {
                if (bookedSportsmen[i].Sport != selectedSport)
                    continue;

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
            // Після фільтрування номер рядка в таблиці не збігається з номером тренування,
            // тому беремо номер із першого стовпця «N»
            int index = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Index"].Value);
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

                    toolStripStatusLabel1.Text = "Тренування успішно скасовано!"    ;


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
            toolStripStatusLabel1.Text = $"Дані тренувань успішно збережено у файл! `{saveDialog.FileName}`";

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

        private void групиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGroups form = new FormGroups();
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
                toolStripStatusLabel1.Text = "Файл успішно видалено, а таблицю очищено";
            }
        }

        private void comboBox1_sportSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCoachesComboBox();
            RefreshSportsmenGrid();
            RefreshTrainingsGrid();   // тренування теж фільтруються за видом спорту
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
                        sportsmen[i].SportRank,
                        // Допуск з медичної картки: показуємо одразу, щоб було видно, кого можна записати
                        sportsmen[i].MedicalCard != null && sportsmen[i].MedicalCard.HasAccessToCompete
                            ? "Допущено"
                            : "Не допущено"
                    );
                }
            }
        }

        private void даніПроТренерівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "XML-файли (*.xml)|*.xml|Усі файли (*.*)|*.*";
            openDialog.Title = "Відкрити файл з тренерами";
            openDialog.InitialDirectory = DataFiles.Folder;
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            LoadCoaches(openDialog.FileName);
        }

        // Читання тренерів з XML-файлу: викликається з меню та автоматично при відкритті форми
        private void LoadCoaches(string path)
        {
            try
            {
                // Десеріалізація тренерів з того самого XML-файлу, що й у формі «Тренери»
                List<Coach>? loaded;
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    loaded = coachesSerializer.Deserialize(fs) as List<Coach>;
                }
                coachesCount = 0;
                if (loaded != null)
                {
                    foreach (Coach coach in loaded)
                    {
                        if (coachesCount >= coaches.Length) break;
                        coaches[coachesCount] = coach;
                        coachesCount++;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Файл не є коректним XML-файлом з тренерами!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Оновлюємо випадаючий список тренерів
            UpdateCoachesComboBox();
            toolStripStatusLabel1.Text = $"Дані тренерів завантажено з файлу  '{Path.GetFileName(path)}'";
        }

        private void даніПроСпортсменівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "JSON-файли (*.json)|*.json|Усі файли (*.*)|*.*";
            openDialog.Title = "Відкрити файл зі спортсменами";
            openDialog.InitialDirectory = DataFiles.Folder;
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            LoadSportsmen(openDialog.FileName);
        }

        // Читання спортсменів з JSON-файлу: викликається з меню та автоматично при відкритті форми
        private void LoadSportsmen(string path)
        {
            try
            {
                // Десеріалізація спортсменів (разом із медичними картами) з JSON-файлу форми «Спортсмени»
                List<Sportsman>? loaded;
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    loaded = JsonSerializer.Deserialize<List<Sportsman>>(fs);
                }
                sportsmenCount = 0;
                if (loaded != null)
                {
                    foreach (Sportsman sportsman in loaded)
                    {
                        if (sportsmenCount >= sportsmen.Length) break;
                        sportsmen[sportsmenCount] = sportsman;
                        sportsmenCount++;
                    }
                }
            }
            catch (JsonException)
            {
                MessageBox.Show("Файл не є коректним JSON-файлом зі спортсменами!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Оновлюємо верхню таблицю зі спортсменами
            RefreshSportsmenGrid();
            toolStripStatusLabel1.Text = $"Дані спортсменів завантажено з файлу '{Path.GetFileName(path)}'";
        }

        private void даніПроТренуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openDialog.Title = "Відкрити файл з тренуваннями";
            openDialog.InitialDirectory = DataFiles.Folder;
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            LoadTrainings(openDialog.FileName);
        }

        // Читання тренувань з текстового файлу: викликається з меню та автоматично при відкритті форми
        private void LoadTrainings(string path)
        {
            bookingsCount = 0;
            string[] lines = File.ReadAllLines(path);
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
            toolStripStatusLabel1.Text = $"Дані тренувань завантажено з файлу '{Path.GetFileName(path)}'";
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
                toolStripStatusLabel1.Text = "Файл успішно видалено, а таблицю тренувань очищено!";
            }
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // РОЗКЛАД НА ТИЖДЕНЬ 

        private readonly string[] dayNames =
            { "Неділя", "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота" };

        // Неділя того тижня, до якого належить дата
        private DateTime GetWeekStart(DateTime date)
        {
            // У C# DayOfWeek: неділя = 0, понеділок = 1, ..., субота = 6
            return date.Date.AddDays(-(int)date.DayOfWeek);
        }

        private void CreateScheduleGrid()
        {
            dataGridViewSchedule.Columns.Clear();
            dataGridViewSchedule.Columns.Add("Time", "Час");
            for (int d = 0; d < 7; d++)
            {
                dataGridViewSchedule.Columns.Add("Day" + d, dayNames[d]);
            }
            foreach (DataGridViewColumn column in dataGridViewSchedule.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridViewSchedule.ReadOnly = true;
            dataGridViewSchedule.AllowUserToAddRows = false;
            dataGridViewSchedule.AllowUserToResizeRows = false;
            dataGridViewSchedule.RowHeadersVisible = false;
            dataGridViewSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewSchedule.Columns["Time"]!.FillWeight = 40;   // стовпець «Time» створено вище, тому «!»

            // Багаторядковий текст у клітинках і заголовках
            dataGridViewSchedule.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewSchedule.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewSchedule.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewSchedule.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Мінімальна висота рядків розкладу така, щоб усі години заповнили таблицю без порожнього місця;
        // рядок із кількома тренуваннями може стати вищим, тоді з'явиться прокрутка
        private void FitScheduleRows()
        {
            int count = dataGridViewSchedule.Rows.Count;
            if (count == 0) return;

            int available = dataGridViewSchedule.ClientSize.Height - dataGridViewSchedule.ColumnHeadersHeight - 2;
            int height = Math.Max(available / count, 24);
            foreach (DataGridViewRow row in dataGridViewSchedule.Rows)
            {
                row.MinimumHeight = height;
            }
        }

        private void RefreshScheduleGrid()
        {
            if (dataGridViewSchedule.Columns.Count == 0)
            {
                CreateScheduleGrid();
            }

            DateTime weekStart = GetWeekStart(dateTimePickerWeek.Value);
            DateTime nextWeekStart = weekStart.AddDays(7);
            labelWeek.Text = $"Тиждень: {weekStart:dd.MM.yyyy} – {weekStart.AddDays(6):dd.MM.yyyy}";

            // Заголовки з датами, сьогоднішній день підсвічуємо
            for (int d = 0; d < 7; d++)
            {
                DateTime day = weekStart.AddDays(d);
                dataGridViewSchedule.Columns[d + 1].HeaderText = $"{dayNames[d]}\n{day:dd.MM}";
                dataGridViewSchedule.Columns[d + 1].DefaultCellStyle.BackColor =
                    day == DateTime.Today ? Color.FromArgb(255, 246, 213) : Color.Empty;   // м'який жовтий
            }

            // Рядки завжди однакові: по одному на кожну годину доби, 00:00-23:00
            dataGridViewSchedule.Rows.Clear();
            for (int hour = 6; hour < 22; hour++)
            {
                dataGridViewSchedule.Rows.Add($"{hour:00}:00");
            }

            // Розкладаємо тренування обраного тижня по клітинках
            for (int i = 0; i < bookingsCount; i++)
            {
                if (bookingDates[i] < weekStart || bookingDates[i] >= nextWeekStart)
                    continue;

                // Колонка 0 - це "Час", тому день тижня зсуваємо на 1; рядок = година
                int column = (int)bookingDates[i].DayOfWeek + 1;
                int row = bookingDates[i].Hour-6;
                if (row < 0 || row >= dataGridViewSchedule.Rows.Count)
                    continue;

                string text =
                    $"{bookingDates[i]:HH:mm} {bookedSportsmen[i].Sport}\n" +
                    $"Тренер: {bookedCoaches[i].Surname} {bookedCoaches[i].Name}\n" +
                    $"Спортсмен: {bookedSportsmen[i].Surname} {bookedSportsmen[i].Name}\n" +
                    $"{bookingDurations[i]} хв";

                DataGridViewCell cell = dataGridViewSchedule.Rows[row].Cells[column];
                // Якщо на цю годину вже є тренування, дописуємо нижче
                if (cell.Value == null)
                {
                  
                    cell.Value = text;
                }
                else
                {
                    // у клітинці вже є тренування — дописуємо нове під ним через риску
                    cell.Value = cell.Value + "\n———\n" + text;
                }
                cell.Style.BackColor = bookedSportsmen[i].Sport == "Веслування"
                    ? Color.FromArgb(207, 228, 250)    // веслування — світло-блакитний
                    : Color.FromArgb(212, 237, 218);   // велоспорт — світло-зелений
            }

            FitScheduleRows();
            dataGridViewSchedule.ClearSelection();
        }

        private void buttonPrevWeek_Click(object sender, EventArgs e)
        {
            dateTimePickerWeek.Value = dateTimePickerWeek.Value.AddDays(-7);
        }

        private void buttonNextWeek_Click(object sender, EventArgs e)
        {
            dateTimePickerWeek.Value = dateTimePickerWeek.Value.AddDays(7);
        }

        private void dateTimePickerWeek_ValueChanged(object sender, EventArgs e)
        {
            RefreshScheduleGrid();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Видалення працює з таблицею на вкладці "Запис", тому на розкладі кнопку вимикаємо
            toolStripButton2.Enabled = tabControl1.SelectedTab == tabPage1;
            RefreshScheduleGrid();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }

}
