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

namespace L8Task3
{

    public partial class CSVtoXMLconverterForm : Form
    {
        List<Student> students;

        public CSVtoXMLconverterForm()
        {
            InitializeComponent();
            lblLoad.Text = "1. Загрузите данные из файла .CSV";
            btnSave.Enabled = false;
            lblSave.Text = "2. Сохраните данные в файл .XML";
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); 
            openFileDialog.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                students = Student.LoadStudentsFromCSV(filename);

                if (students.Count > 0)
                {
                    btnSave.Enabled = true;
                    lblLoad.Text = $"Загружено {students.Count} записей из файла {FilenameFromPath(filename)}";
                } 
                else
                {
                    btnSave.Enabled = false;
                    lblLoad.Text = $"Записи из файла {FilenameFromPath(filename)} не загружены. Неверный формат данных.";
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Question file (*.xml)|*.xml|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Student>));
                try
                {
                    using (Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        xmlSerializer.Serialize(stream, students);
                    }
                    lblSave.Text = $"Сохранено {students.Count} записей в файл {FilenameFromPath(filename)}";
                }
                catch
                {
                    lblSave.Text = $"Произошла ошибка при сохранении данных в файл {FilenameFromPath(filename)}";
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

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string text = @"Структура CSV файла:
Имя, Фамилия, Университет, Факультет, Кафедра, Возраст, Курс, Группа, Город.

Возраст, Курс и Группа - целые числа.
Остальные поля - строки.
Разделитель ',' или ';'.";
            MessageBox.Show(
                text,
                "Информация",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }
    }
}
