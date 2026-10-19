using lr_1_zd_3.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace lr_1_zd_3.Forms
{
    public partial class MedicalForm : Form
    {
        private Sportsman currentSportsman;
        public MedicalForm()
        {
            InitializeComponent();
        }
        public MedicalForm(Sportsman sportsman) : this()
        {
            currentSportsman = sportsman;

        }
        private void MedicalForm_Load(object sender, EventArgs e)
        {
            comboBox1_bloodGroup.Items.AddRange(new object[] { "O+", "O-", "A+", "A-", "B+", "B-", "AB+", "AB-" });
            if (currentSportsman != null)
            {
                label_SportsmanInfo.Text = $"{currentSportsman.Surname} {currentSportsman.Name}";
                comboBox1_bloodGroup.Text = currentSportsman.MedicalCard.BloodGroup;
                checkBox1.Checked = currentSportsman.MedicalCard.HasAccessToCompete;
                dateTimePicker1.Value = currentSportsman.MedicalCard.InspectationDate;

            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (currentSportsman != null)
            {
                // Записуємо нові дані в медичну карту
                currentSportsman.MedicalCard.BloodGroup = comboBox1_bloodGroup.Text;
                currentSportsman.MedicalCard.HasAccessToCompete = checkBox1.Checked;
                currentSportsman.MedicalCard.InspectationDate = dateTimePicker1.Value;
                MessageBox.Show("Дані медичної карти успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
    }
}
}
