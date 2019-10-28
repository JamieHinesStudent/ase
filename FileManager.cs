using System;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace ase
{
    /// <summary>
    /// Class which manages the handling of external files. Such as loading and saving.
    /// </summary>
    class FileManager
    {
        private OpenFileDialog fileOpener;
        private SaveFileDialog fileSaver;

        /// <summary>
        /// Loads a file from the local storage on the computer.
        /// </summary>
        /// <returns>Returns the text contents of a file that is loaded in.</returns>
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
                catch (Exception)
                {
                    MessageBox.Show("Error loading the file.");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Saves a file to the local storage on the computer
        /// </summary>
        /// <param name="commandContent">This string contents of the script command box</param>
        public void SaveFile(string commandContent)
        {
            fileSaver = new SaveFileDialog{FileName = "script.txt",Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"};

            if (fileSaver.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.Stream fileStream = fileSaver.OpenFile();
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream);
                    sw.WriteLine(commandContent);
                    sw.Flush();
                    sw.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error saving the file.");
                }
            }
        }
    }
}
