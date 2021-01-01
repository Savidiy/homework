using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace L8Task2
{
    class TrueFalseGame
    {
        public int HowManyQuestionsInGame { get; private set; } = 0;
        int[] selectedQuestions;
        bool isLoaded = false;
        public int Scores { get; private set; } = 0;
        int currentNumber;
        Random random;
        List<Question> _questions; // always !null

        public TrueFalseGame()
        {
            _questions = new List<Question>();
            random = new Random();
        }
        public TrueFalseGame(string filename) : this()
        {
            LoadQuestionsFromXml(filename);
        }


        public void AddQuestion(string text, bool isTrue)
        {
            _questions.Add(new Question(text: text, tf: isTrue));
        }
        public void RemoveQuestion(int index)
        {
            if (index >= 0 && index < _questions.Count)
            {
                _questions.RemoveAt(index);
            }
        }


        public bool NewGame(int howManyQuestions)
        {
            if (isLoaded && howManyQuestions < _questions.Count)
            {
                HowManyQuestionsInGame = howManyQuestions;
                selectedQuestions = new int[HowManyQuestionsInGame];
                currentNumber = 0;
                Scores = 0;
                return true;
            }
            else
                return false;
        }

        public int NextQuestionNumber()
        {
            // повторять пока не найдешь вопрос, которого еще не было
            bool repeat;
            int nextNumber;
            if (currentNumber < HowManyQuestionsInGame)
            {
                do
                {
                    repeat = false;
                    nextNumber = random.Next(_questions.Count);
                    for (int i = 0; i < currentNumber; i++)
                    {
                        if (nextNumber == selectedQuestions[i])
                        {
                            repeat = true;
                            break;
                        }
                    }
                } while (repeat);
                selectedQuestions[currentNumber] = nextNumber;
                currentNumber++;

                return nextNumber;
            }
            else
            {
                throw new Exception("Хочешь слишком много вопросов, мы так не договаривались!");
            }
        }


        public bool TakeAnswer(int questionNumber, bool answer)
        {
            if (isLoaded && questionNumber >= 0 && questionNumber < _questions.Count)
            {
                if (_questions[questionNumber].TrueFalse == answer)
                {
                    Scores++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new Exception("Недопустимое значение индекса массива вопросов");
            }
        }


        public bool LoadQuestionsFromXml(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    _questions = xmlSerializer.Deserialize(stream) as List<Question>;
                    isLoaded = true;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool SaveQuestionsToXml(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    xmlSerializer.Serialize(stream, _questions);
                }
                return true;
            }
            catch
            {
                return false;
                //MessageBox.Show($"Failed to save database to {filename}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public string this[int questionNumber]
        {
            get
            {
                if (isLoaded && questionNumber >= 0 && questionNumber < _questions.Count)
                {
                    return _questions[questionNumber].Text;
                }
                else
                {
                    throw new Exception("Недопустимое значение индекса массива вопросов.");
                }
            }
        }
        //public Question this[int index]
        //{
        //    get
        //    {
        //        if (index >= 0 && index < _questions.Count)
        //        {
        //            return _questions[index];
        //        } else
        //        {
        //            throw new ArgumentOutOfRangeException("Argument out of range questions array");
        //        }
        //    }
        //    set {
        //        if (index >= 0 && index < _questions.Count)
        //        {
        //            _questions[index] = value;
        //        }
        //        else
        //        {
        //            throw new ArgumentOutOfRangeException("Argument out of range questions array");
        //        }
        //    }
        //}

        public int Count
        {
            get { return _questions.Count; }
        }
        public void Clear()
        {
            _questions.Clear();
        }
    }
}
