using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace L8Task1
{
    public partial class Form1 : Form
    {
        int selectedRow = 0;
        public Form1()
        {
            InitializeComponent();
            var q = tblQuestions.Controls[0] as QuestEditRow;
            q.QuestEditRowFocusedEvent += QuestEditRowFocused;
            tblQuestions.Width = panelForQuestions.Width - vsbQuestions.Width;
        }

        private void QuestEditRowFocused(object sender, EventArgs e)
        {
            selectedRow = (sender as QuestEditRow).Number - 1;
            btnDeleteQuestion.Text = $"Delete #{selectedRow + 1}";
        }

        private void AddQuestion(string text, bool trueFalse)
        {
            tblQuestions.RowCount++;
            tblQuestions.RowStyles.Add(new RowStyle());
            var q = new QuestEditRow(tblQuestions.RowCount, text, trueFalse);
            q.QuestEditRowFocusedEvent += QuestEditRowFocused;
            q.Dock = DockStyle.Top;
            tblQuestions.Controls.Add(q, 0, tblQuestions.RowCount - 1);
            q.tbQuestion.Focus();
        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            AddQuestion("", false);
        }

        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (tblQuestions.Controls.Count == 1)
            {
                var q = tblQuestions.Controls[0] as QuestEditRow;

                if (q.QuestionText == "")
                {
                    MessageBox.Show("You can't delete the first question.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else
                {
                    if (MessageBox.Show("Clear the first question?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        q.QuestionText = "";
                        q.TrueFalse = false;
                    }
                }

            }
            else
            {
                if (selectedRow >= 0 && selectedRow < tblQuestions.RowCount)
                {
                    if (MessageBox.Show($"Delete the question number {selectedRow + 1}?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        // update row number
                        for (int i = selectedRow; i < tblQuestions.RowCount; i++)
                        {
                            if (tblQuestions.Controls[i] != null)
                                (tblQuestions.Controls[i] as QuestEditRow).Number = i;
                        }
                        // remove question
                        //var q = tblQuestions.Controls[selectedRow] as QuestEditRow;
                        tblQuestions.Controls.RemoveAt(selectedRow);
                        // move questions thwt below
                        for (int i = selectedRow + 1; i < tblQuestions.RowCount; i++)
                        {
                            var control = tblQuestions.GetControlFromPosition(0, i);
                            if (control != null)
                            {
                                tblQuestions.SetRow(control, i - 1);
                            }
                        }

                        tblQuestions.RowStyles.RemoveAt(selectedRow);
                        tblQuestions.RowCount--;

                        // focus near question
                        if (selectedRow >= tblQuestions.RowCount)
                        {
                            selectedRow--;
                        }
                        var q = tblQuestions.Controls[selectedRow] as QuestEditRow;
                        q.tbQuestion.Focus();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void tblQuestions_Resize(object sender, EventArgs e)
        {
            vsbQuestions.Visible = !panelForQuestions.VerticalScroll.Visible;
            tblQuestions.Width = panelForQuestions.Width - vsbQuestions.Width;
            foreach (var obj in tblQuestions.Controls)
            {
                if (obj != null)
                {
                    var q = obj as QuestEditRow;
                    q.QuestionTextChanged();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        void DeleteAllQuestions()
        {
            for (int i = tblQuestions.RowCount - 1; i > 0; i--)
            {
                var control = tblQuestions.GetControlFromPosition(0, i);
                if (control != null)
                {
                    tblQuestions.Controls.RemoveAt(i);
                }
                tblQuestions.RowStyles.RemoveAt(i);
            }
            tblQuestions.RowCount = 1;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"When creating a new database, all current questions will be deleted.\r\nContinue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
            {
                DeleteAllQuestions();

                selectedRow = 0;

                var q = tblQuestions.Controls[0] as QuestEditRow;
                q.QuestionText = "";
                q.TrueFalse = false;
                q.tbQuestion.Focus();
            }
        }


        void Save(string filename)
        {
            List<Question> questions = new List<Question>();

            foreach(var obj in tblQuestions.Controls)
            {
                var q = obj as QuestEditRow;
                questions.Add(new Question(q.QuestionText, q.TrueFalse));
            }

            SaveQuestionsToXml(filename, questions);

        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var questions = LoadQuestionsFromXml("base");

            MessageBox.Show($"Загружено {questions.Count} вопросов");

            if(questions.Count > 0)
            {
                DeleteAllQuestions();

                var q = tblQuestions.Controls[0] as QuestEditRow;
                q.QuestionText = questions[0].Text;
                q.TrueFalse = questions[0].TrueFalse;

                for (int i = 1; i < questions.Count; i++)
                {
                    AddQuestion(questions[i].Text, questions[i].TrueFalse);
                }

                selectedRow = questions.Count;

                q = tblQuestions.Controls[questions.Count - 1] as QuestEditRow;
                q.tbQuestion.Focus();
            }            
        }

        void SaveQuestionsToXml(string filename, List<Question> list)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(stream, list);
            }
        }

        List<Question> LoadQuestionsFromXml(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            using(Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                 return xmlSerializer.Deserialize(stream) as List<Question>;
            }
        }
    }
}
