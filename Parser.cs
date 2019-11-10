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
        private VariableStore variables = new VariableStore();

        private void SetIdentifer(List<Token> allTokens, int[] positions)
        {
            for (int i=0; i<positions.Length; i++)
            {
                if (allTokens[positions[i]].tokenType.ToString() == "Identifier")
                {
                    switch (variables.ReturnPosition(allTokens[positions[i]].name.ToUpper()))
                    {
                        case -1: noParseError("Can't pass undeclared parameter on line " + allTokens[positions[i]].lineNumber.ToString()); break;
                        default: allTokens[positions[i]].value = Convert.ToString(variables.ReturnValue(variables.ReturnPosition(allTokens[positions[i]].name.ToUpper()))); break;
                    }
                }

            }
            
        }

        
        
        
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
                    count++; //If line number matches one given then increment counter
                }
            }
            return count; //return counter           
        }

        /// <summary>
        /// Checks if the token passed in is a comma.
        /// </summary>
        /// <param name="token">A given token that you want to check.</param>
        /// <returns>True if the token is a comma and false if it's not.</returns>
        private Boolean IsComma(Token token)
        {
            if (token.tokenType.ToString() == "Comma"){return true;} //string is comma
            else{return false;} //string is not comma
        }

        

        /// <summary>
        /// Main function of the parser class. Responsible for parsing the text and actioning appropriate commands.
        /// </summary>
        /// <param name="commands">The command(s) to parse. These come from the text inputs.</param>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to draw on.</param>
        /// <param name="canvasPen">The pen object to update which stores the x,y coordinates.</param>
        public void parseText(string commands, Object sender, Object drawing, Object canvasPen) {

            DrawingPen local = (DrawingPen)canvasPen; //Local object of pen
            Commands command = new Commands();

            List<Token> tokensReturned = new List<Token>(); //List which stores all the tokens returned

            if (commands.Length >= 1)
            {
                lexer = new Lexer(commands); //New lexer object
                Token getNextToken = lexer.CreateToken();

                //Generates all of the tokens for the piece of text given
                while (getNextToken.tokenType != Tokens.EOF)
                {
                    tokensReturned.Add(getNextToken);
                    getNextToken = lexer.CreateToken();
                }

                tokensReturned.Add(getNextToken); //Adds a token to the list

                //Removes newline, whitespace and end of file characters from the list
                tokensReturned.RemoveAll(t => t.tokenType.ToString() == "NewLine" || t.tokenType.ToString() == "WhiteSpace" || t.tokenType.ToString() == "EOF");

                int maxLineNumber = tokensReturned[tokensReturned.Count - 1].lineNumber; //The number of lines in the program

                parseFull(sender, drawing, canvasPen, tokensReturned, maxLineNumber);

            }
        }

        private void parseTokens(List<Token> tokenList, Object sender, Object drawing, Object canvasPen)
        {
            List<Token> tokensReturned = tokenList; //List which stores all the tokens returned

            //Removes newline, whitespace and end of file characters from the list
            tokensReturned.RemoveAll(t => t.tokenType.ToString() == "NewLine" || t.tokenType.ToString() == "WhiteSpace" || t.tokenType.ToString() == "EOF");

            int maxLineNumber = tokensReturned[tokensReturned.Count - 1].lineNumber; //The number of lines in the program

            parseFull(sender, drawing, canvasPen, tokensReturned, maxLineNumber);

        }

    

        public void parseFull(Object sender, Object drawing, Object canvasPen, List<Token> tokensReturned, int maxLineNumber) {

            

            DrawingPen local = (DrawingPen)canvasPen; //Local object of pen
            Commands command = new Commands();


            /* for each line */
            int i = 1;
            while (i < maxLineNumber+1) { 
            //for (int i=1; i<maxLineNumber+1; i++){
                
                Boolean foundFirst = false; //Variable to store if the first token has been found on a line
                do{
                    for (int x=0; x<tokensReturned.Count; x++){
                        if (tokensReturned[x].lineNumber == i){
                            foundFirst = true; //First token found
                            ShapeFactory parserShapeFactory = new ShapeFactory(); //New shape factory object
                            Shape buildShape;
                            switch (tokensReturned[x].tokenType.ToString()){
                                case "Clear": //Clear token 
                                    if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 1) { command.clearScreen(sender, drawing); } //Clears the screen
                                    else { noParseError("Clear statement invalid on line number " + tokensReturned[x].lineNumber.ToString()); } //Error message displayed if more than one token on line
                                    break;
                                case "Reset": //Reset token
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 1) { command.resetPen(sender, drawing, canvasPen); } //Resets the screen
                                        else { noParseError("Reset statement invalid on line number " + tokensReturned[x].lineNumber.ToString()); } //Error message displayed if more than one token on line
                                        break;
                                    case "Moveto": //Moveto token
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4)
                                        {
                                            try
                                            {
                                                //Checks for valid parameters
                                                if (IsComma(tokensReturned[x + 2]) == true && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) == true) { SetIdentifer(tokensReturned, new int[] { x + 1, x + 3 }); command.moveTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); }
                                                else { noParseError("Moveto statement invalid, coordinates are out of bounds on line number " + tokensReturned[x].lineNumber.ToString()); }
                                            }
                                            catch (Exception) { noParseError("MoveTo command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); } //Command invalid
                                        }
                                        else
                                        {
                                            noParseError("Moveto statement invalid on line number " + tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Drawto": //Drawto token                  
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4)
                                        {
                                            try
                                            {
                                                //Checks for valid parameters
                                                if (IsComma(tokensReturned[x + 2]) == true && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) == true) { SetIdentifer(tokensReturned, new int[] { x + 1, x + 3 }); command.drawTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); }
                                                else { noParseError("Drawto statement invalid, coordinates are out of bounds on line number " + tokensReturned[x].lineNumber.ToString()); }
                                            }
                                            catch (Exception) { noParseError("DrawTo command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); }
                                        }
                                        else
                                        {
                                            noParseError("Drawto statement invalid on line number " + tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Circle": //Circle token 
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 2)
                                        {
                                            
                                            SetIdentifer(tokensReturned, new int[] {x+1});

                                            try
                                            {
                                                
                                                //Checks for in bounds
                                                if (local.CheckDimensions(local.xCoordinate + (Convert.ToInt32(tokensReturned[x + 1].value) * 2), local.yCoordinate + (Convert.ToInt32(tokensReturned[x + 1].value) * 2)))
                                                {


                                                    buildShape = parserShapeFactory.getShape("Circle"); //makes shape
                                                    buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value)); //sets parameters
                                                    buildShape.Draw(sender, drawing); //draws circle
                                                }
                                                else { noParseError("Circle's dimensions out of bounds"); }
                                            }
                                            catch (Exception) { noParseError("Circle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take an integer parameter."); }
                                        }
                                        else
                                        {
                                            noParseError("Circle statement invalid on line number " + tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Rectangle": //Rectangle token    
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4)
                                        {
                                            SetIdentifer(tokensReturned, new int[] { x + 1, x + 3});
                                            if (IsComma(tokensReturned[x + 2]) == true)
                                            {
                                                try
                                                {
                                                    //Checks for in bounds
                                                    if (local.CheckDimensions(local.xCoordinate + (Convert.ToInt32(tokensReturned[x + 1].value)), local.yCoordinate + (Convert.ToInt32(tokensReturned[x + 3].value))))
                                                    {
                                                        buildShape = parserShapeFactory.getShape("Rectangle");
                                                        buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value));
                                                        buildShape.Draw(sender, drawing);
                                                    }
                                                    else { noParseError("Rectangle's dimensions out of bounds"); }
                                                }
                                                catch (Exception) { noParseError("Rectangle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); }
                                            }
                                        }
                                        else
                                        {
                                            noParseError("Rectangle statement invalid on line number " + tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Triangle": //Triangle token          
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 6)
                                        {
                                            SetIdentifer(tokensReturned, new int[] { x + 1, x + 3, x + 5});

                                            if (IsComma(tokensReturned[x + 2]) == true && IsComma(tokensReturned[x + 4]) == true)
                                            {
                                                try
                                                {
                                                    //Checks for in bounds
                                                    if (local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 5].value), local.yCoordinate))
                                                    {
                                                        buildShape = parserShapeFactory.getShape("Triangle");
                                                        buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value), Convert.ToInt32(tokensReturned[x + 5].value));
                                                        buildShape.Draw(sender, drawing);
                                                    }
                                                    else { noParseError("Triangle's dimensions out of bounds"); }
                                                }
                                                catch (Exception) { noParseError("Triangle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); }
                                            }
                                        }
                                        else
                                        {
                                            noParseError("Triangle statement invalid on line number " + tokensReturned[x].lineNumber.ToString());
                                        }
                                        break;
                                    case "Identifier":
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 3)
                                        {
                                            if (variables.ReturnPosition(tokensReturned[x].name.ToUpper()) != -1)
                                            {
                                                switch (tokensReturned[x + 1].tokenType.ToString())
                                                {
                                                    case "Equals": variables.Assign(variables.ReturnPosition(tokensReturned[x].name.ToUpper()), Convert.ToInt32(tokensReturned[x + 2].value)); break;
                                                    case "Plus": variables.Addition(variables.ReturnPosition(tokensReturned[x].name.ToUpper()), Convert.ToInt32(tokensReturned[x + 2].value)); break;
                                                    case "Minus": variables.Subtract(variables.ReturnPosition(tokensReturned[x].name.ToUpper()), Convert.ToInt32(tokensReturned[x + 2].value)); break;
                                                    case "Multiply": variables.Multiply(variables.ReturnPosition(tokensReturned[x].name.ToUpper()), Convert.ToInt32(tokensReturned[x + 2].value)); break;
                                                    default: noParseError("Invalid operator on variable on line " + tokensReturned[x].lineNumber.ToString()); break;

                                                }

                                            }
                                            else
                                            {
                                                if (tokensReturned[x + 1].tokenType.ToString() == "Equals")
                                                {
                                                    tokensReturned[x].value = tokensReturned[x + 2].value;
                                                    variables.AddVariable(tokensReturned[x]);
                                                }
                                                else
                                                {
                                                    noParseError("Variable doesn't exist on line " + tokensReturned[x].lineNumber.ToString());
                                                }
                                                
                                            }
                                        }

                                        break;
                                    case "Loop":

                                        List<Token> loopTokens = new List<Token>(); //List which stores all the tokens in a loop
                                    List<string> operators = new List<string>(){"GreaterThan","Equals","LessThan"};

                                        Boolean endLoopFound = false;
                                        int loopLineStart = x;
                                        int loopSetCounter = x+1;

                                        while ((endLoopFound == false) && (loopSetCounter < tokensReturned.Count))
                                        {
                                            if (tokensReturned[loopSetCounter].tokenType.ToString() == "EndLoop"){endLoopFound = true;}
                                            else
                                            {
                                                loopTokens.Add(tokensReturned[loopSetCounter]);
                                                loopSetCounter++;
                                            }

                                            

                                        }

                                        //validate loop start
                                        for (int v=0; v<loopTokens.Count; v++){
                                            System.Diagnostics.Debug.Write(loopTokens[v].tokenType.ToString());
                                        }

                                    
                                        if ((loopTokens[0].tokenType.ToString() == "While") && (variables.ReturnPosition(loopTokens[1].name.ToUpper()) != -1) && (operators.Contains(loopTokens[2].tokenType.ToString())) && (loopTokens[3].tokenType.ToString() == "IntegerLiteral") ){
                                            System.Diagnostics.Debug.WriteLine("valid loop");

                                            
                                            
                                            if (endLoopFound == true){

                                                switch(loopTokens[2].tokenType.ToString()){
                                                    case "Equals": 
                                                        while (variables.ReturnValue(variables.ReturnPosition(loopTokens[1].name.ToUpper())) == Int32.Parse(loopTokens[3].value)){
                                                            parseTokens(loopTokens, sender, drawing, canvasPen);
                                                        }
                                                    break;
                                                    case "GreaterThan": 
                                                    while (variables.ReturnValue(variables.ReturnPosition(loopTokens[1].name.ToUpper())) > Int32.Parse(loopTokens[3].value)){
                                                            parseTokens(loopTokens, sender, drawing, canvasPen);
                                                        }
                                                    break;
                                                    case "LessThan": 
                                                    while (variables.ReturnValue(variables.ReturnPosition(loopTokens[1].name.ToUpper())) < Int32.Parse(loopTokens[3].value)){
                                                            parseTokens(loopTokens, sender, drawing, canvasPen);
                                                        }
                                                    break;

                                        
                                                }
                                                //parseTokens(loopTokens, sender, drawing, canvasPen);
                                                i = loopTokens[loopTokens.Count - 1].lineNumber + 2;

                                            }
                                            else{
                                                noParseError("Loop not ended on line " + tokensReturned[loopSetCounter-1].lineNumber.ToString());
                                                i = loopTokens[loopTokens.Count-1].lineNumber+3;
                                            }
                                        }else{
                                        System.Diagnostics.Debug.WriteLine("Invalid loop");
                                    }

                                        //check if loop ended
                                        

                                    
                                       

                                        break;


                                    case "If": break;

                                    case "Method": break;

                                    default: //Token not recognised
                                        if (tokensReturned[x].tokenType.ToString() == "Undefined")
                                        {
                                            noParseError("Syntax error. Command not recognised on line number " + tokensReturned[x].lineNumber.ToString());
                                        }

                                        break;
                                }
                            }
                            //empty line encountered
                            else { foundFirst = true; }

                        
                        }
                    }while(foundFirst == false);
                i++;
                }
            
            }    
        
        }

        
    }

