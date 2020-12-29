using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L7Task2
{
    public partial class Form1 : Form
    {

        int minValue = 1;
        int maxValue = 100;
        int countOfAttempt;
        int maxAttemt;
        int magicNumber;
        Random random;

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            NewGame();
        }

        void NewGame()
        {
            magicNumber = random.Next(minValue, maxValue + 1);
            maxAttemt = (int)Math.Ceiling(Math.Log(maxValue - minValue + 2, 2)); // 7 значений 3 попытки, 8 значения 4 попытки
            string text = $"Я загадала число от {minValue} до {maxValue}. ";
            text += $"Попробуй его угадать с {maxAttemt} попыт{GetWordEndByNumber("ки","ок","ок",maxAttemt)}.";

            tbHistory.Clear();
            tbNumber.Visible = true;
            btnAnswer.Visible = true;
            label1.Visible = true;
            btnNewGame.Visible = false;
            tbNumber.Focus();
            countOfAttempt = 0;
            
            SetPic(GirlPicEnum.Hello);
            GirlSay(text);
        }

        enum GirlPicEnum { Hello, GameOver, Error, Win, MyNumIsLess, MyNumIsBigger}
        void SetPic(GirlPicEnum num)
        {
            switch (num)
            {
                case GirlPicEnum.MyNumIsLess:
                case GirlPicEnum.MyNumIsBigger:
                    switch (random.Next(0, 6))
                    {
                        case 0: pbGirl.Image = L7Task2.Properties.Resources._1; break;
                        case 1: pbGirl.Image = L7Task2.Properties.Resources._6; break;
                        case 2: pbGirl.Image = L7Task2.Properties.Resources._2; break;
                        case 3: pbGirl.Image = L7Task2.Properties.Resources._3; break;
                        case 4: pbGirl.Image = L7Task2.Properties.Resources._4; break;
                        case 5: pbGirl.Image = L7Task2.Properties.Resources._2; break;
                    }
                    break;
                case GirlPicEnum.Error:
                    switch (random.Next(0, 4))
                    {
                        case 0: pbGirl.Image = L7Task2.Properties.Resources._4; break;
                        case 1: pbGirl.Image = L7Task2.Properties.Resources._6; break;
                        case 2: pbGirl.Image = L7Task2.Properties.Resources._2; break;
                        case 3: pbGirl.Image = L7Task2.Properties.Resources._5; break;
                    }
                    break;
                case GirlPicEnum.GameOver:
                    pbGirl.Image = L7Task2.Properties.Resources.gameover;
                    break;
                case GirlPicEnum.Win:
                    switch (random.Next(0, 2))
                    {
                        case 0: pbGirl.Image = L7Task2.Properties.Resources._1; break;
                        case 1: pbGirl.Image = L7Task2.Properties.Resources._6; break;
                    }
                    break;
                case GirlPicEnum.Hello:
                    switch (random.Next(0, 2))
                    {
                        case 0: pbGirl.Image = L7Task2.Properties.Resources._4; break;
                        case 1: pbGirl.Image = L7Task2.Properties.Resources._6; break;
                    }
                    break;
            }

        }

        void GirlSay(string text, int num = 0)
        {
            lblDialog.Text = $"\"{text}\"";
            string prefix = num == 0 ? "" : num + ". ";
            tbHistory.Text += $"{prefix}{text}\r\n";
        }

        void GetAnswer()
        {
            string text = tbNumber.Text;
            tbNumber.Text = "";
            int num;
            if (text == "")
            {
                SetPic(GirlPicEnum.Error);
                string[] phrases = new string[]
                {
                    $"Ты думаешь я загадала ничего? Нет, число самое всамделишное!",
                    "И что это? Мнимая единица?",
                    "Если имелся ввиду 0, то можно было так и сказать."
                };
                GirlSay(phrases[random.Next(0, phrases.Length)]);
            }
            else if (int.TryParse(text, out num))
            {
                if (num < minValue)
                {
                    SetPic(GirlPicEnum.Error); 
                    string[] phrases = new string[]
                    {
                        $"Число должно быть больше {minValue - 1}. Я же сразу это сказала!",
                        $"Я же сказала, что загадала число от {minValue} до {maxValue}.",
                        $"Меня вообще слушают? Ответ точно больше {minValue - 1}."
                    };
                    GirlSay(phrases[random.Next(0, phrases.Length)]);
                }
                else if (num > maxValue)
                {
                    SetPic(GirlPicEnum.Error);
                    string[] phrases = new string[]
                    {
                        $"С кем я разговариваю! Число должно быть не больше {maxValue}.",
                        $"Я же сказала, что загадала число от {minValue} до {maxValue}.",
                        $"Меня вообще слушают? Ответ точно меньше {maxValue + 1}."
                    };
                    GirlSay(phrases[random.Next(0, phrases.Length)]);
                }
                else
                {
                    countOfAttempt++;
                    if (num == magicNumber)
                    {
                        SetPic(GirlPicEnum.Win);
                        string[] phrases = new string[]
                        {
                            $"Вау! Я и правда загадала {num}. Ты что мысли читаешь?",
                            $"В точку! Я загадала {num}. Повторим?",
                            $"{num} это правильный ответ! Давай еще раз?"
                        };
                        GirlSay(phrases[random.Next(0, phrases.Length)], countOfAttempt);
                        // скрываем текстовое поле
                        tbNumber.Visible = false;
                        btnAnswer.Visible = false;
                        label1.Visible = false;
                        btnNewGame.Visible = true;
                        btnNewGame.Focus();
                    }
                    else
                    {
                        if (num > magicNumber)
                        {
                            SetPic(GirlPicEnum.MyNumIsLess);
                            string[] phrases = new string[]
                            {
                                $"Я бы такое не загадала! {num} слишком большое число!",
                                $"Хей-хей полегче. Моё число меньше {num}.",
                                $"{num} это много. Слишком много."
                            };
                            GirlSay(phrases[random.Next(0, phrases.Length)], countOfAttempt);
                        }
                        else
                        {
                            SetPic(GirlPicEnum.MyNumIsBigger);
                            string[] phrases = new string[]
                            {
                                $"Ха! {num}! Вот это мелочь ты придумал!",
                                $"Не мелочись! Моё число больше чем {num}.",
                                $"Холодно. {num} слишком маленькое число."
                            };
                            GirlSay(phrases[random.Next(0, phrases.Length)], countOfAttempt);
                        }
                        if (countOfAttempt >= maxAttemt)
                        {
                            SetPic(GirlPicEnum.GameOver);
                            string[] phrases = new string[]
                            {
                                $"У тебя закончились попытки. Я загадала {magicNumber}. Еще разок?",
                                $"Увы, тебе повезет в другой раз. Моё число было {magicNumber}.",
                                $"{magicNumber}! {magicNumber}! Что так сложно угадать было? Давай еще раз?"
                            };
                            GirlSay(phrases[random.Next(0, phrases.Length)]);
                            // скрываем текстовое поле
                            tbNumber.Visible = false;
                            btnAnswer.Visible = false;
                            label1.Visible = false;
                            btnNewGame.Visible = true;
                            btnNewGame.Focus();
                        }
                    }
                }
            }
            else if (double.TryParse(text, out double dd))
            {
                SetPic(GirlPicEnum.Error);
                GirlSay("Точно! Забыла сказать, что я загадала целое число.");
            }
            else
            {
                SetPic(GirlPicEnum.Error);
                string[] phrases = new string[]
                {
                    "Ты точно знаешь как выглядят цифры?",
                    $"Я загадала число и ты отгадывай число!",
                    $"Ты что ли издеваешься надо мной?"
                };
                GirlSay(phrases[random.Next(0, phrases.Length)]);
            }
            // автопрокрутка
            tbHistory.SelectionStart = tbHistory.Text.Length;
            tbHistory.ScrollToCaret();
        }


        private void btnAnswer_Click(object sender, EventArgs e)
        {
            GetAnswer();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void tbNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                GetAnswer();
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        public static string GetWordEndByNumber(string s1, string s4, string s5, int num)
        {
            num %= 100;
            if (num >= 11 && num <= 19)
                return s5;
            else
            {
                num %= 10;
                switch (num)
                {
                    case 1: return s1;
                    case 2:
                    case 3:
                    case 4: return s4;
                    default: return s5;
                }
            }
        }
    }
}
