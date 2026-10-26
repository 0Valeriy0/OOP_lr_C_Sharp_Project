namespace lr_1_zd_3.Forms
{
    partial class FormGroups
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            менюToolStripMenuItem = new ToolStripMenuItem();
            спортсмениToolStripMenuItem = new ToolStripMenuItem();
            тренериToolStripMenuItem = new ToolStripMenuItem();
            тренуванняToolStripMenuItem = new ToolStripMenuItem();
            вихідToolStripMenuItem = new ToolStripMenuItem();
            роботаЗФайломToolStripMenuItem = new ToolStripMenuItem();
            завантажитиТренерівToolStripMenuItem = new ToolStripMenuItem();
            завантажитиСпортсменівToolStripMenuItem = new ToolStripMenuItem();
            записУФайлToolStripMenuItem = new ToolStripMenuItem();
            читанняЗФайлуToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolStripButton_AddGroup = new ToolStripButton();
            toolStripButton_DeleteGroup = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButton_AddMember = new ToolStripButton();
            toolStripButton_RemoveMember = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripButton_Info = new ToolStripButton();
            groupBox_GroupInfo = new GroupBox();
            comboBox_Coach = new ComboBox();
            label4 = new Label();
            numericUpDown_MaxMembers = new NumericUpDown();
            label3 = new Label();
            comboBox_Sport = new ComboBox();
            label2 = new Label();
            textBox_GroupName = new TextBox();
            label1 = new Label();
            label_Title = new Label();
            label_GridTitle = new Label();
            dataGridView_Groups = new DataGridView();
            groupBox_Members = new GroupBox();
            dataGridView_Members = new DataGridView();
            comboBox_Sportsman = new ComboBox();
            label_SelectSportsman = new Label();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            groupBox_GroupInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_MaxMembers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Groups).BeginInit();
            groupBox_Members.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Members).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 10.2F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { менюToolStripMenuItem, роботаЗФайломToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1924, 31);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            менюToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { спортсмениToolStripMenuItem, тренериToolStripMenuItem, тренуванняToolStripMenuItem, вихідToolStripMenuItem });
            менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            менюToolStripMenuItem.Size = new Size(67, 27);
            менюToolStripMenuItem.Text = "Меню";
            // 
            // спортсмениToolStripMenuItem
            // 
            спортсмениToolStripMenuItem.Name = "спортсмениToolStripMenuItem";
            спортсмениToolStripMenuItem.Size = new Size(189, 28);
            спортсмениToolStripMenuItem.Text = "Спортсмени";
            спортсмениToolStripMenuItem.Click += спортсмениToolStripMenuItem_Click;
            // 
            // тренериToolStripMenuItem
            // 
            тренериToolStripMenuItem.Name = "тренериToolStripMenuItem";
            тренериToolStripMenuItem.Size = new Size(189, 28);
            тренериToolStripMenuItem.Text = "Тренери";
            тренериToolStripMenuItem.Click += тренериToolStripMenuItem_Click;
            // 
            // тренуванняToolStripMenuItem
            // 
            тренуванняToolStripMenuItem.Name = "тренуванняToolStripMenuItem";
            тренуванняToolStripMenuItem.Size = new Size(189, 28);
            тренуванняToolStripMenuItem.Text = "Тренування";
            тренуванняToolStripMenuItem.Click += тренуванняToolStripMenuItem_Click;
            // 
            // вихідToolStripMenuItem
            // 
            вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            вихідToolStripMenuItem.Size = new Size(189, 28);
            вихідToolStripMenuItem.Text = "Вихід";
            вихідToolStripMenuItem.Click += вихідToolStripMenuItem_Click;
            // 
            // роботаЗФайломToolStripMenuItem
            // 
            роботаЗФайломToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { завантажитиТренерівToolStripMenuItem, завантажитиСпортсменівToolStripMenuItem, записУФайлToolStripMenuItem, читанняЗФайлуToolStripMenuItem });
            роботаЗФайломToolStripMenuItem.Name = "роботаЗФайломToolStripMenuItem";
            роботаЗФайломToolStripMenuItem.Size = new Size(157, 27);
            роботаЗФайломToolStripMenuItem.Text = "Робота з файлом";
            // 
            // завантажитиТренерівToolStripMenuItem
            // 
            завантажитиТренерівToolStripMenuItem.Name = "завантажитиТренерівToolStripMenuItem";
            завантажитиТренерівToolStripMenuItem.Size = new Size(286, 28);
            завантажитиТренерівToolStripMenuItem.Text = "Завантажити тренерів";
            завантажитиТренерівToolStripMenuItem.Click += завантажитиТренерівToolStripMenuItem_Click;
            // 
            // завантажитиСпортсменівToolStripMenuItem
            // 
            завантажитиСпортсменівToolStripMenuItem.Name = "завантажитиСпортсменівToolStripMenuItem";
            завантажитиСпортсменівToolStripMenuItem.Size = new Size(286, 28);
            завантажитиСпортсменівToolStripMenuItem.Text = "Завантажити спортсменів";
            завантажитиСпортсменівToolStripMenuItem.Click += завантажитиСпортсменівToolStripMenuItem_Click;
            // 
            // записУФайлToolStripMenuItem
            // 
            записУФайлToolStripMenuItem.Name = "записУФайлToolStripMenuItem";
            записУФайлToolStripMenuItem.Size = new Size(286, 28);
            записУФайлToolStripMenuItem.Text = "Записати групи у файл";
            записУФайлToolStripMenuItem.Click += записУФайлToolStripMenuItem_Click;
            // 
            // читанняЗФайлуToolStripMenuItem
            // 
            читанняЗФайлуToolStripMenuItem.Name = "читанняЗФайлуToolStripMenuItem";
            читанняЗФайлуToolStripMenuItem.Size = new Size(286, 28);
            читанняЗФайлуToolStripMenuItem.Text = "Прочитати групи з файлу";
            читанняЗФайлуToolStripMenuItem.Click += читанняЗФайлуToolStripMenuItem_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Font = new Font("Segoe UI", 10.2F);
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton_AddGroup, toolStripButton_DeleteGroup, toolStripSeparator1, toolStripButton_AddMember, toolStripButton_RemoveMember, toolStripSeparator2, toolStripButton_Info });
            toolStrip1.Location = new Point(0, 31);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1924, 31);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_AddGroup
            // 
            toolStripButton_AddGroup.Image = Properties.Resources.plus_PNG84;
            toolStripButton_AddGroup.ImageTransparentColor = Color.Magenta;
            toolStripButton_AddGroup.Name = "toolStripButton_AddGroup";
            toolStripButton_AddGroup.Size = new Size(138, 28);
            toolStripButton_AddGroup.Text = "Додати групу";
            toolStripButton_AddGroup.Click += toolStripButton_AddGroup_Click;
            // 
            // toolStripButton_DeleteGroup
            // 
            toolStripButton_DeleteGroup.Image = Properties.Resources.png_transparent_cross_remove_delete_icocentre_icons_icon;
            toolStripButton_DeleteGroup.ImageTransparentColor = Color.Magenta;
            toolStripButton_DeleteGroup.Name = "toolStripButton_DeleteGroup";
            toolStripButton_DeleteGroup.Size = new Size(150, 28);
            toolStripButton_DeleteGroup.Text = "Видалити групу";
            toolStripButton_DeleteGroup.Click += toolStripButton_DeleteGroup_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // toolStripButton_AddMember
            // 
            toolStripButton_AddMember.Image = Properties.Resources.plus_PNG84;
            toolStripButton_AddMember.ImageTransparentColor = Color.Magenta;
            toolStripButton_AddMember.Name = "toolStripButton_AddMember";
            toolStripButton_AddMember.Size = new Size(251, 28);
            toolStripButton_AddMember.Text = "Додати спортсмена до групи";
            toolStripButton_AddMember.Click += toolStripButton_AddMember_Click;
            // 
            // toolStripButton_RemoveMember
            // 
            toolStripButton_RemoveMember.Image = Properties.Resources.png_transparent_cross_remove_delete_icocentre_icons_icon;
            toolStripButton_RemoveMember.ImageTransparentColor = Color.Magenta;
            toolStripButton_RemoveMember.Name = "toolStripButton_RemoveMember";
            toolStripButton_RemoveMember.Size = new Size(244, 28);
            toolStripButton_RemoveMember.Text = "Вилучити спортсмена з групи";
            toolStripButton_RemoveMember.Click += toolStripButton_RemoveMember_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 31);
            // 
            // toolStripButton_Info
            // 
            toolStripButton_Info.Image = Properties.Resources.writing_square_69719;
            toolStripButton_Info.ImageTransparentColor = Color.Magenta;
            toolStripButton_Info.Name = "toolStripButton_Info";
            toolStripButton_Info.Size = new Size(137, 28);
            toolStripButton_Info.Text = "Картка групи";
            toolStripButton_Info.Click += toolStripButton_Info_Click;
            // 
            // groupBox_GroupInfo
            // 
            groupBox_GroupInfo.Controls.Add(comboBox_Coach);
            groupBox_GroupInfo.Controls.Add(label4);
            groupBox_GroupInfo.Controls.Add(numericUpDown_MaxMembers);
            groupBox_GroupInfo.Controls.Add(label3);
            groupBox_GroupInfo.Controls.Add(comboBox_Sport);
            groupBox_GroupInfo.Controls.Add(label2);
            groupBox_GroupInfo.Controls.Add(textBox_GroupName);
            groupBox_GroupInfo.Controls.Add(label1);
            groupBox_GroupInfo.Controls.Add(label_Title);
            groupBox_GroupInfo.Font = new Font("Segoe UI", 11F);
            groupBox_GroupInfo.Location = new Point(12, 75);
            groupBox_GroupInfo.Name = "groupBox_GroupInfo";
            groupBox_GroupInfo.Size = new Size(660, 649);
            groupBox_GroupInfo.TabIndex = 2;
            groupBox_GroupInfo.TabStop = false;
            groupBox_GroupInfo.Text = "Параметри групи";
            // 
            // comboBox_Coach
            // 
            comboBox_Coach.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Coach.Font = new Font("Segoe UI", 12F);
            comboBox_Coach.FormattingEnabled = true;
            comboBox_Coach.Location = new Point(25, 450);
            comboBox_Coach.Name = "comboBox_Coach";
            comboBox_Coach.Size = new Size(610, 36);
            comboBox_Coach.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(25, 415);
            label4.Name = "label4";
            label4.Size = new Size(260, 28);
            label4.TabIndex = 7;
            label4.Text = "Тренер-куратор (Асоціація):";
            // 
            // numericUpDown_MaxMembers
            // 
            numericUpDown_MaxMembers.Font = new Font("Segoe UI", 12F);
            numericUpDown_MaxMembers.Location = new Point(25, 350);
            numericUpDown_MaxMembers.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown_MaxMembers.Name = "numericUpDown_MaxMembers";
            numericUpDown_MaxMembers.Size = new Size(610, 34);
            numericUpDown_MaxMembers.TabIndex = 6;
            numericUpDown_MaxMembers.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(25, 315);
            label3.Name = "label3";
            label3.Size = new Size(245, 28);
            label3.TabIndex = 5;
            label3.Text = "Макс. кількість учасників:";
            // 
            // comboBox_Sport
            // 
            comboBox_Sport.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_Sport.Font = new Font("Segoe UI", 12F);
            comboBox_Sport.FormattingEnabled = true;
            comboBox_Sport.Location = new Point(25, 250);
            comboBox_Sport.Name = "comboBox_Sport";
            comboBox_Sport.Size = new Size(610, 36);
            comboBox_Sport.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(25, 215);
            label2.Name = "label2";
            label2.Size = new Size(118, 28);
            label2.TabIndex = 3;
            label2.Text = "Вид спорту:";
            // 
            // textBox_GroupName
            // 
            textBox_GroupName.Font = new Font("Segoe UI", 12F);
            textBox_GroupName.Location = new Point(25, 150);
            textBox_GroupName.Name = "textBox_GroupName";
            textBox_GroupName.Size = new Size(610, 34);
            textBox_GroupName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(25, 115);
            label1.Name = "label1";
            label1.Size = new Size(129, 28);
            label1.TabIndex = 1;
            label1.Text = "Назва групи:";
            // 
            // label_Title
            // 
            label_Title.AutoSize = true;
            label_Title.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label_Title.ForeColor = Color.FromArgb(40, 70, 120);
            label_Title.Location = new Point(25, 45);
            label_Title.Name = "label_Title";
            label_Title.Size = new Size(357, 32);
            label_Title.TabIndex = 0;
            label_Title.Text = "Створення спортивної групи";
            // 
            // label_GridTitle
            // 
            label_GridTitle.AutoSize = true;
            label_GridTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label_GridTitle.ForeColor = Color.FromArgb(40, 70, 120);
            label_GridTitle.Location = new Point(695, 75);
            label_GridTitle.Name = "label_GridTitle";
            label_GridTitle.Size = new Size(309, 32);
            label_GridTitle.TabIndex = 3;
            label_GridTitle.Text = "Список сформованих груп";
            // 
            // dataGridView_Groups
            // 
            dataGridView_Groups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_Groups.Location = new Point(695, 115);
            dataGridView_Groups.Name = "dataGridView_Groups";
            dataGridView_Groups.RowHeadersWidth = 51;
            dataGridView_Groups.Size = new Size(1215, 275);
            dataGridView_Groups.TabIndex = 4;
            dataGridView_Groups.SelectionChanged += dataGridView_Groups_SelectionChanged;
            // 
            // groupBox_Members
            // 
            groupBox_Members.Controls.Add(dataGridView_Members);
            groupBox_Members.Controls.Add(comboBox_Sportsman);
            groupBox_Members.Controls.Add(label_SelectSportsman);
            groupBox_Members.Font = new Font("Segoe UI", 11F);
            groupBox_Members.Location = new Point(695, 405);
            groupBox_Members.Name = "groupBox_Members";
            groupBox_Members.Size = new Size(1215, 319);
            groupBox_Members.TabIndex = 5;
            groupBox_Members.TabStop = false;
            groupBox_Members.Text = "Склад обраної групи";
            // 
            // dataGridView_Members
            // 
            dataGridView_Members.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_Members.Location = new Point(15, 80);
            dataGridView_Members.Name = "dataGridView_Members";
            dataGridView_Members.RowHeadersWidth = 51;
            dataGridView_Members.Size = new Size(1185, 225);
            dataGridView_Members.TabIndex = 5;
            // 
            // comboBox_Sportsman
            // 
            comboBox_Sportsman.Font = new Font("Segoe UI", 11F);
            comboBox_Sportsman.FormattingEnabled = true;
            comboBox_Sportsman.Location = new Point(280, 32);
            comboBox_Sportsman.Name = "comboBox_Sportsman";
            comboBox_Sportsman.Size = new Size(920, 33);
            comboBox_Sportsman.TabIndex = 1;
            // 
            // label_SelectSportsman
            // 
            label_SelectSportsman.AutoSize = true;
            label_SelectSportsman.Font = new Font("Segoe UI", 11F);
            label_SelectSportsman.Location = new Point(15, 35);
            label_SelectSportsman.Name = "label_SelectSportsman";
            label_SelectSportsman.Size = new Size(259, 25);
            label_SelectSportsman.TabIndex = 0;
            label_SelectSportsman.Text = "Оберіть / введіть спортсмена:";
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Segoe UI", 10F);
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 744);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1924, 36);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(61, 30);
            toolStripStatusLabel1.Text = "Готово";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormGroups
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 780);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox_Members);
            Controls.Add(dataGridView_Groups);
            Controls.Add(label_GridTitle);
            Controls.Add(groupBox_GroupInfo);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormGroups";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Спортивні групи — Асоціація та колекції";
            Load += FormGroups_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            groupBox_GroupInfo.ResumeLayout(false);
            groupBox_GroupInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_MaxMembers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Groups).EndInit();
            groupBox_Members.ResumeLayout(false);
            groupBox_Members.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Members).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem менюToolStripMenuItem;
        private ToolStripMenuItem спортсмениToolStripMenuItem;
        private ToolStripMenuItem тренериToolStripMenuItem;
        private ToolStripMenuItem тренуванняToolStripMenuItem;
        private ToolStripMenuItem вихідToolStripMenuItem;
        private ToolStripMenuItem роботаЗФайломToolStripMenuItem;
        private ToolStripMenuItem завантажитиТренерівToolStripMenuItem;
        private ToolStripMenuItem завантажитиСпортсменівToolStripMenuItem;
        private ToolStripMenuItem записУФайлToolStripMenuItem;
        private ToolStripMenuItem читанняЗФайлуToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton_AddGroup;
        private ToolStripButton toolStripButton_DeleteGroup;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton_AddMember;
        private ToolStripButton toolStripButton_RemoveMember;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton_Info;
        private GroupBox groupBox_GroupInfo;
        private Label label_Title;
        private Label label1;
        private TextBox textBox_GroupName;
        private Label label2;
        private ComboBox comboBox_Sport;
        private Label label3;
        private NumericUpDown numericUpDown_MaxMembers;
        private Label label4;
        private ComboBox comboBox_Coach;
        private Label label_GridTitle;
        private DataGridView dataGridView_Groups;
        private GroupBox groupBox_Members;
        private Label label_SelectSportsman;
        private ComboBox comboBox_Sportsman;
        private DataGridView dataGridView_Members;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
    }
}
