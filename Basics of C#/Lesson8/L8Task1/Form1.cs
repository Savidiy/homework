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
        int selectedRow = 0; // number of selected row (start from 0) to delete Button
        string dbFilename = string.Empty;
        //string DBfilename
        //{
        //    get { return _dbFilename; }
        //    set
        //    {
        //        _dbFilename = value;
        //        UpdateDatabaseLabel();
        //    }
        //}


        bool isLoadingDatabaseProcess = false; // block Resize events then database loading
        
        public Form1()
        {
            InitializeComponent();

            var q = tblQuestions.Controls[0] as QuestEditRow;
            q.QuestEditRowFocusedEvent += QuestEditRowFocused;

            tblQuestions.Width = panelForQuestions.Width - vsbQuestions.Width;

            UpdateDatabaseLabel();
        }

        private void QuestEditRowFocused(object sender, EventArgs e)
        {
            selectedRow = (sender as QuestEditRow).Number - 1;
            btnDeleteQuestion.Text = $"&Delete #{selectedRow + 1}"; // d - hotkey to delete
        }

        private void AddQuestion(string text, bool trueFalse)
        {
            tblQuestions.RowCount++;
            tblQuestions.RowStyles.Add(new RowStyle());
            var q = new QuestEditRow(tblQuestions.RowCount, text, trueFalse);            
            tblQuestions.Controls.Add(q, 0, tblQuestions.RowCount - 1);
            q.QuestEditRowFocusedEvent += QuestEditRowFocused;
            q.Dock = DockStyle.Top;
            //selectedRow

            if (isLoadingDatabaseProcess == false) // block Resize events then database loading
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
                        int saveSelectedRow = selectedRow; // if add some question with hotkey, then delete question with hotkey will be uncorrect question numbers
                        tblQuestions.Controls.RemoveAt(selectedRow);
                        selectedRow = saveSelectedRow;

                        // move questions thwt below
                        for (int i = selectedRow + 1; i < tblQuestions.RowCount; i++)
                        {
                            var control = tblQuestions.GetControlFromPosition(0, i);
                            if (control != null)
                            {
                                tblQuestions.SetRow(control, i - 1);
                            }
                        }

                        // focus near question
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

        private void tblQuestions_Resize(object sender, EventArgs e)
        {
            if (isLoadingDatabaseProcess == false)
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
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        void DeleteAllQuestions()
        {
            // блокировка отображения таблицы
            isLoadingDatabaseProcess = true; // block Resize events then database loading
            tblQuestions.Visible = false;

            // само удаление строк
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

            // разблокировка отображения таблицы
            isLoadingDatabaseProcess = false;
            tblQuestions.Visible = true;
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
                dbFilename = string.Empty;
                UpdateDatabaseLabel();
                q.TrueFalse = false;
                q.tbQuestion.Focus();
            }
        }

        bool LoadDatabase(string filename)
        {
            var questions = LoadQuestionsFromXml(filename);

            if (questions.Count > 0)
            {
                //MessageBox.Show($"Load {questions.Count} questions", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DeleteAllQuestions();

                var q = tblQuestions.Controls[0] as QuestEditRow;
                q.QuestionText = questions[0].Text;
                q.TrueFalse = questions[0].TrueFalse;

                #region Загрузка базы с ускорениями
                /// Без хитростей база из 58 записей загружается за 2.23 сек, наверное из-за постоянных resize таблицы и текстбоксов
                /// SuspendLayout() + ResumeLayout() - не дает улучшений
                /// isLoadingDatabaseProcess = true - блокирует обработку события tblQuestions_Resize, 
                ///         которое показывает скролбар и вызывает обновление размеров текстбоксов вопросов, это сукоряет загрузку до 1.52
                /// tblQuestions.Visible = false - скрывает таблицу на вермя загрузки записей. Время загрузки 0.65 сек
                /// tblQuestions.Visible + isLoadingDatabaseProcess = true - дает загрузку 0.55 сек

                //var now = DateTime.Now; // Считаем время загрузки базы через 
                isLoadingDatabaseProcess = true; // block Resize events then database loading
                //this.SuspendLayout();
                tblQuestions.Visible = false;
                for (int i = 1; i < questions.Count; i++)
                {
                    AddQuestion(questions[i].Text, questions[i].TrueFalse);
                }
                isLoadingDatabaseProcess = false; // block Resize events then database loading
                //tblQuestions_Resize(this, EventArgs.Empty);
                tblQuestions.Visible = true;
                //this.ResumeLayout()
                // MessageBox.Show($"{DateTime.Now - now}"); // строка для анализа времени загрузки файла
                #endregion

                selectedRow = questions.Count;

                q = tblQuestions.Controls[questions.Count - 1] as QuestEditRow;
                q.tbQuestion.Focus();
                return true;
            } 
            else
            {
                MessageBox.Show($"There are no questions in the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        void SaveDatabase(string filename)
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Question file (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dbFilename = openFileDialog.FileName;
                if (LoadDatabase(dbFilename) == false)
                    dbFilename = String.Empty; // unvalid file
                UpdateDatabaseLabel();
            }                  
        }

        void SaveQuestionsToXml(string filename, List<Question> list)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    xmlSerializer.Serialize(stream, list);
                }
            }
            catch
            {
                MessageBox.Show($"Failed to save database to {filename}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        List<Question> LoadQuestionsFromXml(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    return xmlSerializer.Deserialize(stream) as List<Question>;
                }
            }
            catch
            {
                MessageBox.Show($"Failed to load database from {filename}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Question>();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dbFilename == String.Empty)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            } else
            {
                SaveDatabase(dbFilename);
                UpdateDatabaseLabel(isSave: true);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Question file (*.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                dbFilename = saveFileDialog.FileName;
                SaveDatabase(dbFilename);
                UpdateDatabaseLabel(isSave: true);
            }
        }
        void UpdateDatabaseLabel(bool isSave = false)
        {
            if (dbFilename == string.Empty)
            {
                lblFilename.Text = "file not saved";
            }
            else
            {
                if (isSave)
                {
                    lblFilename.Text = $"{FilenameFromPath(dbFilename)} saved {DateTime.Now.ToString("HH:mm:ss")}";
                }
                else// Open file
                {
                    lblFilename.Text = $"{FilenameFromPath(dbFilename)}";
                }
            }
        }

        string FilenameFromPath(string path)
        {
            int i = path.LastIndexOf('\\') + 1;
            if (i > 0 && i < path.Length)
                return path.Substring(i);
            i = path.LastIndexOf('/') + 1;
            if (i > 0 && i < path.Length)
                return path.Substring(i);
            return path;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
