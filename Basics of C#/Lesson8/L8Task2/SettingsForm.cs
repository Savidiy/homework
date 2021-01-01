using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L8Task2
{
    public partial class SettingsForm : Form
    {
        public int MaxQuestions
        {
            set
            {
                nudCounter.Maximum = value;
            }
        }
        public int CountQuestionsInGame
        {
            get { return (int)nudCounter.Value; }
            set { nudCounter.Value = value; }
        }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
