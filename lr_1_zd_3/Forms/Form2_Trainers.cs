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
    public partial class Form2_Trainers : Form
    {
        public Form2_Trainers()
        {
            InitializeComponent();
        }
        private Coach[] coaches = new Coach[100];
        private int coachesCount = 0;


        private void Form2_Trainers_Load(object sender, EventArgs e)
        {

            comboBox1_sportSelect.Items.Clear();
            comboBox1_sportSelect.Items.Add("Веслування");
            comboBox1_sportSelect.Items.Add("Велоспорт");

            comboBox_RankCoach.Items.Clear();
            comboBox_RankCoach.Items.Add("Молодший тренер");
            comboBox_RankCoach.Items.Add("Старший тренер");
            comboBox_RankCoach.Items.Add("Заслужений тренер");

            comboBox1_sportSelect.SelectedIndex = 0;
            comboBox_RankCoach.SelectedIndex = 0;

            CreateInputGrid();
            CreateOutputGrid();

            // Підключення кнопок
            toolStripButton1.Click += toolStripButton1_Click;
            toolStripButton2.Click += toolStripButton2_Click;
            toolStripButton3.Click += toolStripButton3_Click;

            toolStripStatusLabel1.Text = "Готово до роботи";
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

            for (int i = 0; i < coachesCount; i++)
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

        private void RefreshOutputGrid()
        {
            dataGridView2_Output.Rows.Clear();

            if (coachesCount == 0)
            {
                dataGridView2_Output.Columns.Clear();
                return;
            }

            if (dataGridView2_Output.Columns.Count == 0)
            {
                CreateOutputGrid();
            }

            for (int i = 0; i < coachesCount; i++)
            {

                dataGridView2_Output.Rows.Add(
                    i,
                    coaches[i].Surname,
                    coaches[i].Name,
                    coaches[i].Pobatkovi,
                    coaches[i].Sport,
                    coaches[i].CoachRank,
                    coaches[i].BirthDate
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

            // Перевірка вибору виду спорту
            if (comboBox1_sportSelect.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Виберіть вид спорту!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Перевірка звання
            if (comboBox_RankCoach.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Виберіть звання тренера!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // Перевірка кількості
            if (coachesCount >= coaches.Length)
            {
                MessageBox.Show(
                    "Досягнуто максимальну кількість тренерів!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

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
            coaches[coachesCount] = coach;
            coachesCount++;

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

            if (index < 0 || index >= coachesCount)
                return;

            DialogResult result = MessageBox.Show(
                "Ви дійсно хочете видалити цього тренера?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Зсуваємо елементи масиву
                for (int i = index; i < coachesCount - 1; i++)
                {
                    coaches[i] = coaches[i + 1];
                }

                coaches[coachesCount - 1] = null;
                coachesCount--;

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

            if (index < 0 || index >= coachesCount)
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
            // Використовуємо той самий інтерфейс
            ISortableBySurname sorter = new Coach();
            // Сортуємо масив тренерів
            sorter.SortBySurname(coaches, coachesCount);

            /*            for (int i = 0; i < coachesCount - 1; i++)
                        {
                            for (int j = 0; j < coachesCount - i - 1; j++)
                            {
                                if (string.Compare(
                                    coaches[j].Surname,
                                    coaches[j + 1].Surname) > 0)
                                {
                                    Coach temp = coaches[j];
                                    coaches[j] = coaches[j + 1];
                                    coaches[j + 1] = temp;
                                }
                            }
                        }*/

            RefreshOutputGrid();
        }

        private void читанняЗФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter =
                "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";

            openDialog.Title = "Відкрити файл з тренерами";

            if (openDialog.ShowDialog() != DialogResult.OK)
                return;

            coachesCount = 0;

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

                    coaches[coachesCount] = coach;
                    coachesCount++;
                }
            }

            RefreshInputGrid();

            toolStripStatusLabel1.Text = $"Дані завантажено з файлу `{openDialog.SafeFileName}`";
        }

        private void записУФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            saveDialog.Title = "Зберегти дані тренерів";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
            {
                for (int i = 0; i < coachesCount; i++)
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

            toolStripStatusLabel1.Text = $"Дані записано у файл `{saveDialog.FileName}`";
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

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1_Input.CurrentRow == null)
            {
                MessageBox.Show("Оберіть тренера з таблиці!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int index = dataGridView1_Input.CurrentRow.Index;
            if (index >= 0 && index < coachesCount)
            {

                coaches[index].ShowInfo();
            }
        }
    }

}


