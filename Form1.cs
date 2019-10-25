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

        public Form1()
        {
            InitializeComponent();
            drawing = new Bitmap(canvas.Size.Width, canvas.Size.Height);
            canvas.Image = drawing;  
        }

        private void loadButton_Click(object sender, EventArgs e){programCommand.Text = file.LoadFile();}

        private void saveButton_Click(object sender, EventArgs e){file.SaveFile(programCommand.Text);}

        private void runButton_Click(object sender, EventArgs e){parser.parseText(programCommand.Text, canvas, drawing, canvasPen);}

        private void saveToolStripMenuItem_Click(object sender, EventArgs e){file.SaveFile(programCommand.Text);}

        private void loadToolStripMenuItem_Click(object sender, EventArgs e){programCommand.Text = file.LoadFile();}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e){Application.Exit();}

        private void Form1_Load(object sender, EventArgs e){}

        private void canvas_Paint(object sender, PaintEventArgs e){}

        private void runCommand_Click(object sender, EventArgs e){parser.parseText(singleCommand.Text, canvas, drawing, canvasPen);}

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string message = "Create scripts to draw on the canvas. \u00a92019\n"+
                "Type multiple statements in the script editor on the left or one statement in the command line. You can execute by pressing the \"run\" button.\n"+
                "Save and load scripts.\n\nCommands\n\n"+
                "Clear the screen -> clear\n"+
                "Reset pen x,y coordinates to 0 -> reset\n"+
                "Move the pen to the x,y coordinates -> moveto x,y\n"+
                "Draw with the pen to the x,y coordinates -> drawto x,y\n"+
                "Draw a circle with radius r -> circle r\n"+
                "Draw a rectangle with width x and height y -> rectangle x,y\n"+
                "Draw triangle with sides x,y,z -> triangle x,y,z";
            const string title = "About";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
        }
    }
}
