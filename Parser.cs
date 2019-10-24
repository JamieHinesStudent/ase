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

        private int tokensOnLine(List<Token> tokens, int lineNumber){
            int count = 0;
            for (int i=0; i<tokens.Count; i++){
                if(tokens[i].lineNumber == lineNumber){
                    count++;
                }
            }
            return count;
        }

        public void parseText(string commands, Object sender, Object drawing, Object canvasPen){

            DrawingPen local = (DrawingPen)canvasPen;

            List<Token> tokensReturned = new List<Token>();
            
            if (commands.Length >= 1){
                lexer = new Lexer(commands);
                Token getNextToken = lexer.CreateToken();
                while (getNextToken.tokenType != Tokens.EOF){
                    tokensReturned.Add(getNextToken);
                    getNextToken = lexer.CreateToken();
                }

                tokensReturned.Add(getNextToken);
                tokensReturned.RemoveAll(t => t.tokenType.ToString() == "NewLine" || t.tokenType.ToString() == "WhiteSpace" || t.tokenType.ToString() == "EOF");

                /*
                for (int i=0; i<tokensReturned.Count; i++){
                    System.Diagnostics.Debug.WriteLine(tokensReturned[i].tokenType.ToString());
                    System.Diagnostics.Debug.WriteLine(tokensReturned[i].lineNumber);
                }
                */

                int maxLineNumber  = tokensReturned[tokensReturned.Count - 1].lineNumber;
                /* starts at 1 */
                //System.Diagnostics.Debug.WriteLine(maxLineNumber);

                /* for each line */
                for (int i=1; i<maxLineNumber+1; i++){
                    Boolean foundFirst = false;
                    do{
                        for (int x=0; x<tokensReturned.Count; x++){
                            if (tokensReturned[x].lineNumber == i){
                                foundFirst = true;
                                switch(tokensReturned[0].tokenType.ToString()){
                                    case "Clear": 
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 1){
                                            System.Diagnostics.Debug.WriteLine("Clear");
                                            clearScreen(sender, drawing);
                                        }
                                        break;
                                    case "Reset": 
                                        System.Diagnostics.Debug.WriteLine("Reset");
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 1){
                                            resetPen(sender, drawing, canvasPen);
                                        }
                                        break;
                                    case "Moveto": 
                                        System.Diagnostics.Debug.WriteLine("Moveto");
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                        }
                                        break;
                                    case "Drawto": 
                                        System.Diagnostics.Debug.WriteLine("Drawto");
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                        }
                                        break;
                                    case "Circle": 
                                        System.Diagnostics.Debug.WriteLine("Circle");
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 2){
                                        }
                                        break;
                                    case "Rectangle": 
                                        System.Diagnostics.Debug.WriteLine("Rectangle");
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                        }
                                        break;
                                    case "Triangle": 
                                        System.Diagnostics.Debug.WriteLine("Triangle");
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 6){
                                        }
                                        break;
                                    default: break;
                                }

                                
                            }
                        }
                    
                    }while(foundFirst == false);       
                    System.Diagnostics.Debug.WriteLine("loop");
                }



                
                

                
        
                

                
                

            }


            
        }

      

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
