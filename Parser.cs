using System;
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

            DrawingPen local = (DrawingPen)canvasPen;

            

            if (commands.Length >= 1){
                lexer = new Lexer(commands);
                Tokens getNextToken = lexer.CreateToken();
                while (getNextToken != Tokens.EOF){
                    /*System.Diagnostics.Debug.WriteLine(getNextToken.ToString());*/
                    switch(getNextToken.ToString()){
                        case "Clear": clearScreen(sender, drawing); break;
                        case "Reset": resetPen(sender, drawing, canvasPen); break;
                        case "MoveTo":

                            /* Ignore white spaces get next int */
                            /* Ignore white spaces get next comma */
                            /* Ignore white spaces get next int */
                            /* Ignore white spaces get next newline */

                            break;

                        case "DrawTo":

                            Boolean xFound = false;
                            Boolean yFound = false;
                            Boolean commaFound = false;
                            int xValue;
                            int yValue;

                            while (getNextToken.ToString() != "EOF" || getNextToken.ToString() != "NewLine")
                            {
                                if (getNextToken.ToString() != "WhiteSpace")
                                {
                                    if (xFound != true && getNextToken.ToString() != "IntegerLiteral")
                                    {
                                        xValue = 10;
                                    }

                                    else if (getNextToken.ToString() == "Comma")
                                    {
                                        commaFound = true;

                                    }

                                    else if (yFound != true && getNextToken.ToString() != "IntegerLiteral" && commaFound == true)
                                    {
                                        yValue = 10;
                                    }
                                }

                                getNextToken = lexer.CreateToken();

                            }
                            
                            /* Ignore white spaces get next int */
                            /* Ignore white spaces get next comma */
                            /* Ignore white spaces get next int */
                            /* Ignore white spaces get next newline */

                            break;

                        case "Circle":
                            
                            /* Ignore white spaces get next int */
                            /* Ignore white spaces get next comma */
                            /* Ignore white spaces get next int */
                            /* Ignore white spaces get next newline */

                            break;

                        case "Rectangle":
                            break;
                        case "Triangle":
                            break;
                        default:
                            /*System.Diagnostics.Debug.WriteLine("Not recognised");*/
                            break;
                    }
                    getNextToken = lexer.CreateToken();
                }

                /* Remove spaces */
                System.Diagnostics.Debug.WriteLine(lexer.listLength());
                /*lexer.removeSpacesList();*/

                
                

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
    }
}
