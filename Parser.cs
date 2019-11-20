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
        //private VariableStore variables = new VariableStore();
        private VariableStore variables = VariableStore.Instance;
        private MethodStore methods = MethodStore.Instance;

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

        private List<Token> allTokensOnLine(List<Token> allTokens, int lineNumber)
        {
            List<Token> returnTokens = new List<Token>();
            for (int i = 0; i < allTokens.Count; i++)
            {
                if (allTokens[i].lineNumber == lineNumber)
                {
                    returnTokens.Add(allTokens[i]); 
                }
            }
            return returnTokens; //return counter  

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

        public Boolean ValidParameters()
        {
            return true;
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
                                    if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                        try{
                                            //Checks for valid parameters
                                            if (IsComma(tokensReturned[x + 2]) == true && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) == true) { SetIdentifer(tokensReturned, new int[] { x + 1, x + 3 }); command.moveTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); }
                                            else { noParseError("Moveto statement invalid, coordinates are out of bounds on line number " + tokensReturned[x].lineNumber.ToString()); }
                                        }
                                            catch (Exception) { noParseError("MoveTo command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); } //Command invalid
                                    }
                                    else{
                                        noParseError("Moveto statement invalid on line number " + tokensReturned[x].lineNumber.ToString());
                                    }
                                    break;
                                case "Drawto": //Drawto token                  
                                    if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){
                                        try{
                                            //Checks for valid parameters
                                            if (IsComma(tokensReturned[x + 2]) == true && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) == true) { SetIdentifer(tokensReturned, new int[] { x + 1, x + 3 }); command.drawTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); }
                                            else { noParseError("Drawto statement invalid, coordinates are out of bounds on line number " + tokensReturned[x].lineNumber.ToString()); }
                                        }
                                            catch (Exception) { noParseError("DrawTo command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); }
                                    }
                                    else{
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

                                    //check if it's a method first
                                    if (methods.ReturnPosition(tokensReturned[x].name.ToUpper()) != -1)
                                    {
                                        System.Diagnostics.Debug.WriteLine("method found");

                                        //check if method has parameters or not
                                        if (methods.HasParameters(methods.ReturnPosition(tokensReturned[x].name.ToUpper())) == true)
                                        {
                                            List<string> methodParameters = methods.ReturnParameters(methods.ReturnPosition(tokensReturned[x].name.ToUpper()));

                                            //remove commas
                                            List<Token> methodCall = allTokensOnLine(tokensReturned, i);
                                            methodCall.RemoveAll(t => t.tokenType.ToString() == "Comma");
                                            //System.Diagnostics.Debug.WriteLine("Method call count: "+methodCall.Count);
                                            //1 param = 4
                                            //two param = 5
                                            //System.Diagnostics.Debug.WriteLine("Method parameters number: "+methodParameters.Count);

                                            if ((methodCall.Count - methodParameters.Count) == 3)
                                            {
                                                //check if the parameter passed in is identifer or number
                                                List<Token> passedParameters = new List<Token>();
                                                List<int> passedParametersValue = new List<int>();
                                                Boolean validAfterParse = true;
                                                System.Diagnostics.Debug.WriteLine("Method call count: "+methodCall.Count);
                                                System.Diagnostics.Debug.WriteLine("Method parameters number: " + methodParameters.Count);
                                                //int parametersEntered = 0;

                                                for (int step = 2; step < methodCall.Count - 1; step++) {
                                                    System.Diagnostics.Debug.WriteLine("Step value: " + step);
                                                    if ((methodCall[step].tokenType.ToString() == "Identifier") && (variables.ReturnPosition(methodCall[step].name.ToUpper()) != -1))
                                                    {
                                                        passedParametersValue.Add(Convert.ToInt32(methodCall[step].value));
                                                    }
                                                    else
                                                    {
                                                        if (methodCall[step].tokenType.ToString() == "IntegerLiteral")
                                                        {
                                                            passedParametersValue.Add(Convert.ToInt32(methodCall[step].value));
                                                        }
                                                        else
                                                        {
                                                            validAfterParse = false;
                                                        }

                                                    }
                                                   
                                                }

                                                if (validAfterParse == true)
                                                {
                                                    //look over and replace tokens
                                                    List<Token> methodBody = methods.GetMethodDefinition(methods.ReturnPosition(tokensReturned[x].name.ToUpper()));

                                                    System.Diagnostics.Debug.WriteLine("pp: "+ passedParametersValue.Count);
                                                    System.Diagnostics.Debug.WriteLine("mp: "+ methodParameters.Count);

                                                    if (passedParametersValue.Count == methodParameters.Count)
                                                    {
                                                        System.Diagnostics.Debug.WriteLine("good");
                                                        //have list of integer - passedParameter values
                                                        //have list of string - methodParameters
                                                        for (int tokenInBody = 0; tokenInBody < methodBody.Count; tokenInBody++)
                                                        {
                                                            if (methodBody[tokenInBody].tokenType.ToString() == "Identifier")
                                                            {
                                                                if (methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper()) != -1)
                                                                {
                                                                    methodBody[tokenInBody].tokenType = Tokens.IntegerLiteral;
                                                                    methodBody[tokenInBody].value = passedParametersValue[methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper())].ToString();

                                                                }

                                                            }
                                                        }

                                                        parseTokens(methodBody, sender, drawing, canvasPen);
                                                    }
                                                    else
                                                    {
                                                        System.Diagnostics.Debug.WriteLine("method error 2");
                                                    }
                                                    
                                                    //then call tokens
                                                }
                                                else
                                                {

                                                }
                                                System.Diagnostics.Debug.WriteLine("valid amount of parameters");

                                            }
                                            else
                                            {
                                                //System.Diagnostics.Debug.WriteLine("Invalid amount of parameters!!");
                                                noParseError("Incorrect number of parameters to method call on line " + tokensReturned[x].lineNumber.ToString());
                                                //System.Diagnostics.Debug.WriteLine(methodCall.Count);

                                            }

                                            //check rest of tokens on line to make sure there is correct number of tokens and open/close brackets
                                            //load in method tokens

                                            //loop over them and replace any occurances of the parameters with the value passed in, if the value passed in is a variable then convert the variable 
                                            // to it's integer value
                                            // allow the addition, substraction, multiplication of variables?

                                        }
                                        else
                                        {
                                            List<Token> methodCallTokens = allTokensOnLine(tokensReturned, i);
                                            //System.Diagnostics.Debug.WriteLine(methodCallTokens.Count);
                                            if (methodCallTokens.Count == 3)
                                            {
                                                if ((methodCallTokens[0].tokenType.ToString() == "Identifier") && (methodCallTokens[1].tokenType.ToString() == "OpenBracket") && (methodCallTokens[2].tokenType.ToString() == "CloseBracket"))
                                                {
                                                    parseTokens(methods.GetMethodDefinition(methods.ReturnPosition(tokensReturned[x].name.ToUpper())), sender, drawing, canvasPen);

                                                }
                                                else
                                                {
                                                    noParseError("Method not called properly on line " + tokensReturned[x].lineNumber.ToString());
                                                }
                                            }
                                            else
                                            {
                                                noParseError("Method not called properly it has no parameters on line " + tokensReturned[x].lineNumber.ToString());
                                            }
                                            
                                            //parseTokens(methods.GetMethodDefinition(methods.ReturnPosition(tokensReturned[x].name.ToUpper())), sender, drawing, canvasPen);
                                        }
                                        
                                    }
                                    else
                                    {

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
                                    }

                                        break;

                                case "Loop":

                                    List<Token> loopTokens = new List<Token>(); //List which stores all the tokens in a loop
                                    List<string> operators = new List<string>(){"GreaterThan","Equals","LessThan"};

                                    Boolean endLoopFound = false;
                                    int loopLineStart = x;
                                    int loopSetCounter = x+1;

                                    while ((endLoopFound == false) && (loopSetCounter < tokensReturned.Count)){
                                        if (tokensReturned[loopSetCounter].tokenType.ToString() == "EndLoop"){endLoopFound = true;}
                                        else{
                                            loopTokens.Add(tokensReturned[loopSetCounter]);
                                            loopSetCounter++;
                                        }
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
                                                
                                            i = loopTokens[loopTokens.Count - 1].lineNumber + 2;

                                        }
                                        else{
                                            noParseError("Loop not ended on line " + tokensReturned[loopSetCounter-1].lineNumber.ToString());
                                            i = loopTokens[loopTokens.Count-1].lineNumber+3;
                                        }
                                    }else{
                                        System.Diagnostics.Debug.WriteLine("Invalid loop");
                                    }

                                    break;


                                case "If":
                                    List<Token> ifTokens = new List<Token>(); //List which stores all the tokens in an if statement
                                    List<string> ifOperators = new List<string>() { "GreaterThan", "Equals", "LessThan" };
                                    Boolean multiline = false;
                                    int ifSetCounter = x + 1;

                                    while ((multiline == false) && (ifSetCounter < tokensReturned.Count))
                                    {
                                        if (tokensReturned[ifSetCounter].tokenType.ToString() == "EndIf") { multiline = true; }
                                        else
                                        {
                                            ifTokens.Add(tokensReturned[ifSetCounter]);
                                            ifSetCounter++;
                                        }
                                    }

                                    if (multiline == false)
                                    {
                                        ifTokens = allTokensOnLine(ifTokens, i);
                                        if ((variables.ReturnPosition(ifTokens[0].name.ToUpper()) != -1) && (ifOperators.Contains(ifTokens[1].tokenType.ToString())) && (ifTokens[2].tokenType.ToString() == "IntegerLiteral") && (ifTokens[3].tokenType.ToString() == "Then"))
                                        {
                                            switch (ifTokens[1].tokenType.ToString())
                                            {
                                                case "GreaterThan":
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) > Int32.Parse(ifTokens[2].value))
                                                    {
                                                        System.Diagnostics.Debug.WriteLine(" > is");
                                                        System.Diagnostics.Debug.WriteLine(ifTokens.Count);
                                                        //ifTokens.RemoveRange(0, 4);
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "LessThan":
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) < Int32.Parse(ifTokens[2].value))
                                                    {
                                                        System.Diagnostics.Debug.WriteLine("< is");
                                                        //ifTokens.RemoveRange(0, 4);
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "Equals":
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) == Int32.Parse(ifTokens[2].value))
                                                    {
                                                        System.Diagnostics.Debug.WriteLine("=");
                                                        //ifTokens.RemoveRange(0, 4);
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;

                                            }
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 1;
                                        }
                                        else
                                        {
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 1;
                                        }

                                        //Add all tokens on line
                                    }
                                    else
                                    {
                                        if ((variables.ReturnPosition(ifTokens[0].name.ToUpper()) != -1) && (ifOperators.Contains(ifTokens[1].tokenType.ToString())) && (ifTokens[2].tokenType.ToString() == "IntegerLiteral") && (ifTokens[3].tokenType.ToString() == "Then"))
                                        {
                                            System.Diagnostics.Debug.WriteLine(ifTokens[1].tokenType.ToString());
                                            switch (ifTokens[1].tokenType.ToString())
                                            {
                                                case "GreaterThan": 
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) > Int32.Parse(ifTokens[2].value))
                                                    {
                                                        System.Diagnostics.Debug.WriteLine(" > is");
                                                        System.Diagnostics.Debug.WriteLine(ifTokens.Count);
                                                        //ifTokens.RemoveRange(0, 4);
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "LessThan":
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) < Int32.Parse(ifTokens[2].value))
                                                    {
                                                        System.Diagnostics.Debug.WriteLine("< is");
                                                        //ifTokens.RemoveRange(0, 4);
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "Equals":
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) == Int32.Parse(ifTokens[2].value))
                                                    {
                                                        System.Diagnostics.Debug.WriteLine("=");
                                                        //ifTokens.RemoveRange(0, 4);
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;

                                            }
                                            System.Diagnostics.Debug.WriteLine("valid multiline if");
                                            
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 2;
                                            System.Diagnostics.Debug.WriteLine(i);
                                        }
                                        else
                                        {
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 2;
                                        }
                                    }


                                    break;

                                case "Method":
                                    System.Diagnostics.Debug.WriteLine("Method declaration line");
                                    List<Token> methodHeaderTokens = new List<Token>();
                                    List<Token> methodBodyTokens = new List<Token>(); //List which stores all the tokens in the method declaration statement
                                    methodHeaderTokens = allTokensOnLine(tokensReturned, i);
                                    //System.Diagnostics.Debug.WriteLine(methodHeaderTokens.Count);
                                    
                                    /*
                                    for (int c = 0; c < methodHeaderTokens.Count; c++)
                                    {
                                        System.Diagnostics.Debug.WriteLine("H: "+methodHeaderTokens[c].tokenType.ToString());
                                    }
                                    */

                                    //check for open close brackets
                                    int openBracketIndex = methodHeaderTokens.FindIndex(m => m.tokenType.ToString() == "OpenBracket");
                                    int closeBracketIndex = methodHeaderTokens.FindIndex(m => m.tokenType.ToString() == "CloseBracket");
                                    List<Token> parameters = methodHeaderTokens.GetRange(openBracketIndex, (closeBracketIndex - openBracketIndex));
                                    List<string> namedParameters = new List<string>();
                                    Boolean validParameters = true;
                                    Boolean methodAlreadyDefined = methods.MethodExists(methodHeaderTokens[1].name.ToUpper());
                                    //System.Diagnostics.Debug.WriteLine("Parameters length: "+parameters.Count);
                                    if (parameters.Count > 1)
                                    {
                                        parameters.RemoveAll(t => t.tokenType.ToString() == "Comma");
                                        for (int p=1; p<parameters.Count; p++)
                                        {
                                            System.Diagnostics.Debug.WriteLine("Parameter: " + parameters[p].tokenType.ToString());
                                            if (parameters[p].tokenType.ToString() == "Identifier")
                                            {
                                                namedParameters.Add(parameters[p].name.ToUpper());
                                            }
                                            else
                                            {
                                                validParameters = false;
                                                noParseError("Parameter declared in method is not an identifier " + tokensReturned[x].lineNumber.ToString());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //empty list
                                    }
                                    Boolean endMethodFound = false;
                                    //int methodLineStart = x+1;
                                    int methodSetCounter = x + methodHeaderTokens.Count;
                                    //int methodSetCounter = x + 4; //for method with no parameters (dynamically set this depending on parameter list

                                    while ((endMethodFound == false) && (methodSetCounter < tokensReturned.Count))
                                    {
                                        //System.Diagnostics.Debug.WriteLine(endMethodFound);
                                        if (tokensReturned[methodSetCounter].tokenType.ToString() == "EndMethod") { endMethodFound = true; }
                                        else
                                        {
                                            methodBodyTokens.Add(tokensReturned[methodSetCounter]);
                                            methodSetCounter++;
                                        }
                                    }

                                    /*
                                    for (int o = 0; o < methodBodyTokens.Count; o++)
                                    {
                                        System.Diagnostics.Debug.WriteLine("B: "+methodBodyTokens[o].tokenType.ToString());
                                    }
                                    */
                                    //System.Diagnostics.Debug.WriteLine(endMethodFound);
                                    if (endMethodFound == true && validParameters == true){
                                        
                                        
                                            System.Diagnostics.Debug.WriteLine("Method created");
                                            methods.AddMethod(methodHeaderTokens[1].name.ToUpper(), namedParameters, methodBodyTokens);
                                        
                                        
                                        //System.Diagnostics.Debug.WriteLine(methodHeaderTokens[1].name.ToUpper());
                                        //methods.AddMethod(methodHeaderTokens[1].name.ToUpper(), namedParameters , methodBodyTokens);
                                    }else{
                                        noParseError("Method declaration not ended on line " + tokensReturned[x].lineNumber.ToString());
                                    }

                                    if (methodBodyTokens.Count == 0){
                                        //System.Diagnostics.Debug.WriteLine(methodBodyTokens.Count);
                                        i = tokensReturned[x].lineNumber + 2;
                                    }else{
                                        i = methodBodyTokens[methodBodyTokens.Count - 1].lineNumber + 2;
                                    }

                                    /*
                                    List<Token> test = methods.GetMethodDefinition(0);
                                    for (int k=0; k< test.Count; k++)
                                    {
                                        System.Diagnostics.Debug.WriteLine(test[k].tokenType.ToString());
                                    }
                                    */
                                    //System.Diagnostics.Debug.WriteLine(methods.GetMethodDefinition(0));
                                




                                    //check syntax
                                    // method space (....)
                                    //check method doesn't already exsist
                                    break;

                                default: //Token not recognised
                                    if (tokensReturned[x].tokenType.ToString() == "Undefined"){
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

