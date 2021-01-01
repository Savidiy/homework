using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/// Леонид Васильев
/// 3. *Написать программу-преобразователь из CSV в XML-файл с информацией о студентах (6 урок).

namespace L8Task3
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
            Application.Run(new CSVtoXMLconverterForm());
        }
    }
}
