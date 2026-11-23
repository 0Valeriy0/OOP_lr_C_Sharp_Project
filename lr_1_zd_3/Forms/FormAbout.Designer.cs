namespace lr_1_zd_3.Forms
{
    partial class FormAbout
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
            label_Title = new Label();
            label_Description = new Label();
            panel_Bottom = new Panel();
            button_Close = new Button();
            panel_Bottom.SuspendLayout();
            SuspendLayout();
            // 
            // label_Title
            // 
            label_Title.Dock = DockStyle.Top;
            label_Title.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            label_Title.ForeColor = Color.FromArgb(31, 78, 121);
            label_Title.Location = new Point(0, 0);
            label_Title.Name = "label_Title";
            label_Title.Padding = new Padding(40, 30, 40, 10);
            label_Title.Size = new Size(1400, 200);
            label_Title.TabIndex = 0;
            label_Title.Text = "Автоматизація роботи\r\nДитячо-юнацької спортивної школи (ДЮСШ)";
            label_Title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_Description
            // 
            label_Description.Dock = DockStyle.Fill;
            label_Description.Font = new Font("Segoe UI", 18F);
            label_Description.Location = new Point(0, 200);
            label_Description.Name = "label_Description";
            label_Description.Padding = new Padding(120, 10, 120, 10);
            label_Description.Size = new Size(1400, 520);
            label_Description.TabIndex = 1;
            // 
            // panel_Bottom
            // 
            panel_Bottom.Controls.Add(button_Close);
            panel_Bottom.Dock = DockStyle.Bottom;
            panel_Bottom.Location = new Point(0, 720);
            panel_Bottom.Name = "panel_Bottom";
            panel_Bottom.Size = new Size(1400, 90);
            panel_Bottom.TabIndex = 2;
            // 
            // button_Close
            // 
            button_Close.Anchor = AnchorStyles.Bottom;
            button_Close.DialogResult = DialogResult.OK;
            button_Close.Font = new Font("Segoe UI", 14F);
            button_Close.Location = new Point(600, 15);
            button_Close.Name = "button_Close";
            button_Close.Size = new Size(200, 55);
            button_Close.TabIndex = 0;
            button_Close.Text = "Закрити";
            button_Close.UseVisualStyleBackColor = true;
            // 
            // FormAbout
            // 
            AcceptButton = button_Close;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = button_Close;
            ClientSize = new Size(1400, 810);
            Controls.Add(label_Description);
            Controls.Add(label_Title);
            Controls.Add(panel_Bottom);
            Name = "FormAbout";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Про програму";
            WindowState = FormWindowState.Maximized;
            Load += FormAbout_Load;
            Resize += FormAbout_Resize;
            panel_Bottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label_Title;
        private Label label_Description;
        private Panel panel_Bottom;
        private Button button_Close;
    }
}
