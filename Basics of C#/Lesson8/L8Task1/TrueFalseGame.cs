using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        public bool LoadQuestionsFromXml(string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Question>));
            try
            {
                using (Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    _questions = xmlSerializer.Deserialize(stream) as List<Question>;
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
        public void Clear()
        {
            _questions.Clear();
        }

    }
}
