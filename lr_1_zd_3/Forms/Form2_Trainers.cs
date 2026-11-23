using lr_1_zd_3.Interface;
using lr_1_zd_3.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using ClassLibrary1.Class;
using lr_1_zd_3.Aditionals;

namespace lr_1_zd_3
{
    public partial class Form2_Trainers : Form
    {
        public Form2_Trainers()
        {
            InitializeComponent();
        }
        // private Coach[] coaches = new Coach[100];
        private List<Coach> coaches = new List<Coach>();
        // Серіалізатор XML для списку тренерів: тип вказується один раз при створенні
        private static readonly XmlSerializer coachesSerializer = new 
        XmlSerializer(typeof(List<Coach>));
        // Налаштування JSON для запису результату фільтрування: з відступами та українськими літерами
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        //private int coachesCount = 0;


        private void Form2_Trainers_Load(object sender, EventArgs e)
        {
            dataGridView1_Input.ApplyLargeFont();
            dataGridView2_Output.ApplyLargeFont();
            // Вхідна і вихідна таблиці завжди однакової висоти
            Resize += (s, ev) => GridStyle.SplitEvenly(dataGridView1_Input, label2, dataGridView2_Output);
            GridStyle.SplitEvenly(dataGridView1_Input, label2, dataGridView2_Output);

            comboBox1_sportSelect.Items.Clear();
            comboBox1_sportSelect.Items.Add("Веслування");
            comboBox1_sportSelect.Items.Add("Велоспорт");

            comboBox_RankCoach.Items.Clear();
            comboBox_RankCoach.Items.Add("Молодший тренер");
            comboBox_RankCoach.Items.Add("Старший тренер");
            comboBox_RankCoach.Items.Add("Заслужений тренер");

            comboBox1_sportSelect.SelectedIndex = 0;
            comboBox_RankCoach.SelectedIndex = 0;


            // Підключення кнопок
            toolStripButton1.Click += toolStripButton1_Click;
            toolStripButton2.Click += toolStripButton2_Click;
            toolStripButton3.Click += toolStripButton3_Click;

            toolStripStatusLabel1.Text = "Готово до роботи";

            // Автоматичне завантаження бази тренерів з папки Data
            if (File.Exists(DataFiles.Coaches))
                LoadCoaches(DataFiles.Coaches);
        }


        private void CreateInputGrid()
        {
            dataGridView1_Input.Columns.Clear();

            dataGridView1_Input.Columns.Add("Index", "N");
            dataGridView1_Input.Columns.Add("Surname", "Прізвище");
            dataGridView1_Input.Columns.Add("Name", "Ім'я");
            dataGridView1_Input.Columns.Add("Pobatkovi", "По батькові");
            dataGridView1_Input.Columns.Add("Sport", "Вид спорту");
            dataGridView1_Input.Columns.Add("CoachRank", "Звання");
            dataGridView1_Input.Columns.Add("BirthDate", "Дата народження");

            dataGridView1_Input.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1_Input.ReadOnly = true;
            dataGridView1_Input.AllowUserToAddRows = false;
            dataGridView1_Input.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }

        // СТВОРЕННЯ ТАБЛИЦІ РЕЗУЛЬТАТІВ

        private void CreateOutputGrid()
        {
            dataGridView2_Output.Columns.Clear();

            dataGridView2_Output.Columns.Add("Index", "N");
            dataGridView2_Output.Columns.Add("Surname", "Прізвище");
            dataGridView2_Output.Columns.Add("Name", "Ім'я");
            dataGridView2_Output.Columns.Add("Pobatkovi", "По батькові");
            dataGridView2_Output.Columns.Add("Sport", "Вид спорту");
            dataGridView2_Output.Columns.Add("CoachRank", "Звання");
            dataGridView2_Output.Columns.Add("BirthDate", "Дата народження");

            dataGridView2_Output.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2_Output.ReadOnly = true;
            dataGridView2_Output.AllowUserToAddRows = false;
            dataGridView2_Output.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }
        // ОНОВЛЕННЯ ТАБЛИЦІ ВХІДНИХ ДАНИХ

        private void RefreshInputGrid()
        {
            dataGridView1_Input.Rows.Clear();

            if (dataGridView1_Input.Columns.Count == 0)
            {
                CreateInputGrid();
            }

            for (int i = 0; i < coaches.Count; i++)
            {

                dataGridView1_Input.Rows.Add(
                    i + 1,
                    coaches[i].Surname,
                    coaches[i].Name,
                    coaches[i].Pobatkovi,
                    coaches[i].Sport,
                    coaches[i].CoachRank,
                    coaches[i].BirthDate.ToShortDateString()
                );
            }
        }

        // ОНОВЛЕННЯ ТАБЛИЦІ РЕЗУЛЬТАТІВ

        private void RefreshOutputGrid(List<Coach> sorted)
        {
            // Будь-яке нове виведення (наприклад, сортування) скасовує попередній фільтр
            filteredCoaches = null;
            dataGridView2_Output.Rows.Clear();

            // Перевіряємо саме переданий список: він може бути відфільтрованим або прочитаним з файлу
            if (sorted.Count == 0)
            {
                dataGridView2_Output.Columns.Clear();
                return;
            }

            if (dataGridView2_Output.Columns.Count == 0)
            {
                CreateOutputGrid();
            }

            // Проходимо саме по переданому списку: після фільтрування він коротший за основний
            for (int i = 0; i < sorted.Count; i++)
            {

                dataGridView2_Output.Rows.Add(
                    i + 1,
                    sorted[i].Surname,
                    sorted[i].Name,
                    sorted[i].Pobatkovi,
                    sorted[i].Sport,
                    sorted[i].CoachRank,
                    sorted[i].BirthDate.ToShortDateString()
                );
            }
        }

        // ДОДАВАННЯ ТРЕНЕРА

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBox_inputNameCoach.Text) ||
                string.IsNullOrWhiteSpace(textBox_inputSurnameCoach.Text) ||
                string.IsNullOrWhiteSpace(textBox_inputPobatkoviCoach.Text))
            {
                MessageBox.Show(
                    "Заповніть усі поля!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            /*            // Перевірка кількості
                        if (coachesCount >= coaches.Count)
                        {
                            MessageBox.Show(
                                "Досягнуто максимальну кількість тренерів!",
                                "Помилка",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }*/

            // Створення об'єкта Coach
            Coach coach = new Coach(
                textBox_inputNameCoach.Text,
                textBox_inputSurnameCoach.Text,
                textBox_inputPobatkoviCoach.Text,
                dateTimePicker1_CoachBirthday.Value,
                comboBox1_sportSelect.Text,
                comboBox_RankCoach.Text
            );

            // Додавання в масив
            coaches.Add(coach);
            //coachesCount++;

            RefreshInputGrid();

            ClearFields();

            toolStripStatusLabel1.Text =
                "Тренера успішно додано";
        }

        // ВИДАЛЕННЯ ТРЕНЕРА

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1_Input.CurrentRow == null)
            {
                MessageBox.Show(
                    "Виберіть тренера для видалення!",
                    "Увага",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int index = dataGridView1_Input.CurrentRow.Index;

            if (index < 0 || index >= coaches.Count)
                return;

            DialogResult result = MessageBox.Show(
                "Ви дійсно хочете видалити цього тренера?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                coaches.RemoveAt(index);


                /*          // Зсуваємо елементи масиву
                          for (int i = index; i < coachesCount - 1; i++)
                          {
                              coaches[i] = coaches[i + 1];
                          }

                          coaches[coachesCount - 1] = null;*/
                //coachesCount--;

                RefreshInputGrid();

                toolStripStatusLabel1.Text =
                    "Тренера видалено";
            }
        }
        // РЕДАГУВАННЯ ТРЕНЕРА

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1_Input.CurrentRow == null)
            {
                MessageBox.Show(
                    "Виберіть тренера для редагування!",
                    "Увага",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int index = dataGridView1_Input.CurrentRow.Index;

            if (index < 0 || index >= coaches.Count)
                return;

            if (string.IsNullOrWhiteSpace(textBox_inputNameCoach.Text) ||
                string.IsNullOrWhiteSpace(textBox_inputSurnameCoach.Text) ||
                string.IsNullOrWhiteSpace(textBox_inputPobatkoviCoach.Text))
            {
                MessageBox.Show(
                    "Заповніть усі поля!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            Coach coach = coaches[index];

            coach.Name = textBox_inputNameCoach.Text;
            coach.Surname = textBox_inputSurnameCoach.Text;
            coach.Pobatkovi = textBox_inputPobatkoviCoach.Text;
            coach.Sport = comboBox1_sportSelect.Text;
            coach.CoachRank = comboBox_RankCoach.Text;
            coach.BirthDate = dateTimePicker1_CoachBirthday.Value;

            RefreshInputGrid();

            toolStripStatusLabel1.Text =
                "Дані тренера змінено";

            MessageBox.Show(
                "Дані тренера успішно змінено!",
                "Інформація",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        // Довідка → Про програму: окреме вікно з темою та можливостями програми
        private void проToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormAbout about = new FormAbout())
            {
                about.ShowDialog(this);
            }
        }

        private void ClearFields()
        {
            textBox_inputNameCoach.Clear();
            textBox_inputSurnameCoach.Clear();
            textBox_inputPobatkoviCoach.Clear();

            comboBox1_sportSelect.SelectedIndex = 0;
            comboBox_RankCoach.SelectedIndex = 0;
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void спортсмениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void dataGridView2_Output_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void заПрізвищемToolStripMenuItem_Click(object sender, EventArgs e)
        {

           // Coach[] sorted = new Coach[coaches.Count];
            List <Coach> sorted = new List<Coach>(coaches);

/*            for (int i = 0; i < coaches.Count; i++)
            {
                sorted[i] = coaches[i];
            }*/

            // Використовуємо той самий інтерфейс
            ISortableBySurname sorter = new Coach();
            // Сортуємо масив тренерів
            sorter.SortBySurname(sorted);

            RefreshOutputGrid(sorted);
        }

        //  Фільтрування тренерів за видом спорту
        private void FilterBySport(string sport)
        {
            if (coaches.Count == 0)
            {
                MessageBox.Show("Список тренерів порожній!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Coach> filtered = new List<Coach>();
            foreach (Coach c in coaches)
            {
                if (c.Sport == sport)
                    filtered.Add(c);
            }

            RefreshOutputGrid(filtered);
            // Запам'ятовуємо результат, щоб його можна було записати у файл
            filteredCoaches = filtered;
            filteredSport = sport;
            toolStripStatusLabel1.Text = $"Фільтр «{sport}»: знайдено {filtered.Count} з {coaches.Count} тренерів";
        }

        // Останній результат фільтрування (null — фільтр не застосовано)
        private List<Coach>? filteredCoaches;
        private string filteredSport = "";

        private void веслуванняФільтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterBySport("Веслування");
        }

        private void велоспортФільтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterBySport("Велоспорт");
        }

        private void усіТренериФільтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshOutputGrid(coaches);   // також скидає filteredCoaches — записувати нічого
            toolStripStatusLabel1.Text = "Фільтр знято: показано всіх тренерів";
        }

        // Фільтрування → Записати результат у JSON
        private void записФільтруJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filteredCoaches == null)
            {
                MessageBox.Show("Спочатку виберіть фільтр: «Веслування» або «Велоспорт»!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (filteredCoaches.Count == 0)
            {
                MessageBox.Show($"За фільтром «{filteredSport}» не знайдено жодного тренера — записувати нічого.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JSON-файли (*.json)|*.json|Усі файли (*.*)|*.*";
            saveDialog.Title = "Зберегти результат фільтрування";
            saveDialog.InitialDirectory = DataFiles.Folder;
            saveDialog.FileName = $"Тренери_{filteredSport}.json";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            // Серіалізація лише відфільтрованих тренерів
            using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, filteredCoaches, jsonOptions);
            }

            toolStripStatusLabel1.Text = $"Результат фільтра «{filteredSport}» ({filteredCoaches.Count} тренерів) записано у файл '{Path.GetFileName(saveDialog.FileName)}'";
        }

        // Фільтрування → Прочитати результат з JSON:
        // показує збережений результат у таблиці результатів, основний список тренерів не змінюється
        private void читанняФільтруJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "JSON-файли (*.json)|*.json|Усі файли (*.*)|*.*";
            openDialog.Title = "Відкрити результат фільтрування";
            openDialog.InitialDirectory = DataFiles.Folder;

            if (openDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                List<Coach>? loaded;
                using (FileStream fs = new FileStream(openDialog.FileName, FileMode.Open))
                {
                    loaded = JsonSerializer.Deserialize<List<Coach>>(fs, jsonOptions);
                }

                if (loaded == null || loaded.Count == 0)
                {
                    MessageBox.Show("У файлі немає жодного тренера!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RefreshOutputGrid(loaded);
                // Прочитаний результат можна знову записати у файл
                filteredCoaches = loaded;
                filteredSport = Path.GetFileNameWithoutExtension(openDialog.FileName);
                toolStripStatusLabel1.Text = $"Прочитано {loaded.Count} тренерів з файлу '{Path.GetFileName(openDialog.FileName)}'";
            }
            catch (JsonException)
            {
                MessageBox.Show("Файл не є коректним JSON-файлом з тренерами!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void читанняЗФайлToolStripMenuItem_Click(object sender, EventArgs e)
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
                // Десеріалізація: відновлюємо список тренерів із XML-файлу
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    List<Coach>? loaded = coachesSerializer.Deserialize(fs) as List<Coach>;
                    coaches.Clear();
                    if (loaded != null) coaches.AddRange(loaded);
                }

                RefreshInputGrid();
                toolStripStatusLabel1.Text = $"Дані десеріалізовано з файлу '{Path.GetFileName(path)}'";
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Файл не є коректним XML-файлом з тренерами!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*            OpenFileDialog openDialog = new OpenFileDialog();

                          openDialog.Filter =
                              "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";

                          openDialog.Title = "Відкрити файл з тренерами";

                          if (openDialog.ShowDialog() != DialogResult.OK)
                              return;

                          coaches.Clear();
                          string[] lines = File.ReadAllLines(openDialog.FileName);

                          foreach (string line in lines)
                          {
                              string[] parts = line.Split(';');

                              if (parts.Length == 6)
                              {
                                  Coach coach = new Coach(
                                      parts[1],                 // Ім'я
                                      parts[0],                 // Прізвище
                                      parts[2],                 // По батькові
                                      DateTime.Parse(parts[5]), // Дата народження
                                      parts[3],                 // Вид спорту
                                      parts[4]                  // Звання тренера
                                  );

                                  coaches.Add(coach);

                              }
                          }

                          RefreshInputGrid();

                          toolStripStatusLabel1.Text = $"Дані завантажено з файлу `{openDialog.SafeFileName}`";*/
        }

        private void записУФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML-файли (*.xml)|*.xml|Усі файли (*.*)|*.*";
            saveDialog.Title = "Зберегти дані тренерів";
            saveDialog.FileName = "coaches.xml";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            // Серіалізація: увесь список тренерів у XML одним викликом
            using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create))
            {
                coachesSerializer.Serialize(fs, coaches);
            }

            toolStripStatusLabel1.Text = $"Дані серіалізовано у файл '{saveDialog.FileName}'";


            /*       SaveFileDialog saveDialog = new SaveFileDialog();

                   saveDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
                   saveDialog.Title = "Зберегти дані тренерів";

                   if (saveDialog.ShowDialog() != DialogResult.OK)
                       return;

                   using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                   {
                       for (int i = 0; i < coaches.Count; i++)
                       {

                           writer.WriteLine(
                               $"{coaches[i].Surname};" +
                               $"{coaches[i].Name};" +
                               $"{coaches[i].Pobatkovi};" +
                               $"{coaches[i].Sport};" +
                               $"{coaches[i].CoachRank};" +
                               $"{coaches[i].BirthDate}"
                           );
                       }
                   }

                   toolStripStatusLabel1.Text = $"Дані записано у файл '{saveDialog.FileName}'";*/
        }

        private void очиститиФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter =
                "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";

            openDialog.Title = "Виберіть файл для очищення";

            if (openDialog.ShowDialog() != DialogResult.OK)
                return;

            DialogResult result = MessageBox.Show(
                "Ви дійсно хочете очистити цей файл?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                File.Delete(openDialog.FileName);

                toolStripStatusLabel1.Text =
                    "Файл успішно очищено";
            }
        }

        private void тренуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trainings form = new Trainings();
            form.Show();

        }

        private void групиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGroups form = new FormGroups();
            form.Show();
            this.Hide();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1_Input.CurrentRow == null)
            {
                MessageBox.Show("Оберіть тренера з таблиці!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView1_Input.CurrentRow.Index;
            if (index >= 0 && index < coaches.Count)
            {

                MessageBox.Show(coaches[index].ShowInfo(), "Картка тренера", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {

        }
    }

}


