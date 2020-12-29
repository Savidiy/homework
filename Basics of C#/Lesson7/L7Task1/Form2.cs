using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L7Task1
{
    public partial class Form2 : Form
    {
        public Form2(int k, int record)
        {
            InitializeComponent();
            if (k < record)
                lblText.Text = $"Поздравляю!\n\nВы побили рекорд за {k} действ{Program.GetWordEndByNumber("ие", "ия", "ий", k)}.\n\nЕще разок?";
            else if (k == record)
                lblText.Text = $"Отлично!\n\nВы как рекордсмен справились за {k} действ{Program.GetWordEndByNumber("ие", "ия", "ий", k)}.\n\nЧто дальше?";
            else
                lblText.Text = $"Хорошо!\n\nВы справились за {k} действ{Program.GetWordEndByNumber("ие", "ия", "ий", k)}.\n\nПопробуйте побить рекорд {record}.";
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.OK;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            
        }
        
    }
}
