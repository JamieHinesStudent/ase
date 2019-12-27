using System;
using System.IO;
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
        /// Checks the extension of the file selected.
        /// </summary>
        /// <param name="filename">The file to check.</param>
        private void ValidateFileType(string filename)
        {
            if (Path.GetExtension(filename) != ".txt") //not a text file
            {
                throw new InvalidFileTypeException(filename); //Custom exception thrown
            }

        }

        /// <summary>
        /// Loads a file from the local storage on the computer.
        /// </summary>
        /// <returns>Returns the text contents of a file that is loaded in.</returns>
        public string LoadFile()
        {
            fileOpener = new OpenFileDialog();
            if (fileOpener.ShowDialog() == DialogResult.OK) //Opens dialog
            {
                try
                {
                    try
                    {
                        var sr = new StreamReader(fileOpener.FileName);
                        ValidateFileType(fileOpener.FileName); //validates file
                        return sr.ReadToEnd(); //File in form a string returned
                    }catch(InvalidFileTypeException ex)
                    {
                        MessageBox.Show("Error loading the file '" + ex.Message + "'. The program only supports .txt files.");
                        return null;
                    }
                    
                }
                catch (Exception) //Error in handling file
                {
                    MessageBox.Show("Error loading the file."); //Error message shown
                    return null; // Empty returned
                }
            }
            else
            {
                return null; //Empty returned
            }
        }

        /// <summary>
        /// Saves a file to the local storage on the computer
        /// </summary>
        /// <param name="commandContent">This string contents of the script command box</param>
        public void SaveFile(string commandContent)
        {
            fileSaver = new SaveFileDialog{FileName = "script.txt",Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"}; //New file saver object with settings

            if (fileSaver.ShowDialog() == DialogResult.OK) //File dialog
            {
                try
                {
                    try
                    {
                        ValidateFileType(fileSaver.FileName);
                        System.IO.Stream fileStream = fileSaver.OpenFile(); //Opens file
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream);
                        sw.WriteLine(commandContent); //Writes all content to file
                        sw.Flush();
                        sw.Close(); //close connection
                    }
                    catch (InvalidFileTypeException ex)
                    {
                        MessageBox.Show("Error saving the file '" + ex.Message + "'. The program only supports .txt files."); //Error message displayed
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error saving the file."); //Error message displayed
                }
            }
        }
    }
}
