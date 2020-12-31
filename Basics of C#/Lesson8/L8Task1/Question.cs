using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L8Task1
{

    [Serializable]
    public class Question
    {
        public string Text { set; get; }
        public bool TrueFalse { set; get; }

        public Question()
        {

        }

        public Question(string text, bool tf)
        {
            Text = text;
            TrueFalse = tf;
        }
    }
}
