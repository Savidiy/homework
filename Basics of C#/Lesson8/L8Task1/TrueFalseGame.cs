using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L8Task1
{
    class TrueFalseGame
    {
        List<Question> _questions; // always !null

        public TrueFalseGame()
        {
            _questions = new List<Question>();
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

        public Question this[int index]
        {
            get
            {
                if (index >= 0 && index < _questions.Count)
                {
                    return _questions[index];
                } else
                {
                    throw new ArgumentOutOfRangeException("Argument out of range questions array");
                }
            }
            set {
                if (index >= 0 && index < _questions.Count)
                {
                    _questions[index] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Argument out of range questions array");
                }
            }
        }

        public int Count
        {
            get { return _questions.Count; }
        }

    }
}
