using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ase
{
    /// <summary>
    /// The logic of the program. It interprets the tokens recieved back from the lexer and actions the appropriate commands.
    /// </summary>
    class Parser
    {

        private Lexer lexer; //private instance of the lexer for use by the whole class
        
        /// <summary>
        /// Displays and error message if the parser can't interpret the commands.
        /// </summary>
        /// <param name="errorMessage">The error message to be displayed the user.</param>
        private void noParseError(string errorMessage){
            const string title = "Invalid Statement Error";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(errorMessage, title, buttons, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Counts the number of tokens on a given line. Used for the error checking process.
        /// </summary>
        /// <param name="tokens">The token list created by the calling the lexer.</param>
        /// <param name="lineNumber">The line number to count how many tokens are on it.</param>
        /// <returns>Number of tokens on the given line.</returns>
        private int tokensOnLine(List<Token> tokens, int lineNumber){
            int count = 0;
            for (int i=0; i<tokens.Count; i++){
                if(tokens[i].lineNumber == lineNumber){
                    count++;
                }
            }

            return count;
            
        }

        /// <summary>
        /// Checks if the token passed in is a comma.
        /// </summary>
        /// <param name="token">A given token that you want to check.</param>
        /// <returns>True if the token is a comma and false if it's not.</returns>
        private Boolean IsComma(Token token)
        {
            if (token.tokenType.ToString() == "Comma"){return true;}
            else{return false;}
        }

        /// <summary>
        /// Main function of the parser class. Responsible for parsing the text and actioning appropriate commands.
        /// </summary>
        /// <param name="commands">The command(s) to parse. These come from the text inputs.</param>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to draw on.</param>
        /// <param name="canvasPen">The pen object to update which stores the x,y coordinates.</param>
        public void parseText(string commands, Object sender, Object drawing, Object canvasPen){

            DrawingPen local = (DrawingPen)canvasPen;

            List<Token> tokensReturned = new List<Token>();
            
            if (commands.Length >= 1){
                lexer = new Lexer(commands); //New lexer object
                Token getNextToken = lexer.CreateToken();

                //Generates all of the tokens for the piece of text given
                while (getNextToken.tokenType != Tokens.EOF){
                    tokensReturned.Add(getNextToken);
                    getNextToken = lexer.CreateToken();
                }

                tokensReturned.Add(getNextToken); //Adds a token to the list

                //Removes newline, whitespace and end of file characters from the list
                tokensReturned.RemoveAll(t => t.tokenType.ToString() == "NewLine" || t.tokenType.ToString() == "WhiteSpace" || t.tokenType.ToString() == "EOF");

                int maxLineNumber  = tokensReturned[tokensReturned.Count - 1].lineNumber; //The number of lines in the program


                /* for each line */
                for (int i=1; i<maxLineNumber+1; i++){
                
                    Boolean foundFirst = false; //Variable to store if the first token has been found on a line
                    do{
                        for (int x=0; x<tokensReturned.Count; x++){
                            if (tokensReturned[x].lineNumber == i){
                                foundFirst = true; //First token found
                                ShapeFactory parserShapeFactory = new ShapeFactory(); //New shape factory object
                                Shape buildShape;
                                switch(tokensReturned[x].tokenType.ToString()){
                                    case "Clear": //Clear token 
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 1){clearScreen(sender, drawing);}
                                        else{noParseError("Clear statement invalid on line number "+tokensReturned[x].lineNumber.ToString());}
                                        break;
                                    case "Reset": //Reset token
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 1){ resetPen(sender, drawing, canvasPen);}
                                        else{noParseError("Reset statement invalid on line number "+tokensReturned[x].lineNumber.ToString());}
                                        break;
                                    case "Moveto": //Moveto token
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                            try
                                            {
                                                if (IsComma(tokensReturned[x + 2]) == true && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) == true) { moveTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); }
                                                else { noParseError("Moveto statement invalid, coordinates are out of bounds on line number " + tokensReturned[x].lineNumber.ToString());}
                                            }
                                            catch (Exception) { noParseError("MoveTo command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); }
                                        }
                                        else{
                                            noParseError("Moveto statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Drawto": //Drawto token
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                            try
                                            {
                                                if (IsComma(tokensReturned[x + 2]) == true && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) == true) { drawTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); }
                                                else { noParseError("Drawto statement invalid, coordinates are out of bounds on line number " + tokensReturned[x].lineNumber.ToString()); }
                                            }
                                            catch (Exception) { noParseError("DrawTo command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); }
                                        }
                                        else{
                                            noParseError("Drawto statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Circle": //Circle token
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 2){
                                            try
                                            {
                                                buildShape = parserShapeFactory.getShape("Circle");
                                                buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value));
                                                buildShape.Draw(sender, drawing);
                                            }
                                            catch (Exception) { noParseError("Circle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take an integer parameter."); }
                                        }
                                        else{
                                            noParseError("Circle statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Rectangle": //Rectangle token
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){

                                            if (IsComma(tokensReturned[x + 2]) == true)
                                            {
                                                try
                                                {
                                                    buildShape = parserShapeFactory.getShape("Rectangle");
                                                    buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value));
                                                    buildShape.Draw(sender, drawing);
                                                }
                                                catch (Exception){noParseError("Rectangle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters.");}
                                            }
                                        }else{
                                            noParseError("Rectangle statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Triangle": //Triangle token
                                        
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 6){
                                            if (IsComma(tokensReturned[x + 2]) == true && IsComma(tokensReturned[x + 4]) == true)
                                            {
                                                try
                                                {
                                                    buildShape = parserShapeFactory.getShape("Triangle");
                                                    buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value), Convert.ToInt32(tokensReturned[x + 5].value));
                                                    buildShape.Draw(sender, drawing);
                                                }
                                                catch (Exception) { noParseError("Triangle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); }
                                            }
                                        }else{
                                            noParseError("Triangle statement invalid on line number "+tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    default: //Token not recognised
                                        if (tokensReturned[x].tokenType.ToString() == "Undefined")
                                        {
                                            noParseError("Syntax error. Command not recognised on line number " + tokensReturned[x].lineNumber.ToString());
                                        }
                                        

                                        break;
                                }

                                
                            }
                        }
                    
                    }while(foundFirst == false);       
                    
                }
            }   

            
        }

        /// <summary>
        /// Clears the screen of any drawings made.
        /// </summary>
        /// <param name="sender">The canvas.</param>
        /// <param name="drawing">The bitmap image.</param>
        private void clearScreen(Object sender, Object drawing){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.Transparent);
            canvas.Image = image;
            g.Dispose();
        }

        /// <summary>
        /// Resets the coordinates of the pen to x:0 and y:0.
        /// </summary>
        /// <param name="sender">The canvas.</param>
        /// <param name="drawing">The bitmap image.</param>
        /// <param name="canvasPen">The pen object where the x and y coordinates are stored.</param>
        public void resetPen(Object sender, Object drawing, Object canvasPen){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            g.TranslateTransform(0, 0);

            local.xCoordinate = 0; //Reset x coordinate to 0
            local.yCoordinate = 0; //Reset y coordinate to 0
            canvas.Image = image; //Update image
        }

        /// <summary>
        /// Draws a line from the current x,y coordinates to the given x,y coordinates.
        /// </summary>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to darw on.</param>
        /// <param name="canvasPen">The pen object which stores x,y coordinates.</param>
        /// <param name="x">The x coordinate to draw to.</param>
        /// <param name="y">The y coordinate to draw to.</param>
        public void drawTo(Object sender, Object drawing, Object canvasPen, int x, int y){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            g.DrawLine(new Pen(Color.Black), local.xCoordinate, local.yCoordinate, x, y); //Draw line command
            local.xCoordinate = x;
            local.yCoordinate = y;
            canvas.Image = image; //Updates the image

        }

        /// <summary>
        /// Moves the pen to a given x and y coordinate.
        /// </summary>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to draw on.</param>
        /// <param name="canvasPen">The pen object.</param>
        /// <param name="x">X coordinate to move to.</param>
        /// <param name="y">Y coordinate to move to.</param>
        public void moveTo(Object sender, Object drawing, Object canvasPen, int x, int y){
            PictureBox canvas = (PictureBox)sender;
            Bitmap image = (Bitmap)drawing;
            DrawingPen local = (DrawingPen)canvasPen;
            Graphics g = Graphics.FromImage(image);

            g.TranslateTransform(x, y); //Sets x and y coordinates
            local.xCoordinate = x; //Updates the x coordinate in the pen object
            local.yCoordinate = y; //Updates the y coordinate in the pen object
            canvas.Image = image; //Updates the image
        }
    }
}
