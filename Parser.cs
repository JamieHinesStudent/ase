﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase
{
    class Parser
    {


        private Lexer lexer;

        private string[] splitLines(string command)
        {
            return command.Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
        }

        public void callParser(string text)
        {
            string[] programLines = splitLines(text);
            Console.WriteLine(programLines.Length);
            
           
        }

        

        public void parseText(string commands, Object sender, Object drawing, Object canvasPen){
            if (commands.Length >= 1){
                lexer = new Lexer(commands);
                Tokens getNextToken = lexer.CreateToken();
                while (getNextToken != Tokens.EOF){
                    System.Diagnostics.Debug.WriteLine(getNextToken.ToString());
                    switch(getNextToken.ToString()){
                        case "Clear": clearScreen(sender, drawing); break;
                        case "Reset": resetPen(sender, drawing, canvasPen); break;
                        case "MoveTo": System.Diagnostics.Debug.WriteLine("Move to command"); break;
                        case "DrawTo": System.Diagnostics.Debug.WriteLine("Draw to command"); break;
                        case "Circle":
                            /* Get next int */
                            circle(sender, drawing, 10, canvasPen);
                            break;
                        case "Rectangle": System.Diagnostics.Debug.WriteLine("Rectangle command"); break;
                        case "Triangle": System.Diagnostics.Debug.WriteLine("Triangle command"); break;
                        default:
                            System.Diagnostics.Debug.WriteLine("Not recognised");
                            break;
                    }
                    getNextToken = lexer.CreateToken();
                }
            }
        }

        /* Commands */

        /* Clears the screen */
        private void clearScreen(Object sender, Object drawing){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Transparent);
            canvas.Image = image;
            g.Dispose();
        }

        /* Resets the pen */
        public void resetPen(Object sender, Object drawing, Object canvasPen){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            g.TranslateTransform(0, 0);

            local.xCoordinate = 0;
            local.yCoordinate = 0;
            canvas.Image = image;
        }


        /* Draws pen to position */
        public void drawTo(Object sender, Object drawing, int x, int y, Object canvasPen){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);
            Pen mypen = new Pen(Color.Black);

            g.DrawLine(mypen, local.xCoordinate, local.yCoordinate, x, y);
            local.xCoordinate = x;
            local.yCoordinate = y;
            canvas.Image = image;

        }

        /* Moves pen to position */
        public void moveTo(Object sender, Object drawing, int x, int y, Object canvasPen){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            g.TranslateTransform(x, y);
            local.xCoordinate = x;
            local.yCoordinate = y;
            canvas.Image = image;
        }

        /* Circle */
        private void circle(Object sender, Object drawing, int radius, Object canvasPen){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            Pen mypen = new Pen(Color.Black);

            g.DrawEllipse(Pens.Red, local.xCoordinate, local.yCoordinate, radius*2, radius*2);
            canvas.Image = image;

            g.Dispose();
        }


        /* Base for testing */
        public void testDraw(Object sender, Object drawing)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g;
            g = Graphics.FromImage(image);

            Pen mypen = new Pen(Color.Black);

            g.DrawLine(mypen, 0, 0, 500, 150);

            g.DrawEllipse(Pens.Red, 50, 50, 20, 20);

            g.DrawRectangle(Pens.Blue, 50,50,100,100);

            Point[] points = { new Point(10, 10), new Point(100, 10), new Point(50, 100) };
            g.DrawPolygon(new Pen(Color.Blue), points);
            
            canvas.Image = image;

            g.Dispose();

           
        }

        public void testDrawing(Object sender, Object drawing)
        {
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g;
            g = Graphics.FromImage(image);

            Pen mypen = new Pen(Color.Black);


            g.DrawEllipse(Pens.Red, 50, 50, 100, 100);
            
            canvas.Image = image;

            g.Dispose();
        }
        
        
    }
}
