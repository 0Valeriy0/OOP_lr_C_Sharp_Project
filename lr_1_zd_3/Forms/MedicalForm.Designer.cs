namespace lr_1_zd_3.Forms
{
    partial class MedicalForm
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
            groupBox2_sportRozrad = new GroupBox();
            checkBox1 = new CheckBox();
            label_SportsmanInfo = new Label();
            comboBox1_bloodGroup = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            label9 = new Label();
            label7 = new Label();
            label5 = new Label();
            label3 = new Label();
            toolStripButton1 = new ToolStripButton();
            toolStrip1 = new ToolStrip();
            groupBox2_sportRozrad.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2_sportRozrad
            // 
            groupBox2_sportRozrad.Controls.Add(checkBox1);
            groupBox2_sportRozrad.Controls.Add(label_SportsmanInfo);
            groupBox2_sportRozrad.Controls.Add(comboBox1_bloodGroup);
            groupBox2_sportRozrad.Controls.Add(dateTimePicker1);
            groupBox2_sportRozrad.Controls.Add(label9);
            groupBox2_sportRozrad.Controls.Add(label7);
            groupBox2_sportRozrad.Controls.Add(label5);
            groupBox2_sportRozrad.Controls.Add(label3);
            groupBox2_sportRozrad.Location = new Point(12, 30);
            groupBox2_sportRozrad.Name = "groupBox2_sportRozrad";
            groupBox2_sportRozrad.Size = new Size(595, 401);
            groupBox2_sportRozrad.TabIndex = 63;
            groupBox2_sportRozrad.TabStop = false;
            groupBox2_sportRozrad.Text = "Панель редагування даних";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 22.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 204);
            checkBox1.Location = new Point(503, 245);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(18, 17);
            checkBox1.TabIndex = 66;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label_SportsmanInfo
            // 
            label_SportsmanInfo.AutoSize = true;
            label_SportsmanInfo.Font = new Font("Segoe UI", 13.8F);
            label_SportsmanInfo.Location = new Point(233, 34);
            label_SportsmanInfo.Name = "label_SportsmanInfo";
            label_SportsmanInfo.Size = new Size(56, 31);
            label_SportsmanInfo.TabIndex = 65;
            label_SportsmanInfo.Text = "ХХХ";
            // 
            // comboBox1_bloodGroup
            // 
            comboBox1_bloodGroup.Font = new Font("Segoe UI", 13.8F);
            comboBox1_bloodGroup.FormattingEnabled = true;
            comboBox1_bloodGroup.Location = new Point(233, 170);
            comboBox1_bloodGroup.Name = "comboBox1_bloodGroup";
            comboBox1_bloodGroup.Size = new Size(288, 39);
            comboBox1_bloodGroup.TabIndex = 64;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Font = new Font("Segoe UI", 13.8F);
            dateTimePicker1.Location = new Point(233, 308);
            dateTimePicker1.MaxDate = new DateTime(2026, 10, 19, 0, 0, 0, 0);
            dateTimePicker1.MinDate = new DateTime(2020, 2, 1, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(288, 38);
            dateTimePicker1.TabIndex = 39;
            dateTimePicker1.Value = new DateTime(2026, 10, 19, 0, 0, 0, 0);
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 13.8F);
            label9.Location = new Point(48, 170);
            label9.Name = "label9";
            label9.Size = new Size(142, 31);
            label9.TabIndex = 12;
            label9.Text = "Група крові:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13.8F);
            label7.Location = new Point(38, 242);
            label7.Name = "label7";
            label7.Size = new Size(216, 31);
            label7.TabIndex = 10;
            label7.Text = "Допуск до змагань:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 13.8F);
            label5.Location = new Point(26, 314);
            label5.Name = "label5";
            label5.Size = new Size(201, 31);
            label5.TabIndex = 7;
            label5.Text = "Дата проведення:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F);
            label3.Location = new Point(6, 34);
            label3.Name = "label3";
            label3.Size = new Size(134, 31);
            label3.TabIndex = 6;
            label3.Text = "Спортсмен:";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            toolStripButton1.Image = Properties.Resources.plus_PNG84;
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(29, 24);
            toolStripButton1.Text = "toolStripButton1";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.Font = new Font("Segoe UI", 10.2F);
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(614, 27);
            toolStrip1.TabIndex = 54;
            toolStrip1.Text = "toolStrip1";
            // 
            // MedicalForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(614, 450);
            Controls.Add(groupBox2_sportRozrad);
            Controls.Add(toolStrip1);
            Name = "MedicalForm";
            Text = "Гавронський Валерій Пі-241";
            Load += MedicalForm_Load;
            groupBox2_sportRozrad.ResumeLayout(false);
            groupBox2_sportRozrad.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private GroupBox groupBox2_sportRozrad;
        private ComboBox comboBox1_bloodGroup;
        private DateTimePicker dateTimePicker1;
        private Label label9;
        private Label label7;
        private Label label5;
        private Label label3;
        private Label label_SportsmanInfo;
        private CheckBox checkBox1;
        private ToolStripButton toolStripButton1;
        private ToolStrip toolStrip1;
    }
}