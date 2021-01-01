using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L8Task1
{
    public partial class QuestEditRow : UserControl
    {
        public event EventHandler QuestEditRowFocusedEvent;// (object sender, EventArgs e);

        int number = 1;
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                lblNumber.Text = number.ToString();
            }
        }
        public string QuestionText {
            get { return tbQuestion.Text; }
            set { tbQuestion.Text = value; }
        }
        public bool TrueFalse {
            get { return cbTrueFalse.Checked; }
            set { cbTrueFalse.Checked = value; }
        }

        public QuestEditRow()
        {
            InitializeComponent();
        }
        public QuestEditRow(int num, string text, bool trueFalse)
        {
            InitializeComponent();
            Number = num;
            tbQuestion.Text = text;
            cbTrueFalse.Checked = trueFalse;
        }

        private void cbTrueFalse_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTrueFalse.Checked == true)
                cbTrueFalse.Text = "TRUE";
            else
                cbTrueFalse.Text = "FALSE";
        }

        const int deltaTextHeight = 12;
        public void QuestionTextChanged()
        {
            var g = tbQuestion.CreateGraphics();
            var measureText = tbQuestion.Text + "."; // dot for check empty last string
            var textBoxWidth = tbQuestion.Width;
            int textH = (int)Math.Ceiling(g.MeasureString(measureText, tbQuestion.Font, textBoxWidth).Height);
            textH += deltaTextHeight;
            if (textH != this.Height)
            {
                Height = textH;
            }
        }

        private void tbQuestiontText_TextChanged(object sender, EventArgs e)
        {
            QuestionTextChanged();
        }

        private void tbQuestion_Enter(object sender, EventArgs e)
        {
            if (QuestEditRowFocusedEvent != null)
                QuestEditRowFocusedEvent.Invoke(this, EventArgs.Empty);
        }

        private void cbTrueFalse_Enter(object sender, EventArgs e)
        {
            if (QuestEditRowFocusedEvent != null)
                QuestEditRowFocusedEvent.Invoke(this, EventArgs.Empty);
        }
    }
}
