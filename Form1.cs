using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load button on form clicked.");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Save button on form clicked.");
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Run button (for multi line program) on form clicked.");
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
    }
}
