using lr_1_zd_3.Class;
using System;
using System.IO;
using System.Windows.Forms;

namespace lr_1_zd_3.Forms
{
    public partial class FormGroups : Form
    {
        // 1. Колекція груп та лічильник
        private SportGroup[] groups = new SportGroup[50];
        private int groupsCount = 0;

        // 2. Колекція тренерів для вибору куратора
        private Coach[] availableCoaches = new Coach[50];
        private int coachesCount = 0;

        // 3. Колекція доступних спортсменів для додавання в групи
        private Sportsman[] availableSportsmen = new Sportsman[100];
        private int sportsmenCount = 0;

        public FormGroups()
        {
            InitializeComponent();
        }

        private void FormGroups_Load(object sender, EventArgs e)
        {
            // Налаштування списку видів спорту
            comboBox_Sport.Items.Clear();
            comboBox_Sport.Items.Add("Веслування");
            comboBox_Sport.Items.Add("Велоспорт");
            comboBox_Sport.SelectedIndex = 0;

            numericUpDown_MaxMembers.Value = 20;

            // Налаштування випадаючих списків
            UpdateCoachesComboBox();
            UpdateSportsmenComboBox();

            // При завантаженні форми таблиці повинні бути ПУСТИМИ
            dataGridView_Groups.Columns.Clear();
            dataGridView_Members.Columns.Clear();

            toolStripStatusLabel1.Text = "Готово до роботи зі спортивними групами";
        }

        // --- ІНІЦІАЛІЗАЦІЯ СТРУКТУРИ ТАБЛИЦЬ ---

        private void CreateGroupsGrid()
        {
            dataGridView_Groups.Columns.Clear();
            dataGridView_Groups.Columns.Add("Index", "№");
            dataGridView_Groups.Columns.Add("GroupName", "Назва групи");
            dataGridView_Groups.Columns.Add("Sport", "Вид спорту");
            dataGridView_Groups.Columns.Add("Members", "Учасників");
            dataGridView_Groups.Columns.Add("Curator", "Куратор (Асоціація)");

            dataGridView_Groups.ReadOnly = true;
            dataGridView_Groups.AllowUserToAddRows = false;
            dataGridView_Groups.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_Groups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Groups.MultiSelect = false;
        }

        private void CreateMembersGrid()
        {
            dataGridView_Members.Columns.Clear();
            dataGridView_Members.Columns.Add("Index", "№");
            dataGridView_Members.Columns.Add("Surname", "Прізвище");
            dataGridView_Members.Columns.Add("Name", "Ім'я");
            dataGridView_Members.Columns.Add("Pobatkovi", "По батькові");
            dataGridView_Members.Columns.Add("Sport", "Вид спорту");
            dataGridView_Members.Columns.Add("Rank", "Спортивний розряд");

            dataGridView_Members.ReadOnly = true;
            dataGridView_Members.AllowUserToAddRows = false;
            dataGridView_Members.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_Members.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Members.MultiSelect = false;
        }

        // --- ОНОВЛЕННЯ ТАБЛИЦЬ ---

        private void RefreshGroupsGrid()
        {
            int savedIndex = dataGridView_Groups.CurrentRow != null ? dataGridView_Groups.CurrentRow.Index : -1;

            dataGridView_Groups.Rows.Clear();

            if (groupsCount == 0)
            {
                dataGridView_Groups.Columns.Clear();
                RefreshMembersGrid();
                return;
            }

            if (dataGridView_Groups.Columns.Count == 0)
            {
                CreateGroupsGrid();
            }

            for (int i = 0; i < groupsCount; i++)
            {
                dataGridView_Groups.Rows.Add(
                    i,
                    groups[i].GroupName,
                    groups[i].Sport,
                    $"{groups[i].MembersCount} / {groups[i].MaxMembers}",
                    groups[i].CuratorCoach != null
                        ? $"{groups[i].CuratorCoach.Surname} {groups[i].CuratorCoach.Name}"
                        : "Не призначено"
                );
            }

            // Відновлення виділення рядка
            if (savedIndex >= 0 && savedIndex < groupsCount)
            {
                dataGridView_Groups.Rows[savedIndex].Selected = true;
                dataGridView_Groups.CurrentCell = dataGridView_Groups.Rows[savedIndex].Cells[0];
            }
            else if (groupsCount > 0)
            {
                dataGridView_Groups.Rows[groupsCount - 1].Selected = true;
                dataGridView_Groups.CurrentCell = dataGridView_Groups.Rows[groupsCount - 1].Cells[0];
            }

            RefreshMembersGrid();
        }

        private void RefreshMembersGrid()
        {
            dataGridView_Members.Rows.Clear();

            if (dataGridView_Groups.CurrentRow == null ||
                dataGridView_Groups.CurrentRow.Index < 0 ||
                dataGridView_Groups.CurrentRow.Index >= groupsCount)
            {
                dataGridView_Members.Columns.Clear();
                groupBox_Members.Text = "Склад обраної групи";
                return;
            }

            int groupIndex = dataGridView_Groups.CurrentRow.Index;
            groupBox_Members.Text = $"Склад обраної групи: '{groups[groupIndex].GroupName}' ({groups[groupIndex].MembersCount} з {groups[groupIndex].MaxMembers} осіб)";

            if (groups[groupIndex].MembersCount == 0)
            {
                dataGridView_Members.Columns.Clear();
                return;
            }

            if (dataGridView_Members.Columns.Count == 0)
            {
                CreateMembersGrid();
            }

            for (int i = 0; i < groups[groupIndex].MembersCount; i++)
            {
                if (groups[groupIndex].Members[i] != null)
                {
                    dataGridView_Members.Rows.Add(
                        i,
                        groups[groupIndex].Members[i].Surname,
                        groups[groupIndex].Members[i].Name,
                        groups[groupIndex].Members[i].Pobatkovi,
                        groups[groupIndex].Members[i].Sport,
                        string.IsNullOrWhiteSpace(groups[groupIndex].Members[i].SportRank) ? "Без розряду" : groups[groupIndex].Members[i].SportRank
                    );
                }
            }
        }

        private void dataGridView_Groups_SelectionChanged(object sender, EventArgs e)
        {
            RefreshMembersGrid();
        }

        // --- ОНОВЛЕННЯ СПИСКІВ COMBOBOX ---

        private void UpdateCoachesComboBox()
        {
            comboBox_Coach.Items.Clear();
            comboBox_Coach.Items.Add("Без куратора");

            for (int i = 0; i < coachesCount; i++)
            {
                comboBox_Coach.Items.Add($"{availableCoaches[i].Surname} {availableCoaches[i].Name} ({availableCoaches[i].Sport})");
            }

            comboBox_Coach.SelectedIndex = 0;
        }

        private void UpdateSportsmenComboBox()
        {
            comboBox_Sportsman.Items.Clear();
            comboBox_Sportsman.Items.Add("--- Оберіть зі списку або введіть ПІБ ---");

            for (int i = 0; i < sportsmenCount; i++)
            {
                string rank = string.IsNullOrWhiteSpace(availableSportsmen[i].SportRank) ? "Без розряду" : availableSportsmen[i].SportRank;
                comboBox_Sportsman.Items.Add($"{availableSportsmen[i].Surname} {availableSportsmen[i].Name} ({availableSportsmen[i].Sport} - {rank})");
            }

            comboBox_Sportsman.SelectedIndex = 0;
        }

        // --- ДІЇ TOOLSTRIP: ДОДАВАННЯ ТА ВИДАЛЕННЯ ГРУПИ ---

        private void toolStripButton_AddGroup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_GroupName.Text))
            {
                MessageBox.Show("Введіть назву спортивної групи!", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (groupsCount >= groups.Length)
            {
                MessageBox.Show("Досягнуто максимальну кількість груп (50)!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Пряме створення об'єкта в масиві без створення проміжних змінних
            groups[groupsCount] = new SportGroup();
            groups[groupsCount].GroupName = textBox_GroupName.Text.Trim();
            groups[groupsCount].Sport = comboBox_Sport.Text;
            groups[groupsCount].MaxMembers = (int)numericUpDown_MaxMembers.Value;

            if (comboBox_Coach.SelectedIndex > 0 && (comboBox_Coach.SelectedIndex - 1) < coachesCount)
            {
                groups[groupsCount].CuratorCoach = availableCoaches[comboBox_Coach.SelectedIndex - 1];
            }
            else
            {
                groups[groupsCount].CuratorCoach = null;
            }

            groupsCount++;

            RefreshGroupsGrid();

            string coachInfo = groups[groupsCount - 1].CuratorCoach != null
                ? $"{groups[groupsCount - 1].CuratorCoach.Surname} {groups[groupsCount - 1].CuratorCoach.Name}"
                : "без куратора";

            toolStripStatusLabel1.Text = $"Групу '{groups[groupsCount - 1].GroupName}' успішно додано ({coachInfo})";

            textBox_GroupName.Clear();
            comboBox_Coach.SelectedIndex = 0;
        }

        private void toolStripButton_DeleteGroup_Click(object sender, EventArgs e)
        {
            if (dataGridView_Groups.CurrentRow == null ||
                dataGridView_Groups.CurrentRow.Index < 0 ||
                dataGridView_Groups.CurrentRow.Index >= groupsCount)
            {
                MessageBox.Show("Будь ласка, виберіть групу для видалення у таблиці!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int index = dataGridView_Groups.CurrentRow.Index;

            DialogResult res = MessageBox.Show(
                $"Ви дійсно бажаєте видалити групу '{groups[index].GroupName}'?\n\n(Зверніть увагу: тренер-куратор та спортсмени не видаляються, оскільки це зв'язки асоціації та агрегації)",
                "Підтвердження видалення",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (res != DialogResult.Yes) return;

            string deletedName = groups[index].GroupName;

            for (int i = index; i < groupsCount - 1; i++)
            {
                groups[i] = groups[i + 1];
            }
            groups[groupsCount - 1] = null!;
            groupsCount--;

            RefreshGroupsGrid();
            toolStripStatusLabel1.Text = $"Групу '{deletedName}' успішно видалено!";
        }

        // --- ДІЇ TOOLSTRIP: УПРАВЛІННЯ СКЛАДОМ ГРУПИ ---

        private void toolStripButton_AddMember_Click(object sender, EventArgs e)
        {
            if (dataGridView_Groups.CurrentRow == null ||
                dataGridView_Groups.CurrentRow.Index < 0 ||
                dataGridView_Groups.CurrentRow.Index >= groupsCount)
            {
                MessageBox.Show("Оберіть групу в таблиці вище, до якої потрібно додати спортсмена!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int groupIndex = dataGridView_Groups.CurrentRow.Index;

            if (groups[groupIndex].MembersCount >= groups[groupIndex].MaxMembers)
            {
                MessageBox.Show($"Група '{groups[groupIndex].GroupName}' уже заповнена! Ліміт: {groups[groupIndex].MaxMembers} осіб.", "Ліміт групи", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Вибір зі списку завантажених спортсменів
            if (comboBox_Sportsman.SelectedIndex > 0 && (comboBox_Sportsman.SelectedIndex - 1) < sportsmenCount)
            {
                bool added = groups[groupIndex].AddSportsman(availableSportsmen[comboBox_Sportsman.SelectedIndex - 1]);
                if (!added)
                {
                    MessageBox.Show("Цей спортсмен уже є в складі групи або досягнуто ліміту!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RefreshGroupsGrid();
                toolStripStatusLabel1.Text = $"Спортсмена успішно додано до групи '{groups[groupIndex].GroupName}'!";
            }
            // 2. Ручне введення ПІБ
            else if (!string.IsNullOrWhiteSpace(comboBox_Sportsman.Text) && comboBox_Sportsman.Text != "--- Оберіть зі списку або введіть ПІБ ---")
            {
                string[] parts = comboBox_Sportsman.Text.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                Sportsman newSportsman = new Sportsman();
                newSportsman.Surname = parts.Length > 0 ? parts[0] : "Спортсмен";
                newSportsman.Name = parts.Length > 1 ? parts[1] : "Новий";
                newSportsman.Pobatkovi = parts.Length > 2 ? parts[2] : "";
                newSportsman.Sport = groups[groupIndex].Sport;
                newSportsman.SportRank = "Без розряду";
                newSportsman.BirthDate = DateTime.Now.AddYears(-18);

                bool added = groups[groupIndex].AddSportsman(newSportsman);
                if (!added)
                {
                    MessageBox.Show($"Спортсмен {newSportsman.Surname} {newSportsman.Name} уже є в складі групи або досягнуто ліміту!", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RefreshGroupsGrid();
                toolStripStatusLabel1.Text = $"Спортсмена {newSportsman.Surname} {newSportsman.Name} успішно додано до групи '{groups[groupIndex].GroupName}'!";
            }
            else
            {
                MessageBox.Show("Оберіть спортсмена зі списку або введіть його Прізвище та Ім'я в поле вибору!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripButton_RemoveMember_Click(object sender, EventArgs e)
        {
            if (dataGridView_Groups.CurrentRow == null ||
                dataGridView_Groups.CurrentRow.Index < 0 ||
                dataGridView_Groups.CurrentRow.Index >= groupsCount)
            {
                MessageBox.Show("Оберіть групу в таблиці вище!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int groupIndex = dataGridView_Groups.CurrentRow.Index;

            if (dataGridView_Members.CurrentRow == null ||
                dataGridView_Members.CurrentRow.Index < 0 ||
                dataGridView_Members.CurrentRow.Index >= groups[groupIndex].MembersCount)
            {
                MessageBox.Show("Оберіть спортсмена в таблиці нижче для вилучення з групи!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int memberIndex = dataGridView_Members.CurrentRow.Index;
            string memberName = groups[groupIndex].Members[memberIndex] != null
                ? $"{groups[groupIndex].Members[memberIndex].Surname} {groups[groupIndex].Members[memberIndex].Name}"
                : "спортсмена";

            groups[groupIndex].RemoveSportsman(memberIndex);
            RefreshGroupsGrid();

            toolStripStatusLabel1.Text = $"Спортсмена {memberName} вилучено з групи '{groups[groupIndex].GroupName}'!";
        }

        // --- ДІЯ TOOLSTRIP: КАРТКА ГРУПИ (GETINFO) ---

        private void toolStripButton_Info_Click(object sender, EventArgs e)
        {
            if (dataGridView_Groups.CurrentRow == null ||
                dataGridView_Groups.CurrentRow.Index < 0 ||
                dataGridView_Groups.CurrentRow.Index >= groupsCount)
            {
                MessageBox.Show("Будь ласка, виберіть групу зі списку для перегляду картки!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int index = dataGridView_Groups.CurrentRow.Index;
            MessageBox.Show(groups[index].GetInfo(), $"Картка групи: {groups[index].GroupName}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- РОБОТА З ФАЙЛАМИ ---

        private void завантажитиТренерівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openFileDialog1.Title = "Відкрити файл з тренерами";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            coachesCount = 0;
            string[] lines = File.ReadAllLines(openFileDialog1.FileName);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(';');

                // Пряме заповнення елементів масиву availableCoaches без створення зайвих змінних
                if (parts.Length >= 6 && coachesCount < availableCoaches.Length)
                {
                    availableCoaches[coachesCount] = new Coach();
                    availableCoaches[coachesCount].Surname = parts[0];
                    availableCoaches[coachesCount].Name = parts[1];
                    availableCoaches[coachesCount].Pobatkovi = parts[2];
                    availableCoaches[coachesCount].Sport = parts[3];
                    availableCoaches[coachesCount].CoachRank = parts[4];
                    availableCoaches[coachesCount].BirthDate = DateTime.Parse(parts[5]);
                    coachesCount++;
                }
            }

            UpdateCoachesComboBox();
            toolStripStatusLabel1.Text = $"Завантажено {coachesCount} тренерів з файлу '{Path.GetFileName(openFileDialog1.FileName)}'";
            MessageBox.Show($"Успішно завантажено {coachesCount} тренерів для призначення кураторами!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void завантажитиСпортсменівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openFileDialog1.Title = "Відкрити файл зі спортсменами";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            sportsmenCount = 0;
            string[] lines = File.ReadAllLines(openFileDialog1.FileName);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(';');

                // Пряме заповнення елементів масиву availableSportsmen без створення зайвих змінних
                if (parts.Length >= 7 && sportsmenCount < availableSportsmen.Length)
                {
                    availableSportsmen[sportsmenCount] = new Sportsman();
                    availableSportsmen[sportsmenCount].Surname = parts[0];
                    availableSportsmen[sportsmenCount].Name = parts[1];
                    availableSportsmen[sportsmenCount].Pobatkovi = parts[2];
                    availableSportsmen[sportsmenCount].Sport = parts[3];
                    availableSportsmen[sportsmenCount].SportRank = parts[4];
                    availableSportsmen[sportsmenCount].BirthDate = DateTime.Parse(parts[5]);
                    availableSportsmen[sportsmenCount].PrizePlaces = int.Parse(parts[6]);
                    sportsmenCount++;
                }
            }

            UpdateSportsmenComboBox();
            toolStripStatusLabel1.Text = $"Завантажено {sportsmenCount} спортсменів з файлу '{Path.GetFileName(openFileDialog1.FileName)}'";
            MessageBox.Show($"Успішно завантажено {sportsmenCount} спортсменів! Тепер їх можна додавати до груп.", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void записУФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (groupsCount == 0)
            {
                MessageBox.Show("Немає створених груп для збереження!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            saveFileDialog1.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            saveFileDialog1.Title = "Зберегти групи у файл";
            saveFileDialog1.FileName = "groups.txt";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
            {
                for (int i = 0; i < groupsCount; i++)
                {
                    string curatorSurname = groups[i].CuratorCoach != null ? groups[i].CuratorCoach!.Surname : "None";
                    string curatorName = groups[i].CuratorCoach != null ? groups[i].CuratorCoach!.Name : "None";

                    string membersData = "None";
                    if (groups[i].MembersCount > 0)
                    {
                        string[] membersList = new string[groups[i].MembersCount];
                        for (int m = 0; m < groups[i].MembersCount; m++)
                        {
                            membersList[m] = $"{groups[i].Members[m].Surname}:{groups[i].Members[m].Name}:{groups[i].Members[m].Pobatkovi}:{groups[i].Members[m].Sport}:{groups[i].Members[m].SportRank}";
                        }
                        membersData = string.Join(",", membersList);
                    }

                    writer.WriteLine(
                        $"{groups[i].GroupName};" +
                        $"{groups[i].Sport};" +
                        $"{groups[i].MaxMembers};" +
                        $"{curatorSurname};" +
                        $"{curatorName};" +
                        $"{membersData}"
                    );
                }
            }

            toolStripStatusLabel1.Text = $"Дані груп збережено у файл '{Path.GetFileName(saveFileDialog1.FileName)}'";
            MessageBox.Show("Дані спортивних груп успішно збережено у файл!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void читанняЗФайлуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Текстові файли (*.txt)|*.txt|Усі файли (*.*)|*.*";
            openFileDialog1.Title = "Відкрити файл зі спортивними групами";

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            groupsCount = 0;
            string[] lines = File.ReadAllLines(openFileDialog1.FileName);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split(';');

                if (parts.Length >= 5 && groupsCount < groups.Length)
                {
                    // Пряме заповнення елемента масиву groups[groupsCount]
                    groups[groupsCount] = new SportGroup();
                    groups[groupsCount].GroupName = parts[0];
                    groups[groupsCount].Sport = parts[1];
                    groups[groupsCount].MaxMembers = int.Parse(parts[2]);
                    groups[groupsCount].CuratorCoach = null;

                    if (parts[3] != "None")
                    {
                        for (int c = 0; c < coachesCount; c++)
                        {
                            if (availableCoaches[c].Surname == parts[3] && availableCoaches[c].Name == parts[4])
                            {
                                groups[groupsCount].CuratorCoach = availableCoaches[c];
                                break;
                            }
                        }

                        if (groups[groupsCount].CuratorCoach == null)
                        {
                            Coach newCoach = new Coach();
                            newCoach.Surname = parts[3];
                            newCoach.Name = parts[4];
                            newCoach.Sport = parts[1];
                            groups[groupsCount].CuratorCoach = newCoach;
                        }
                    }

                    if (parts.Length >= 6 && parts[5] != "None" && !string.IsNullOrWhiteSpace(parts[5]))
                    {
                        string[] memberTokens = parts[5].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string token in memberTokens)
                        {
                            string[] smParts = token.Split(':');
                            if (smParts.Length >= 2)
                            {
                                Sportsman sm = new Sportsman();
                                sm.Surname = smParts[0];
                                sm.Name = smParts[1];
                                sm.Pobatkovi = smParts.Length > 2 ? smParts[2] : "";
                                sm.Sport = smParts.Length > 3 ? smParts[3] : parts[1];
                                sm.SportRank = smParts.Length > 4 ? smParts[4] : "Без розряду";
                                groups[groupsCount].AddSportsman(sm);
                            }
                        }
                    }

                    groupsCount++;
                }
            }

            RefreshGroupsGrid();
            toolStripStatusLabel1.Text = $"Дані груп завантажено з файлу '{Path.GetFileName(openFileDialog1.FileName)}' ({groupsCount} груп)";
            MessageBox.Show($"Успішно завантажено {groupsCount} груп!", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- НАВІГАЦІЯ МІЖ ФОРМАМИ ---

        private void спортсмениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
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
            this.Hide();
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
