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
        
        private void noParseError(string errorMessage){
            const string title = "Invalid Statement Error";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(errorMessage, title, buttons, MessageBoxIcon.Error);
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

                

                int maxLineNumber  = tokensReturned[tokensReturned.Count - 1].lineNumber;
                

                /* for each line */
                for (int i=1; i<maxLineNumber+1; i++){
                    Boolean foundFirst = false;
                    do{
                        for (int x=0; x<tokensReturned.Count; x++){
                            if (tokensReturned[x].lineNumber == i){
                                foundFirst = true;
                                ShapeFactory parserShapeFactory = new ShapeFactory();
                                Shape buildShape;
                                switch(tokensReturned[x].tokenType.ToString()){
                                    case "Clear": 
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 1){
                                            clearScreen(sender, drawing);
                                        }else{
                                            noParseError("Clear statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Reset": 
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 1){
                                            System.Diagnostics.Debug.WriteLine("Reset");
                                            resetPen(sender, drawing, canvasPen);
                                        }else{
                                            noParseError("Reset statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Moveto": 
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                            System.Diagnostics.Debug.WriteLine("Moveto");
                                            moveTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x+1].value), Convert.ToInt32(tokensReturned[x+3].value));
                                        }else{
                                            noParseError("Moveto statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Drawto": 
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                            System.Diagnostics.Debug.WriteLine("Drawto");
                                            drawTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x+1].value), Convert.ToInt32(tokensReturned[x+3].value));
                                        }else{
                                            noParseError("Drawto statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Circle": 
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 2){
                                            System.Diagnostics.Debug.WriteLine("Circle");
                                            buildShape = parserShapeFactory.getShape("Circle");
                                            buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x+1].value));
                                            buildShape.Draw(sender, drawing);
                                        }else{
                                            noParseError("Circle statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Rectangle": 
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                            System.Diagnostics.Debug.WriteLine("Rectangle");
                                            buildShape = parserShapeFactory.getShape("Rectangle");
                                            buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x+1].value), Convert.ToInt32(tokensReturned[x+3].value));
                                            buildShape.Draw(sender, drawing);
                                        }else{
                                            noParseError("Rectangle statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Triangle": 
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 6){
                                            System.Diagnostics.Debug.WriteLine("Triangle");
                                            buildShape = parserShapeFactory.getShape("Triangle");
                                            buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x+1].value), Convert.ToInt32(tokensReturned[x+3].value), Convert.ToInt32(tokensReturned[x+5].value));
                                            buildShape.Draw(sender, drawing);
                                        }else{
                                            noParseError("Triangle statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    default: 
                                        
                                        
                                        break;
                                }

                                
                            }
                        }
                    
                    }while(foundFirst == false);       
                    
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
        public void drawTo(Object sender, Object drawing, Object canvasPen, int x, int y){
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
        public void moveTo(Object sender, Object drawing, Object canvasPen, int x, int y){
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
