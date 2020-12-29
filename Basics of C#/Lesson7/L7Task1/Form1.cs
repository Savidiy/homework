using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// Леонид Васильев
/// 1. 
/// а) Добавить в программу «Удвоитель» подсчет количества отданных команд.
/// б) Добавить меню и команду «Играть». При нажатии появляется сообщение, какое число должен получить игрок. 
///    Игрок должен постараться получить это число за минимальное количество ходов.
/// в) *Добавить кнопку «Отменить», которая отменяет последние ходы.

namespace L7Task1
{
    public partial class Form1 : Form
    {
        public int currentNumber;
        public int targetNumber;
        readonly Random random;
        readonly int minValue = 11;
        readonly int maxValue = 60;
        int record;
        readonly List<bool> actions; // true +1, false *2

        public Form1()
        {
            InitializeComponent();
            actions = new List<bool>();
            random = new Random();
            UpdateNumber(1, true);          
        }

        private void btnCommand1_Click(object sender, EventArgs e)
        {
            actions.Add(true);
            UpdateNumber(currentNumber + 1);
        }

        private void btnCommand2_Click(object sender, EventArgs e)
        {
            actions.Add(false);
            UpdateNumber(currentNumber * 2);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        void UpdateNumber(int num, bool resetCommandCount = false)
        {
            if (resetCommandCount == true)
            {
                actions.Clear();
            }
            
            currentNumber = num;
            lblNumber.Text = $"Ваше число: {currentNumber}";
            lblCommandCount.Text = $"Количество команд: {actions.Count}";

            if(currentNumber == targetNumber)
            {
                //btnCommand1.Enabled = false;
                //btnCommand2.Enabled = false;
                switch(new Form2(actions.Count, record).ShowDialog(this))
                {
                    case DialogResult.OK:
                        NewGame();
                        break;
                    case DialogResult.Cancel:
                        Reset();
                        break;
                }
            } else if(currentNumber > targetNumber)
            {
                btnCommand1.Enabled = false;
                btnCommand2.Enabled = false;
                lblNumber.ForeColor = Color.Red;
            } else
            {
                btnCommand1.Enabled = true;
                btnCommand2.Enabled = true;
                lblNumber.ForeColor = Color.Black;
            }
        }
        void GetNewTargetNumber(int min, int max)
        {
            targetNumber = random.Next(min, max);
            lblTargetNumber.Text = $"Получите: {targetNumber}";
        }
        void NewGame()
        {
            btnCommand1.Enabled = true;
            btnCommand2.Enabled = true;
            actions.Clear();
            GetNewTargetNumber(minValue, maxValue);
            record = CalcRecord(targetNumber);
            lblRecord.Text = $"Рекорд: {record}";
            UpdateNumber(1, true);
            MessageBox.Show(this, $"Получите число {targetNumber} за наименьшее количество команд.\r\n+1 - увеливает ваше число на 1\r\nх2 - увеличивает ваше число в два раза. ", "Новая игра", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        int CalcRecord(int num)
        {
            int count = 0;
            while (num > 1)
            {
                if (num % 2 == 1)
                {
                    num -= 1;
                } else
                {
                    num /= 2;
                }
                count++;
            }
            return count;
        }
        void Reset()
        {
            btnCommand1.Enabled = true;
            btnCommand2.Enabled = true;
            actions.Clear();
            UpdateNumber(1, true);
        }
        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            NewGame();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (actions.Count > 0)
            {
                bool lastAction = actions[actions.Count - 1];
                actions.RemoveAt(actions.Count - 1);
                if (lastAction) // last action +1
                {
                    UpdateNumber(currentNumber - 1);
                } else // last action *2
                {
                    UpdateNumber(currentNumber / 2);
                }
            }
        }
    }
}
