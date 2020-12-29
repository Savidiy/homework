using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace L7Task1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
