using System.Text.Encodings.Web;
using System.Text.Unicode;
using lr_1_zd_3.Forms;
using lr_1_zd_3.Interface;
using System.Text.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

using SportsmenList = System.Collections.Generic.List<ClassLibrary1.Class.Sportsman>;
using ClassLibrary1.Class;
using ClassLibrary1;
using static System.String;
using lr_1_zd_3.Aditionals;



namespace lr_1_zd_3
{
    public partial class Form1 : Form
    {
        // Налаштування JSON: відступи для читабельності, кирилиця без \u-кодів
        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };

        // private Sportsman[] sportsmen = new Sportsman[100];

       // private List<Sportsman> sportsmen = new List<Sportsman>();
         private SportsmenList sportsmen = new SportsmenList();

        //private int sportsmenCount = 0;
        //Делегат на метод класу
        delegate void StatusUpdateDelegate(string message);
        private StatusUpdateDelegate setStatus;
        private void UpdateStatusText(string message)
        {
            toolStripStatusLabel1.Text = message;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            setStatus = new StatusUpdateDelegate(UpdateStatusText);

            dataGridView1_Input.ApplyLargeFont();
            dataGridView2_Output.ApplyLargeFont();
            // Вхідна і вихідна таблиці завжди однакової висоти
            Resize += (s, ev) => GridStyle.SplitEvenly(dataGridView1_Input, label2, dataGridView2_Output);
            GridStyle.SplitEvenly(dataGridView1_Input, label2, dataGridView2_Output);

            comboBox1_sportSelect.Items.Add("Веслування");
            comboBox1_sportSelect.Items.Add("Велоспорт");
            comboBox2.Items.Add("Без розряду");
            comboBox2.Items.Add("3 розряд");
            comboBox2.Items.Add("2 розряд");
            comboBox2.Items.Add("1 розряд");
            comboBox2.Items.Add("Кандитат у майстри спорту України");
            comboBox2.Items.Add("Майстер спорту України");
            comboBox1_sportSelect.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox_bloodGroup.Items.AddRange(new object[] { "O+", "O-", "A+", "A-", "B+", "B-", "AB+", "AB-" });
            comboBox_bloodGroup.SelectedIndex = 0;
            setStatus("Готово");

            // Автоматичне завантаження бази спортсменів з папки Data
            if (File.Exists(DataFiles.Sportsmen))
                LoadSportsmen(DataFiles.Sportsmen);
        }
        private void CreateInputGrid()
        {
            dataGridView1_Input.Columns.Clear();
            dataGridView1_Input.Columns.Add("Index", "Індекс");
            dataGridView1_Input.Columns.Add("Surname", "Прізвище");
            dataGridView1_Input.Columns.Add("Name", "Ім'я");
            dataGridView1_Input.Columns.Add("Pobatkovi", "По батькові");
            dataGridView1_Input.Columns.Add("Sport", "Вид спорту");
            dataGridView1_Input.Columns.Add("SportRank", "Спортивний розряд");
            dataGridView1_Input.Columns.Add("BirthDate", "Дата народження");
            dataGridView1_Input.Columns.Add("PrizePlaces", "Кількість призових місць");
            dataGridView1_Input.Columns.Add("BloodGroup", "Група крові");
            dataGridView1_Input.Columns.Add("HasAccess", "Допуск до змагань");
            dataGridView1_Input.Columns.Add("InspectDate", "Дата медогляду");

            dataGridView1_Input.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1_Input.ReadOnly = true;
            dataGridView1_Input.AllowUserToAddRows = false;
        }
        private void CreateOutputGrid()
        {
            dataGridView2_Output.Columns.Clear();
            dataGridView2_Output.Columns.Add("Index", "Індекс");
            dataGridView2_Output.Columns.Add("Surname", "Прізвище");
            dataGridView2_Output.Columns.Add("Name", "Ім'я");
            dataGridView2_Output.Columns.Add("Sport", "Вид спорту");
            dataGridView2_Output.Columns.Add("SportRank", "Спортивний розряд");
            dataGridView2_Output.Columns.Add("BirthDate", "Дата народження");
            dataGridView2_Output.Columns.Add("PrizePlaces", "Кількість призових місць");
            dataGridView2_Output.Columns.Add("BloodGroup", "Група крові");
            dataGridView2_Output.Columns.Add("HasAccess", "Допуск до змагань");
            dataGridView2_Output.Columns.Add("InspectDate", "Дата медогляду");


            dataGridView2_Output.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2_Output.ReadOnly = true;
            dataGridView2_Output.AllowUserToAddRows = false;
        }
        private void RefreshInputGrid()
        {
            if (sportsmen.Count == 0)
            {
                dataGridView1_Input.Columns.Clear();
                return;
            }
            if (dataGridView1_Input.Columns.Count == 0)
            {
                CreateInputGrid();
            }
            dataGridView1_Input.Rows.Clear();
            for (int i = 0; i < sportsmen.Count; i++)
            {

                dataGridView1_Input.Rows.Add(
                    i,
                    sportsmen[i].Surname,
                    sportsmen[i].Name,
                    sportsmen[i].Pobatkovi,
                    sportsmen[i].Sport,
                    sportsmen[i].SportRank,
                    sportsmen[i].BirthDate.ToShortDateString(),
                    sportsmen[i].PrizePlaces,
                    sportsmen[i].MedicalCard.BloodGroup,
                    sportsmen[i].MedicalCard.HasAccessToCompete ? "Допущено" : "Не допущено",
                    sportsmen[i].MedicalCard.InspectationDate.ToShortDateString()
                );
            }

            setStatus($"Кількість спортсменів: {sportsmen.Count}");
        }
        private void RefreshOutputGrid()
        {
            dataGridView2_Output.Rows.Clear();

            if (sportsmen.Count == 0)
            {
                dataGridView2_Output.Columns.Clear();
                return;
            }

            if (dataGridView2_Output.Columns.Count == 0)
            {
                CreateOutputGrid();
            }

            for (int i = 0; i < sportsmen.Count; i++)
            {

                dataGridView2_Output.Rows.Add(
                    i,
                    sportsmen[i].Surname,
                    sportsmen[i].Name,
                    sportsmen[i].Sport,
                    sportsmen[i].SportRank,
                    sportsmen[i].BirthDate.ToShortDateString(),
                    sportsmen[i].PrizePlaces,
                    sportsmen[i].MedicalCard.BloodGroup,
                    sportsmen[i].MedicalCard.HasAccessToCompete ? "Допущено" : "Не допущено",
                    sportsmen[i].MedicalCard.InspectationDate.ToShortDateString()
                );
            }
        }
        private void ClearInputFields()
        {
            textBox_inputNameSportsmen.Clear();
            textBox_inputSurnameSportsmen.Clear();
            textBox_inputPobatkoviSportsmen.Clear();


            comboBox1_sportSelect.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            numericUpDown1_amountOfMedals.Value = 0;
            comboBox_bloodGroup.SelectedIndex = 0;
            checkBox_medAccess.Checked = false;
            dateTimePicker_medInspection.Value = DateTime.Today;
        }
        // Делегат як метод класу
        delegate void MyDelegate(string s);
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            /* if (string.IsNullOrWhiteSpace(textBox_inputNameSportsmen.Text) ||
                 string.IsNullOrWhiteSpace(textBox_inputSurnameSportsmen.Text) ||
                 string.IsNullOrWhiteSpace(textBox_inputPobatkoviSportsmen.Text))*/

            // Використання статичного імпорту для String.IsNullOrWhiteSpace
            if (IsNullOrWhiteSpace(textBox_inputNameSportsmen.Text) ||
                IsNullOrWhiteSpace(textBox_inputSurnameSportsmen.Text) ||
                IsNullOrWhiteSpace(textBox_inputPobatkoviSportsmen.Text))

            {
                MessageBox.Show(
                    "Заповніть ім'я, прізвище та по батькові!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }
            /*   // Перевірка кількості
               if (sportsmenCount >= sportsmen.Count)
               {
                   MessageBox.Show(
                       "Досягнуто максимальну кількість спортсменів!",
                       "Помилка",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Warning);

                   return;
               }*/

            void Myfunc(string s)
            {
                MessageBox.Show(s);
            }
            MyDelegate del = new MyDelegate(Myfunc);
            del("Викликано делегат\n" + "Імя: " + textBox_inputNameSportsmen.Text + "\n Прізвище: " + textBox_inputSurnameSportsmen.Text + "\nПобатьковві: " + textBox_inputPobatkoviSportsmen.Text);



            sportsmen.Add(new Sportsman
            {
                Name = textBox_inputNameSportsmen.Text.ToTitleCase(),
                Surname = textBox_inputSurnameSportsmen.Text.ToTitleCase(),
                Pobatkovi = textBox_inputPobatkoviSportsmen.Text.ToTitleCase(),
                Sport = comboBox1_sportSelect.Text,
                SportRank = comboBox2.Text,
                BirthDate = dateTimePicker1_sportsmenBirthday.Value,
                PrizePlaces = (int)numericUpDown1_amountOfMedals.Value,
                MedicalCard = new MedicalCard
                {
                    BloodGroup = comboBox_bloodGroup.Text,
                    HasAccessToCompete = checkBox_medAccess.Checked,
                    InspectationDate = dateTimePicker_medInspection.Value
                }
            });


            RefreshInputGrid();

            ClearInputFields();
            setStatus("Спортсмен успішно доданий");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1_Input.CurrentRow == null)
            {
                MessageBox.Show(
                    "Виберіть спортсмена для видалення!",
                    "Увага",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int index = dataGridView1_Input.CurrentRow.Index;

            if (index >= 0 && index < sportsmen.Count)
            {
                DialogResult result = MessageBox.Show(
                    "Ви дійсно хочете видалити цього спортсмена?",
                    "Підтвердження",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Зсуваємо всі наступні елементи на одну позицію вліво
                    /*                 for (int i = index; i < sportsmenCount - 1; i++)
                                     {
                                         sportsmen[i] = sportsmen[i + 1];
                                     }

                                     sportsmen[sportsmenCount - 1] = null;*/

                    sportsmen.RemoveAt(index);


                    RefreshInputGrid();

                    setStatus("Спортсмен видалений");
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!inputControl())
                return;

            // inputControl() вже перевірив, що рядок вибрано, тому ставимо «!» — тут точно не null
            int rowIndex = dataGridView1_Input.CurrentCell!.RowIndex;
            int columnIndex = dataGridView1_Input.CurrentCell.ColumnIndex;
            if (rowIndex < 0 || rowIndex >= sportsmen.Count)
                return;

            Sportsman sportsman = sportsmen[rowIndex];
            switch (columnIndex)
            {
                case 1: // Прізвище
                    if (!IsNullOrWhiteSpace(textBox_inputSurnameSportsmen.Text))
                    {
                        sportsman.Surname =
                            textBox_inputSurnameSportsmen.Text;
                    }
                    break;

                case 2: // Ім'я
                    if (!IsNullOrWhiteSpace(textBox_inputNameSportsmen.Text))
                    {
                        sportsman.Name =
                            textBox_inputNameSportsmen.Text;
                    }
                    break;

                case 3: // По батькові
                    if (!IsNullOrWhiteSpace(textBox_inputPobatkoviSportsmen.Text))
                    {
                        sportsman.Pobatkovi =
                            textBox_inputPobatkoviSportsmen.Text;
                    }
                    break;

                case 4: // Вид спорту
                    sportsman.Sport =
                        comboBox1_sportSelect.Text;
                    break;

                case 5: // Спортивний розряд
                    sportsman.SportRank =
                        comboBox2.Text;
                    break;

                case 6: // Дата народження
                    sportsman.BirthDate =
                        dateTimePicker1_sportsmenBirthday.Value;
                    break;
                case 7: // Кількість призових місць
                    sportsman.PrizePlaces =
                        (int)numericUpDown1_amountOfMedals.Value;
                    break;

                case 8: // Група крові
                    sportsman.MedicalCard.BloodGroup =
                        comboBox_bloodGroup.Text;
                    break;

                case 9: // Допуск до змагань
                    sportsman.MedicalCard.HasAccessToCompete =
                        checkBox_medAccess.Checked;
                    break;

                case 10: // Дата медогляду
                    sportsman.MedicalCard.InspectationDate =
                        dateTimePicker_medInspection.Value;
                    break;

                case 0: // Індекс
                    MessageBox.Show(
                        "Індекс змінювати не можна!",
                        "Увага",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
            }

            RefreshInputGrid();

            setStatus("Вибране поле успішно змінено");
        }

        private void записУФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*      SaveFileDialog saveDialog = new SaveFileDialog();

                  saveDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
                  saveDialog.Title = "Зберегти дані спортсменів";

                  if (saveDialog.ShowDialog() != DialogResult.OK)
                      return;

                  using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                  {
                      for (int i = 0; i < sportsmen.Count; i++)
                      {


                          writer.WriteLine(
                              $"{sportsmen[i].Surname};" +
                              $"{sportsmen[i].Name};" +
                              $"{sportsmen[i].Pobatkovi};" +
                              $"{sportsmen[i].Sport};" +
                              $"{sportsmen[i].SportRank};" +
                              $"{sportsmen[i].BirthDate};" +
                              $"{sportsmen[i].PrizePlaces};" +
                              $"{sportsmen[i].MedicalCard.BloodGroup};" +
                              $"{sportsmen[i].MedicalCard.HasAccessToCompete};" +
                              $"{sportsmen[i].MedicalCard.InspectationDate:dd.MM.yyyy}"
                          );
                      }
                  }

                  setStatus($"Дані записано у файл '{saveDialog.FileName}'");*/
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "JSON-файли (*.json)|*.json|Усі файли (*.*)|*.*";
            saveDialog.Title = "Зберегти дані спортсменів";
            saveDialog.FileName = "sportsmen.json";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            // Серіалізація: увесь список спортсменів разом із медичними картами одним викликом
            using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, sportsmen, jsonOptions);
            }

            setStatus($"Дані серіалізовано у файл '{saveDialog.FileName}'");
        }

        private void читанняЗФайлToolStripMenuItem_Click(object sender, EventArgs e)
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
                // Десеріалізація: відновлюємо список спортсменів разом із медичними картами
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    SportsmenList? loaded = JsonSerializer.Deserialize<SportsmenList>(fs, jsonOptions);
                    sportsmen.Clear();
                    if (loaded != null) sportsmen.AddRange(loaded);
                }

                RefreshInputGrid();
                setStatus($"Дані десеріалізовано з файлу '{Path.GetFileName(path)}'");
            }
            catch (JsonException)
            {
                MessageBox.Show("Файл не є коректним JSON-файлом зі спортсменами!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /*            OpenFileDialog openDialog = new OpenFileDialog();
                        openDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
                        openDialog.Title = "Відкрити файл зі спортсменами";
                        if (openDialog.ShowDialog() != DialogResult.OK)
                            return;

                        sportsmen.Clear();
                        List<string> lines = File.ReadAllLines(openDialog.FileName).ToList();
                        foreach (string line in lines)
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length >= 7)
                            {
                                Sportsman sportsman = new Sportsman(

                               parts[1],                // Ім'я
                               parts[0],                // Прізвище
                               parts[2],                // По батькові
                               parts[3],                // Вид спорту
                               parts[4],               // Спортивний розряд
                               DateTime.Parse(parts[5]),  // Дата народження
                               int.Parse(parts[6])       // Кількість призових місць
                               );
                                if (parts.Length >= 10)
                                {
                                    sportsman.MedicalCard.BloodGroup = parts[7];
                                    sportsman.MedicalCard.HasAccessToCompete = bool.Parse(parts[8]);
                                    sportsman.MedicalCard.InspectationDate = DateTime.Parse(parts[9]);
                                }
                                sportsmen.Add(sportsman);

                            }
                        }

                        RefreshInputGrid();

                        setStatus($"Дані завантажено з файлу '{openDialog.SafeFileName}'");*/
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

                setStatus($"Файл  '{openDialog.SafeFileName}' успішно очищено");
            }
        }

        private void заПрізвищемToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Копіюємо масив, щоб основний залишився в порядку
            SportsmenList sorted = new SportsmenList(sportsmen);
            /*      for (int i = 0; i < sportsmenCount; i++)
                 {
                     sorted[i] = sportsmen[i];
                 }*/

            ISortableBySurname sorter = new Sportsman();
            sorter.SortBySurname(sorted);

            ShowSortedSportsmen(sorted);
        }

        //  Фільтрування за видом спорту
        private void FilterBySport(string sport)
        {
            if (sportsmen.Count == 0)
            {
                MessageBox.Show("Список спортсменів порожній!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SportsmenList filtered = new SportsmenList();
            foreach (Sportsman s in sportsmen)
            {
                if (s.Sport == sport)       
                    filtered.Add(s);
            }

            ShowSortedSportsmen(filtered);
            // Запам'ятовуємо результат, щоб його можна було записати у файл
            filteredSportsmen = filtered;
            filteredSport = sport;
            setStatus($"Фільтр «{sport}»: знайдено {filtered.Count} з {sportsmen.Count} спортсменів");
        }

        // Останній результат фільтрування (null — фільтр не застосовано)
        private SportsmenList? filteredSportsmen;
        private string filteredSport = "";

        // Фільтрування → Записати результат у JSON
        private void записФільтруJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filteredSportsmen == null)
            {
                MessageBox.Show("Спочатку виберіть фільтр: «Веслування» або «Велоспорт»!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (filteredSportsmen.Count == 0)
            {
                MessageBox.Show($"За фільтром «{filteredSport}» не знайдено жодного спортсмена — записувати нічого.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JSON-файли (*.json)|*.json|Усі файли (*.*)|*.*";
            saveDialog.Title = "Зберегти результат фільтрування";
            saveDialog.InitialDirectory = DataFiles.Folder;
            saveDialog.FileName = $"Спортсмени_{filteredSport}.json";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            // Серіалізація лише відфільтрованих спортсменів (разом із медичними картами)
            using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, filteredSportsmen, jsonOptions);
            }

            setStatus($"Результат фільтра «{filteredSport}» ({filteredSportsmen.Count} спортсменів) записано у файл '{Path.GetFileName(saveDialog.FileName)}'");
        }

        // Фільтрування → Прочитати результат з JSON:
        // показує збережений результат у таблиці результатів, основний список спортсменів не змінюється
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
                SportsmenList? loaded;
                using (FileStream fs = new FileStream(openDialog.FileName, FileMode.Open))
                {
                    loaded = JsonSerializer.Deserialize<SportsmenList>(fs, jsonOptions);
                }

                if (loaded == null || loaded.Count == 0)
                {
                    MessageBox.Show("У файлі немає жодного спортсмена!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ShowSortedSportsmen(loaded);
                // Прочитаний результат можна знову записати у файл
                filteredSportsmen = loaded;
                filteredSport = Path.GetFileNameWithoutExtension(openDialog.FileName);
                setStatus($"Прочитано {loaded.Count} спортсменів з файлу '{Path.GetFileName(openDialog.FileName)}'");
            }
            catch (JsonException)
            {
                MessageBox.Show("Файл не є коректним JSON-файлом зі спортсменами!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void веслуванняФільтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterBySport("Веслування");
        }

        private void велоспортФільтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterBySport("Велоспорт");
        }

        private void усіСпортсмениФільтрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filteredSportsmen = null;   // фільтр знято — записувати нічого
            RefreshOutputGrid();
            setStatus("Фільтр знято: показано всіх спортсменів");
        }

        private void ShowSortedSportsmen(SportsmenList sorted)
        {
            // Будь-яке нове виведення (наприклад, сортування) скасовує попередній фільтр
            filteredSportsmen = null;
            dataGridView2_Output.Rows.Clear();

            // Перевіряємо саме переданий список: він може бути прочитаний з файлу, коли основний порожній
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
                Sportsman sportsman = sorted[i];

                dataGridView2_Output.Rows.Add(
                    i,
                    sportsman.Surname,
                    sportsman.Name,
                    sportsman.Sport,
                    sportsman.SportRank,
                    sportsman.BirthDate.ToShortDateString(),
                    sportsman.PrizePlaces,
                    sportsman.MedicalCard.BloodGroup,
                    sportsman.MedicalCard.HasAccessToCompete ? "Допущено" : "Не допущено",
                    sportsman.MedicalCard.InspectationDate.ToShortDateString()
                );
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!inputControl())
                return;

            // inputControl() вже перевірив, що рядок вибрано, тому ставимо «!» — тут точно не null
            int index = dataGridView1_Input.CurrentRow!.Index;
            if (index >= 0 && index < sportsmen.Count)
            {
                sportsmen[index]++;
                RefreshOutputGrid();
                setStatus("Кількість призових місць спортсмена збільшено на 1");

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (!inputControl())
                return;

            // inputControl() вже перевірив, що рядок вибрано, тому ставимо «!» — тут точно не null
            int index = dataGridView1_Input.CurrentRow!.Index;
            if (index >= 0 && index < sportsmen.Count)
            {
                sportsmen[index]--;
                RefreshOutputGrid();
                setStatus("Кількість призових місць спортсмена зменшено на 1");

            }
        }
        private bool inputControl()
        {
            if (dataGridView1_Input.CurrentRow == null)
            {
                MessageBox.Show(
                    "Виберіть спортсмена!",
                    "Увага",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;

            }
            return true;
        }

        private void заІменемПеревантаженняОператорівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Копіюємо масив, щоб основний залишився в порядку
            SportsmenList sorted = new SportsmenList(sportsmen);
            sorted.Sort((x, y) => string.Compare(x.Name, y.Name));
            /*
                        for (int i = 0; i < sportsmenCount - 1; i++)
                        {
                            for (int j = 0; j < sportsmenCount - i - 1; j++)
                            {
                                if (sorted[j] > sorted[j + 1])
                                {
                                    Sportsman temp = sorted[j];
                                    sorted[j] = sorted[j + 1];
                                    sorted[j + 1] = temp;
                                }
                            }
                        }*/

            ShowSortedSportsmen(sorted);
        }

        private void спортсмениToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void тренериToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2_Trainers form = new Form2_Trainers();
            form.Show();
            this.Hide();
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
                MessageBox.Show("Оберіть спортсмена з таблиці!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView1_Input.CurrentRow.Index;
            if (index >= 0 && index < sportsmen.Count)
            {
                MessageBox.Show(sportsmen[index].ShowInfo(), "Картка спортсмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (dataGridView1_Input.CurrentRow == null)
            {
                MessageBox.Show("Оберіть спортсмена з таблиці!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView1_Input.CurrentRow.Index;
            if (index >= 0 && index < sportsmen.Count)
            {
                // Відкриваємо MedicalForm і передаємо туди обраного спортсмена
                MedicalForm medForm = new MedicalForm(sportsmen[index]);
                medForm.MedicalCardSaved += (s, ev) => setStatus($"Медичну карту для {sportsmen[index].Surname} оновлено");
                medForm.ShowDialog();
            }

        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Довідка → Про програму: окреме вікно з темою та можливостями програми
        private void проToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormAbout about = new FormAbout())
            {
                about.ShowDialog(this);
            }
        }

        #region РЕФЛЕКСІЯ
        private void рефлексіяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Збірка, в якій лежить клас Sportsman, — тобто наша бібліотека ClassLibrary1.dll
            Assembly assembly = typeof(Sportsman).Assembly;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"=== БІБЛІОТЕКА: {assembly.GetName().Name} ===");
            sb.AppendLine();

            // Усі публічні типи бібліотеки без службових класів компілятора
            var types = assembly.GetExportedTypes()
                .Where(t => !Attribute.IsDefined(t, typeof(CompilerGeneratedAttribute)))
                .OrderBy(t => t.Namespace)
                .ThenBy(t => t.Name);

            foreach (Type type in types)
            {
                // Категорія типу
                string category = "КЛАС";
                if (type.IsInterface) category = "ІНТЕРФЕЙС";
                else if (type.IsAbstract && type.IsSealed) category = "СТАТИЧНИЙ КЛАС";
                else if (type.IsAbstract) category = "АБСТРАКТНИЙ КЛАС";
                else if (type.IsEnum) category = "ПЕРЕЛІК (ENUM)";
                else if (type.IsValueType) category = "СТРУКТУРА (STRUCT)";

                sb.AppendLine($"[{category}] {type.FullName}");

                // Базовий клас
                if (type.BaseType != null && type.BaseType != typeof(object) &&
                    type.BaseType != typeof(ValueType) && type.BaseType != typeof(Enum))
                {
                    sb.AppendLine($"  ├── Успадковано від (базовий клас): {type.BaseType.Name}");
                }

                // Реалізовані інтерфейси
                Type[] interfaces = type.GetInterfaces();
                if (interfaces.Length > 0)
                {
                    sb.AppendLine($"  ├── Реалізує інтерфейси: {string.Join(", ", interfaces.Select(i => i.Name))}");
                }

                // Властивості, оголошені саме в цьому типі
                PropertyInfo[] properties = type.GetProperties(
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
                if (properties.Length > 0)
                {
                    sb.AppendLine("  ├── Властивості:");
                    foreach (PropertyInfo prop in properties)
                    {
                        sb.AppendLine($"  │     • {prop.PropertyType.Name} {prop.Name}");
                    }
                }

                // Методи без службових get_/set_ для властивостей
                MethodInfo[] methods = type.GetMethods(
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    .ToArray();
                if (methods.Length > 0)
                {
                    sb.AppendLine("  └── Методи:");
                    foreach (MethodInfo method in methods)
                    {
                        string parameters = string.Join(", ",
                            method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                        sb.AppendLine($"        • {method.ReturnType.Name} {method.Name}({parameters})");
                    }
                }

                sb.AppendLine(new string('-', 50));
            }

            // Збереження результату у файл
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            sfd.Title = "Зберегти інформацію про класи";
            sfd.FileName = "classes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, sb.ToString());
                setStatus($"Інформацію про класи збережено у файл '{sfd.FileName}'");
            }
        }
#endregion
    
}
}
