using System.Xml.Linq;

namespace lr_1_zd_3
{
    public partial class Form1 : Form
    {
        private Sportsman[] sportsmen = new Sportsman[100];
        private int sportsmenCount = 0;
        private void Form1_Load(object sender, EventArgs e)
        {;
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
            toolStripStatusLabel1.Text = "Готово";
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


            dataGridView2_Output.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2_Output.ReadOnly = true;
            dataGridView2_Output.AllowUserToAddRows = false;
        }
        private void RefreshInputGrid()
        {
            if (sportsmenCount == 0)
            {
                dataGridView1_Input.Columns.Clear();
                return;
            }
            if (dataGridView1_Input.Columns.Count == 0)
            {
                CreateInputGrid();
            }
            dataGridView1_Input.Rows.Clear();
            for (int i = 0; i < sportsmenCount; i++)
            {
                
                dataGridView1_Input.Rows.Add(
                    i,
                    sportsmen[i].Surname,
                    sportsmen[i].Name,
                    sportsmen[i].Pobatkovi,
                    sportsmen[i].Sport,
                    sportsmen[i].SportRank,
                    sportsmen[i].BirthDate,
                    sportsmen[i].PrizePlaces
                );
            }

            toolStripStatusLabel1.Text =
                $"Кількість спортсменів: {sportsmenCount}";
        }
        private void RefreshOutputGrid()
        {
            dataGridView2_Output.Rows.Clear();

            if (sportsmenCount == 0)
            {
                dataGridView2_Output.Columns.Clear();
                return;
            }

            if (dataGridView2_Output.Columns.Count == 0)
            {
                CreateOutputGrid();
            }

            for (int i = 0; i < sportsmenCount; i++)
            {

                dataGridView2_Output.Rows.Add(
                    i,
                    sportsmen[i].Surname,
                    sportsmen[i].Name,
                    sportsmen[i].Sport,
                    sportsmen[i].SportRank,
                    sportsmen[i].BirthDate,
                    sportsmen[i].PrizePlaces
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
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_inputNameSportsmen.Text) ||
                string.IsNullOrWhiteSpace(textBox_inputSurnameSportsmen.Text) ||
                string.IsNullOrWhiteSpace(textBox_inputPobatkoviSportsmen.Text))
            {
                MessageBox.Show(
                    "Заповніть ім'я, прізвище та по батькові!",
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            Sportsman newSportsman = new Sportsman(
                textBox_inputNameSportsmen.Text,
                textBox_inputSurnameSportsmen.Text,
                textBox_inputPobatkoviSportsmen.Text,
                comboBox1_sportSelect.Text,
                comboBox2.Text,
                dateTimePicker1_sportsmenBirthday.Value,
                (int)numericUpDown1_amountOfMedals.Value
            );

            sportsmen[sportsmenCount] = newSportsman;
            sportsmenCount++;

            RefreshInputGrid();

            ClearInputFields();

            toolStripStatusLabel1.Text =
                "Спортсмен успішно доданий";
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

            if (index >= 0 && index < sportsmenCount)
            {
                DialogResult result = MessageBox.Show(
                    "Ви дійсно хочете видалити цього спортсмена?",
                    "Підтвердження",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Зсуваємо всі наступні елементи на одну позицію вліво
                    for (int i = index; i < sportsmenCount - 1; i++)
                    {
                        sportsmen[i] = sportsmen[i + 1];
                    }

                    sportsmen[sportsmenCount - 1] = null;
                    sportsmenCount--;

                    RefreshInputGrid();

                    toolStripStatusLabel1.Text =
                        "Спортсмен видалений";
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!inputControl())
                return;

            int rowIndex = dataGridView1_Input.CurrentCell.RowIndex;
            int columnIndex = dataGridView1_Input.CurrentCell.ColumnIndex;
            if (rowIndex < 0 || rowIndex >= sportsmenCount)
                return;

            Sportsman sportsman = sportsmen[rowIndex];
            switch (columnIndex)
            {
                case 1: // Прізвище
                    if (!string.IsNullOrWhiteSpace(textBox_inputSurnameSportsmen.Text))
                    {
                        sportsman.Surname =
                            textBox_inputSurnameSportsmen.Text;
                    }
                    break;

                case 2: // Ім'я
                    if (!string.IsNullOrWhiteSpace(textBox_inputNameSportsmen.Text))
                    {
                        sportsman.Name =
                            textBox_inputNameSportsmen.Text;
                    }
                    break;

                case 3: // По батькові
                    if (!string.IsNullOrWhiteSpace(textBox_inputPobatkoviSportsmen.Text))
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

                case 0: // Індекс
                    MessageBox.Show(
                        "Індекс змінювати не можна!",
                        "Увага",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
            }

            RefreshInputGrid();

            toolStripStatusLabel1.Text =
                "Вибране поле успішно змінено";
        }

        private void записУФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            saveDialog.Title = "Зберегти дані спортсменів";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
            {
                for (int i = 0; i < sportsmenCount; i++)
                {


                    writer.WriteLine(
                        $"{sportsmen[i].Surname};" +
                        $"{sportsmen[i].Name};" +
                        $"{sportsmen[i].Pobatkovi};" +
                        $"{sportsmen[i].Sport};" +
                        $"{sportsmen[i].SportRank};" +
                        $"{sportsmen[i].BirthDate};" +
                        $"{sportsmen[i].PrizePlaces}"
                    );
                }
            }

            toolStripStatusLabel1.Text = "Дані записано у файл";
        }

        private void читанняЗФайлToolStripMenuItem_Click(object sender, EventArgs e)
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
                string[] parts = line.Split(';');
                if (parts.Length == 7)
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
                    sportsmen[sportsmenCount] = sportsman;
                    sportsmenCount++;
                }
            }

            RefreshInputGrid();

            toolStripStatusLabel1.Text = "Дані завантажено з файлу";
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

        private void заПрізвищемToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sportsmenCount - 1; i++)
            {
                for (int j = 0; j < sportsmenCount - i - 1; j++)
                {
                    if (string.Compare(
                        sportsmen[j].Surname,
                        sportsmen[j + 1].Surname) > 0)
                    {
                        Sportsman temp = sportsmen[j];
                        sportsmen[j] = sportsmen[j + 1];
                        sportsmen[j + 1] = temp;
                    }
                }
            }
            ShowSortedSportsmen();
        }
        private void ShowSortedSportsmen()
        {
            dataGridView2_Output.Rows.Clear();

            if (sportsmenCount == 0)
            {
                dataGridView2_Output.Columns.Clear();
                return;
            }

            if (dataGridView2_Output.Columns.Count == 0)
            {
                CreateOutputGrid();
            }
            for (int i = 0; i < sportsmenCount; i++)
            {
                Sportsman sportsman = sportsmen[i];

                dataGridView2_Output.Rows.Add(
                    i,
                    sportsman.Surname,
                    sportsman.Name,
                    sportsman.Sport,
                    sportsman.SportRank,
                    sportsman.BirthDate,
                    sportsman.PrizePlaces
                );
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!inputControl())
                return;


            int index = dataGridView1_Input.CurrentRow.Index;
            if (index >= 0 && index < sportsmenCount)
            {
                sportsmen[index]++;
                RefreshOutputGrid();
                toolStripStatusLabel1.Text = "Кількість призових місць спортсмена збільшено на 1";

            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (!inputControl())
                return;


            int index = dataGridView1_Input.CurrentRow.Index;
            if (index >= 0 && index < sportsmenCount)
            {
                sportsmen[index]--;
                RefreshOutputGrid();
                toolStripStatusLabel1.Text = "Кількість призових місць спортсмена зменшено на 1";

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
            for (int i = 0; i < sportsmenCount - 1; i++)
            {
                for (int j = 0; j < sportsmenCount - i - 1; j++)
                {
                    if (sportsmen[j] > sportsmen[j + 1])
                    {
                        Sportsman temp = sportsmen[j];
                        sportsmen[j] = sportsmen[j + 1];
                        sportsmen[j + 1] = temp;
                    }
                }
            }

            ShowSortedSportsmen();
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
    }
}
