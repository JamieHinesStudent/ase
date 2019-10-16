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

namespace ase
{
    public partial class Form1 : Form
    {
        Parser commandParser = new Parser();
        FileManager file = new FileManager();
        Bitmap drawing;

        public Form1()
        {
            InitializeComponent();
            drawing = new Bitmap(canvas.Size.Width, canvas.Size.Height);
            canvas.Image = drawing;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            programCommand.Text = file.LoadFile();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            file.SaveFile(programCommand.Text);
        }

        private void runButton_Click(object sender, EventArgs e)
        {
           
            commandParser.testDraw(canvas, drawing);

            commandParser.callParser(programCommand.Text);
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Save button on menu clicked.");
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load button on menu clicked.");
            var shape = ShapeFactory.GetShape<Circle>();
            shape.Draw();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {

        }

        private void runCommand_Click(object sender, EventArgs e)
        {
            commandParser.callParser(singleCommand.Text);
        }
    }
}
