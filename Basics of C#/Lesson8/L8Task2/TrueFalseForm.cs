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
    public partial class TrueFalseForm : Form
    {
        TrueFalseGame trueFalseGame;
        int countQuestionsInGame = 5; // количество вопросоа в сессии
        int questionIndex = 0; // номер вопроса в сессии
        int currentQuestionDatabaseIndex = 0; // номер вопроса в базы
        int _score = 0; // don't use in code
        int Score
        {
            get { return _score; }
            set { 
                _score = value;
                UpdateScoreLabel();
            }
        }

        public TrueFalseForm()
        {
            InitializeComponent();
            ShowNewGameButtons();
            lblBaseText.Text = string.Empty;
            trueFalseGame = new TrueFalseGame(AppDomain.CurrentDomain.BaseDirectory + "Database.xml");
        }

        // interface
        void ShowTrueFalseButtons()
        {
            btnFalse.Visible = true;
            btnTrue.Visible = true;
            lblScore.Visible = true;
            btnNewGame.Visible = false;
        }
        void ShowNewGameButtons()
        {
            btnFalse.Visible = false;
            btnTrue.Visible = false;
            lblScore.Visible = false;
            btnNewGame.Visible = true;
        }
        void UpdateScoreLabel()
        {
            lblScore.Text = $"Правильных ответов {Score} из {trueFalseGame.HowManyQuestionsInGame}";
        }

        // game logic
        void NewGame()
        {
            trueFalseGame.NewGame(countQuestionsInGame);
            Score = 0;
            questionIndex = 0;
            ShowTrueFalseButtons();
            ShowNextQuestion();

        }
        void EndGame()
        {
            lblBaseText.Text = $"Поздравляю!\r\nВы правильно ответили на {Score} из {trueFalseGame.HowManyQuestionsInGame} вопросов.";
            ShowNewGameButtons();
        }
        void TakeAnswer(bool isTrue)
        {
            if(trueFalseGame.TakeAnswer(currentQuestionDatabaseIndex, isTrue))
            {
                Score++;
            }
            
            if (questionIndex >= trueFalseGame.HowManyQuestionsInGame) // end game
            {
                EndGame();
            } 
            else // next question
            {
                ShowNextQuestion();
            }
        }
        void ShowNextQuestion()
        {
            currentQuestionDatabaseIndex = trueFalseGame.NextQuestionNumber();
            lblBaseText.Text = $"{questionIndex + 1}. {trueFalseGame[currentQuestionDatabaseIndex]}";
            questionIndex++;
        }

        // button events
        private void btnTrue_Click(object sender, EventArgs e)
        {
            TakeAnswer(true);
        }
        private void btnFalse_Click(object sender, EventArgs e)
        {
            TakeAnswer(false);
        }
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        // menu events
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.MaxQuestions = trueFalseGame.Count;
            settingsForm.CountQuestionsInGame = countQuestionsInGame;

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                countQuestionsInGame = settingsForm.CountQuestionsInGame;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
