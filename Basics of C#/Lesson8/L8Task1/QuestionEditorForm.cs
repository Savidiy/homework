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
    public partial class QuestionEditorForm : Form
    {
        string DBfilename
        {
            get { return _dbFilename; }
            set
            {
                _dbFilename = value;
                UpdateDatabaseLabel();
            }
        } // auto update filename interface label
        string _dbFilename; // don't use it variables in code
        TrueFalseGame trueFalseGame; // used to save/load xml file
        int selectedRow = 0; // number of last selected row (start from 0) for delete Button
        bool isLoadingDatabaseProcess = false; // block Resize events then database loading

        // main constructor
        public QuestionEditorForm()
        {
            InitializeComponent();

            var q = tblQuestions.Controls[0] as QuestEditRow;
            q.QuestEditRowFocusedEvent += QuestEditRowFocused;

            tblQuestions.Width = panelForQuestions.Width - vsbQuestions.Width;

            DBfilename = string.Empty;

            trueFalseGame = new TrueFalseGame();
        }

        // interface change functions
        private void AddQuestionRow(string text, bool trueFalse)
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
        private void DeleteAllRowQuestions()
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
        private void QuestEditRowFocused(object sender, EventArgs e)
        {
            selectedRow = (sender as QuestEditRow).Number - 1;
            btnDeleteQuestion.Text = $"&Delete #{selectedRow + 1}"; // d - hotkey to delete
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

        // database operations
        bool LoadDatabase(string filename)
        {
            if (trueFalseGame.LoadQuestionsFromXml(filename))
            {
                int count = trueFalseGame.Count;
                if (count > 0)
                {
                    //MessageBox.Show($"Load {questions.Count} questions", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DeleteAllRowQuestions();

                    var q = tblQuestions.Controls[0] as QuestEditRow;
                    q.QuestionText = trueFalseGame[0].Text;
                    q.TrueFalse = trueFalseGame[0].TrueFalse;

                    #region создание строчек в таблице с ускорениями
                    /// Без хитростей база из 58 записей загружается за 2.23 сек, наверное из-за постоянных resize таблицы и текстбоксов
                    /// SuspendLayout() + ResumeLayout() - не дает улучшений
                    /// isLoadingDatabaseProcess = true - блокирует обработку события tblQuestions_Resize, 
                    ///         которое показывает скролбар и вызывает обновление размеров текстбоксов вопросов, это сукоряет загрузку до 1.52
                    /// tblQuestions.Visible = false - скрывает таблицу на вермя загрузки записей. Время загрузки 0.65 сек
                    /// tblQuestions.Visible + isLoadingDatabaseProcess = true - дает загрузку 0.55 сек

                    //var now = DateTime.Now; // Считаем время загрузки базы через 
                    isLoadingDatabaseProcess = true; // block Resize events then database loading
                    tblQuestions.Visible = false;
                    //this.SuspendLayout();
                    for (int i = 1; i < count; i++)
                    {
                        AddQuestionRow(trueFalseGame[i].Text, trueFalseGame[i].TrueFalse);
                    }
                    isLoadingDatabaseProcess = false; // block Resize events then database loading
                    tblQuestions.Visible = true;
                    //this.ResumeLayout()
                    //tblQuestions_Resize(this, EventArgs.Empty);
                    // MessageBox.Show($"{DateTime.Now - now}"); // строка для анализа времени загрузки файла
                    #endregion

                    selectedRow = count - 1;

                    q = tblQuestions.Controls[count - 1] as QuestEditRow;
                    q.tbQuestion.Focus();
                    return true;
                }
                else
                {
                    MessageBox.Show($"There are no questions in the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            } else
            {
                MessageBox.Show($"Failed to load database from {filename}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }           
        }
        void SaveDatabase(string filename)
        {
            //List<Question> questions = new List<Question>();
            trueFalseGame.Clear();

            foreach(var obj in tblQuestions.Controls)
            {
                var q = obj as QuestEditRow;
                trueFalseGame.AddQuestion(q.QuestionText, q.TrueFalse);
            }

            if( trueFalseGame.SaveQuestionsToXml(filename) == false)
            {
                MessageBox.Show($"Failed to save database to {filename}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // button and menu events
        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            AddQuestionRow("", false);
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
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"When creating a new database, all current questions will be deleted.\r\nContinue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.Yes)
            {

                DeleteAllRowQuestions();

                selectedRow = 0;


                var q = tblQuestions.Controls[0] as QuestEditRow;
                q.QuestionText = "";
                DBfilename = string.Empty;
                q.TrueFalse = false;
                q.tbQuestion.Focus();
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Question file (*.xml)|*.xml|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DBfilename = openFileDialog.FileName;
                if (LoadDatabase(DBfilename) == false)
                    DBfilename = String.Empty; // unvalid file
            }                  
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBfilename == String.Empty)
            {
                saveAsToolStripMenuItem_Click(sender, e);
            } else
            {
                SaveDatabase(DBfilename); 
                UpdateDatabaseLabel(isSave: true);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Question file (*.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DBfilename = saveFileDialog.FileName;
                SaveDatabase(DBfilename);
                UpdateDatabaseLabel(isSave: true);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        // additional functions
        void UpdateDatabaseLabel(bool isSave = false)
        {
            if (DBfilename == string.Empty)
            {
                lblFilename.Text = "file not saved";
            }
            else
            {
                if (isSave)
                {
                    lblFilename.Text = $"{FilenameFromPath(DBfilename)} saved {DateTime.Now.ToString("HH:mm:ss")}";
                }
                else// Open file
                {
                    lblFilename.Text = $"{FilenameFromPath(DBfilename)}";
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
    }
}
