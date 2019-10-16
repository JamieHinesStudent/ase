using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    class FileManager
    {
        private OpenFileDialog fileOpener;
        private SaveFileDialog fileSaver;

        public string LoadFile()
        {
            fileOpener = new OpenFileDialog();
            if (fileOpener.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var sr = new StreamReader(fileOpener.FileName);
                    return sr.ReadToEnd();
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                    return null;
                   
                }
            }
            else
            {
                return null;
            }
        }

        public void SaveFile(string commandContent)
        {
            fileSaver = new SaveFileDialog();
            fileSaver.FileName = "script.txt";
            fileSaver.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
           
            if (fileSaver.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream fileStream = fileSaver.OpenFile();
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream);
                sw.WriteLine(commandContent);
                sw.Flush();
                sw.Close();
            }
        }
    }
}
