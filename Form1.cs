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
        Parser parser = new Parser();
        FileManager file = new FileManager();
        Bitmap drawing;
        DrawingPen canvasPen = new DrawingPen(0, 0, 860, 677);

        Commands test = new Commands();
        Pen drawingPen = new Pen(Color.Black);

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
            /*file.SaveFile(programCommand.Text);*/
            ShapeFactory test = new ShapeFactory();
            Shape tester = test.getShape("Rectangle");
            tester.Set(200, 200, 500, 100);
            tester.Draw(canvas, drawing);
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            parser.parseText(programCommand.Text, canvas, drawing, canvasPen);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file.SaveFile(programCommand.Text);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            programCommand.Text = file.LoadFile();
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
            parser.parseText(singleCommand.Text, canvas, drawing, canvasPen);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "ASE project.";
            string title = "About";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
          
        }
    }
}
