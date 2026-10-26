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
    public partial class FormGroups : Form
    {
        // Масив груп та лічильник
        private SportGroup[] groups = new SportGroup[50];
        private int groupsCount = 0;
        // Масив тренерів, які завантажуються для вибору куратора
        private Coach[] availableCoaches = new Coach[50];
        private int coachesCount = 0;

        public FormGroups()
        {
            InitializeComponent();
        }

        private void FormGroups_Load(object sender, EventArgs e)
        {

        }
    }
}
