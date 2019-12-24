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
        private VariableStore variables = VariableStore.Instance; //singleton instance of variables
        private MethodStore methods = MethodStore.Instance; //singleton instance of methods

        /// <summary>
        /// Gets the value of an identifier if it's a variable and sets the value of it to a token.
        /// </summary>
        /// <param name="allTokens">The tokens to check if they are identifiers</param>
        /// <param name="positions">Positions of the tokens to check.</param>
        private void SetIdentifer(List<Token> allTokens, int[] positions)
        {
            for (int i=0; i<positions.Length; i++) //loops over list of tokens
            {
                if (allTokens[positions[i]].tokenType.ToString() == "Identifier") //checks to see if its a identifer
                {
                    switch (variables.ReturnPosition(allTokens[positions[i]].name.ToUpper())) //convert variable name to upper to check
                    {
                        case -1: noParseError("Can't pass undeclared parameter on line " + allTokens[positions[i]].lineNumber.ToString()); break; //variable not declared
                        default: allTokens[positions[i]].value = Convert.ToString(variables.ReturnValue(variables.ReturnPosition(allTokens[positions[i]].name.ToUpper()))); break; //variable declared
                    }
                }

            }
            
        }

        /// <summary>
        /// Returns a list of tokens that are on a given line number.
        /// </summary>
        /// <param name="allTokens">The token list to search through.</param>
        /// <param name="lineNumber">The line number to check against.</param>
        /// <returns>A list of tokens that are on the line number.</returns>
        private List<Token> allTokensOnLine(List<Token> allTokens, int lineNumber)
        {
            List<Token> returnTokens = new List<Token>();
            for (int i = 0; i < allTokens.Count; i++)
            {
                if (allTokens[i].lineNumber == lineNumber) //line numbers match
                {
                    returnTokens.Add(allTokens[i]); //adds matching token to the list
                }
            }
            return returnTokens; //return token list 

        }
        
        
        /// <summary>
        /// Displays and error message if the parser can't interpret the commands.
        /// </summary>
        /// <param name="errorMessage">The error message to be displayed the user.</param>
        private void noParseError(string errorMessage){
            const string title = "Invalid Statement Error";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(errorMessage, title, buttons, MessageBoxIcon.Error); //displays the error message on the screen
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
        /// Parses text to commands
        /// </summary>
        /// <param name="commands">The command(s) to parse. These come from the text inputs.</param>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to draw on.</param>
        /// <param name="canvasPen">The pen object to update which stores the x,y coordinates.</param>
        public void parseText(string commands, Object sender, Object drawing, Object canvasPen) {

            methods.ClearDown(); //reset saved methods
            variables.ClearDown(); //reset saved variables

            DrawingPen local = (DrawingPen)canvasPen; //Local object of pen
            Commands command = new Commands();

            List<Token> tokensReturned = new List<Token>(); //List which stores all the tokens returned

            if (commands.Length >= 1)
            {
                lexer = new Lexer(commands); //New lexer object
                Token getNextToken = lexer.CreateToken(); //gets the first token

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

                parseFull(sender, drawing, canvasPen, tokensReturned, maxLineNumber); //Parse

            }
        }

        /// <summary>
        /// Parses tokens to commands.
        /// </summary>
        /// <param name="tokenList">The list of tokens to parse.</param>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to draw on.</param>
        /// <param name="canvasPen">The pen object to update which stores the x,y coordinates.</param>
        private void parseTokens(List<Token> tokenList, Object sender, Object drawing, Object canvasPen)
        {
            List<Token> tokensReturned = tokenList; //List which stores all the tokens returned

            //Removes newline, whitespace and end of file characters from the list
            tokensReturned.RemoveAll(t => t.tokenType.ToString() == "NewLine" || t.tokenType.ToString() == "WhiteSpace" || t.tokenType.ToString() == "EOF");

            int maxLineNumber = tokensReturned[tokensReturned.Count - 1].lineNumber; //The number of lines in the program
            //int maxLineNumber = tokensReturned[tokensReturned.Count].lineNumber;
            parseFull(sender, drawing, canvasPen, tokensReturned, maxLineNumber); //Parse

        }

    
        /// <summary>
        /// The main parsing function of the program. Tokens are converted into commands and executed.
        /// </summary>
        /// <param name="sender">The canvas to draw on.</param>
        /// <param name="drawing">The image to draw on.</param>
        /// <param name="canvasPen">The pen object to update which stores the x,y coordinates.</param>
        /// <param name="tokensReturned">The tokens that represent the commands.</param>
        /// <param name="maxLineNumber">The number of lines in the program.</param>
        public void parseFull(Object sender, Object drawing, Object canvasPen, List<Token> tokensReturned, int maxLineNumber) {

            DrawingPen local = (DrawingPen)canvasPen; //Local object of pen
            Commands command = new Commands(); //commands


            //for each line
            int i = 1;
            while (i < maxLineNumber+1) { 
                
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
                                    if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){ //tokens on line check
                                        try{
                                            //Checks for valid parameters
                                            if (IsComma(tokensReturned[x + 2]) == true && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) == true) { SetIdentifer(tokensReturned, new int[] { x + 1, x + 3 }); command.moveTo(sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); }
                                            else { noParseError("Moveto statement invalid, coordinates are out of bounds on line number " + tokensReturned[x].lineNumber.ToString()); } //error message
                                        }
                                            catch (Exception) { noParseError("MoveTo command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); } //Command invalid
                                    }
                                    else{
                                        noParseError("Moveto statement invalid on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                    }
                                    break;
                                case "Drawto": //Drawto token                  
                                    if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4){ //tokens on line check
                                        try{
                                            //Checks for valid parameters
                                            if (IsComma(tokensReturned[x + 2]) == true && local.CheckDimensions(Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)) == true) { SetIdentifer(tokensReturned, new int[] { x + 1, x + 3 }); command.drawTo(local, sender, drawing, canvasPen, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); }
                                            else { noParseError("Drawto statement invalid, coordinates are out of bounds on line number " + tokensReturned[x].lineNumber.ToString()); } //error message
                                        }
                                            catch (Exception) { noParseError("DrawTo command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); } //command invalid
                                    }
                                    else{
                                        noParseError("Drawto statement invalid on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                    }
                                    break;
                                case "Circle": //Circle token 
                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 2) //tokens on line check
                                        {
                                            
                                            SetIdentifer(tokensReturned, new int[] {x+1}); //get value

                                            try
                                            {
                                                
                                                //Checks for in bounds
                                                if (local.CheckDimensions(local.xCoordinate + (Convert.ToInt32(tokensReturned[x + 1].value) * 2), local.yCoordinate + (Convert.ToInt32(tokensReturned[x + 1].value) * 2)))
                                                {


                                                    buildShape = parserShapeFactory.getShape("Circle"); //makes shape
                                                    buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value)); //sets parameters
                                               
                                                buildShape.Draw(local, sender, drawing); //draws circle
                                                }
                                                else { noParseError("Circle's dimensions out of bounds"); } //error message
                                            }
                                            catch (Exception) { noParseError("Circle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take an integer parameter."); } //error message
                                        }
                                        else
                                        {
                                            noParseError("Circle statement invalid on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                        }
                                        break;
                                case "Rectangle": //Rectangle token    
                                    if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 4) //tokens on line check
                                        {
                                            SetIdentifer(tokensReturned, new int[] { x + 1, x + 3}); //get value
                                            if (IsComma(tokensReturned[x + 2]) == true) //syntax checking for a comma
                                            {
                                                try
                                                {
                                                    //Checks for in bounds
                                                    if (local.CheckDimensions(local.xCoordinate + (Convert.ToInt32(tokensReturned[x + 1].value)), local.yCoordinate + (Convert.ToInt32(tokensReturned[x + 3].value))))
                                                    {
                                                        buildShape = parserShapeFactory.getShape("Rectangle"); //makes shape
                                                        buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value)); //sets parameters
                                                        buildShape.Draw(local, sender, drawing); //draws rectangle
                                                    }
                                                    else { noParseError("Rectangle's dimensions out of bounds"); } //error message
                                                }
                                                catch (Exception) { noParseError("Rectangle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); } //error message
                                            }
                                        }
                                        else
                                        {
                                            noParseError("Rectangle statement invalid on line number " + tokensReturned[x].lineNumber.ToString()); //error message
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
                                                        buildShape = parserShapeFactory.getShape("Triangle"); //creates shape
                                                        buildShape.Set(local.xCoordinate, local.yCoordinate, Convert.ToInt32(tokensReturned[x + 1].value), Convert.ToInt32(tokensReturned[x + 3].value), Convert.ToInt32(tokensReturned[x + 5].value)); //sets shape coordinates
                                                        buildShape.Draw(local, sender, drawing); //draws the shape
                                                    }
                                                    else { noParseError("Triangle's dimensions are out of bounds"); } //error message
                                                }
                                                catch (Exception) { noParseError("Triangle command on line " + tokensReturned[x].lineNumber.ToString() + " can only take integer parameters."); } //error message
                                            }
                                        }
                                        else
                                        {
                                            noParseError("Triangle statement invalid on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                        }
                                        break;

                                case "Polygon": //Polygon token
                                    if (((tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) % 2) == 0) && (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) >= 4)) //check correct number of tokens on the line
                                    {
                                        try
                                        {
                                            int arraySize = 0; //array size to pass
                                            
                                            //calculate array size
                                            for (int polygonParam = 1; polygonParam < tokensOnLine(tokensReturned, tokensReturned[x].lineNumber); polygonParam += 2)
                                            {
                                                arraySize++; //increment array size

                                            }

                                            
                                            //tokens for polygon
                                            int[] tokensForPoly = new int[arraySize];

                                            //values for polygon

                                            int polyCounter = 0; //counter variable
                                            //set items in array
                                            for (int polygonParam = 1; polygonParam < tokensOnLine(tokensReturned, tokensReturned[x].lineNumber); polygonParam += 2)
                                            {
                                                tokensForPoly[polyCounter] = x + polygonParam;
                                                polyCounter++;

                                            }

                                            //set identifiers
                                            SetIdentifer(tokensReturned, tokensForPoly);
                                            bool validPolyComma = true; //control variable for commas
                                            bool validPolyBounds = true; //control variable for bounds (x,y)

                                            //comma check
                                            for (int polygonParam = 2; polygonParam < tokensOnLine(tokensReturned, tokensReturned[x].lineNumber); polygonParam += 2)
                                            {
                                                if (IsComma(tokensReturned[polygonParam]) == false)
                                                {
                                                    validPolyComma = false;
                                                }
                                                

                                            }

                                            int[] polygonValues = new int[tokensForPoly.Length + 2];
                                            polygonValues[0] = local.xCoordinate;
                                            polygonValues[1] = local.yCoordinate;
                                            int polyCount = 2;

                                            //dimensions check
                                            for (int polygonParam = 1; polygonParam < tokensOnLine(tokensReturned, tokensReturned[x].lineNumber)-2; polygonParam += 2)
                                            {
                                                
                                                if (local.CheckDimensions(Convert.ToInt32(tokensReturned[x + polygonParam].value), Convert.ToInt32(tokensReturned[x + (polygonParam + 2)].value)) == false)
                                                {
                                                    validPolyBounds = false;
                                                }
                                                
                                            }

                                            //populate values to pass to the polygon shape to draw
                                            for (int polygonParam = 1; polygonParam < tokensOnLine(tokensReturned, tokensReturned[x].lineNumber); polygonParam += 2)
                                            {
                                                polygonValues[polyCount] = Convert.ToInt32(tokensReturned[x + polygonParam].value);
                                                polyCount++; //increment counter

                                            }

                                            //valid command check
                                            if ((validPolyComma == true) && (validPolyBounds == true))
                                            {
                                                buildShape = parserShapeFactory.getShape("Polygon"); //create shape
                                                //buildShape.Set(local.xCoordinate, local.yCoordinate, 20, 30, 80, 50, 90, 70);
                                                buildShape.Set(polygonValues);
                                                buildShape.Draw(local, sender, drawing); //draws the shape

                                            }
                                            else
                                            {
                                                if (validPolyComma == false)
                                                {
                                                    noParseError("Incorrect call to polygon command. Check syntax for commas on line number " + tokensReturned[x].lineNumber.ToString()); //error message

                                                }
                                                else
                                                {
                                                    noParseError("Incorrect call to polygon command. Check dimensions on line number " + tokensReturned[x].lineNumber.ToString()); //error message

                                                }

                                            }


                                        }
                                        //Exception with converting values to integer or out of bounds
                                        catch(Exception)
                                        {
                                            noParseError("Incorrect type of parameters to polygon on line number " + tokensReturned[x].lineNumber.ToString()); //error message

                                        }



                                    }
                                    else
                                    {
                                        noParseError("Incorrect number of parameters to polygon on line number " + tokensReturned[x].lineNumber.ToString()); //error message

                                    }

                                        



                                    break;

                                case "Colour": //setting the colour
                                    
                                    if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 3) //syntax check
                                    {
                                        if (tokensReturned[x + 1].tokenType.ToString() == "Equals" && tokensReturned[x + 2].tokenType.ToString() == "Identifier")
                                        {
                                            if (local.SetColour(tokensReturned[x + 2].name.ToUpper()) != true) //sets the colour and checks it has been set
                                            {
                                                noParseError("The colour " + tokensReturned[x+2].name + " is not currently supported"); //error message
                                            }
                                        }
                                        else
                                        {
                                            noParseError("Colour set statement not defined properly on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                        }
                                    }
                                    else
                                    {
                                        noParseError("Colour set statement not defined properly on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                    }

                                    i++; //increment line
                                        break;

                                case "Identifier": //Identifier token (this could be a method or variable)

                                    //check if it's a method first
                                    if (methods.ReturnPosition(tokensReturned[x].name.ToUpper()) != -1)
                                    {

                                        //check if method has parameters or not
                                        if (methods.HasParameters(methods.ReturnPosition(tokensReturned[x].name.ToUpper())) == true)
                                        {
                                            List<string> methodParameters = methods.ReturnParameters(methods.ReturnPosition(tokensReturned[x].name.ToUpper())); //gets the methods parameters

                                            //remove commas
                                            List<Token> methodCall = allTokensOnLine(tokensReturned, i);
                                            methodCall.RemoveAll(t => t.tokenType.ToString() == "Comma"); //removes all commas to allow easy comparison
                                            
                                            if ((methodCall.Count - methodParameters.Count) == 3) //checks the correct amount of parameters have been passed in
                                            {
                                                //check if the parameter passed in is identifer or number
                                                List<Token> passedParameters = new List<Token>();
                                                List<int> passedParametersValue = new List<int>();
                                                Boolean validAfterParse = true; //valid method hold variable

                                                for (int step = 2; step < methodCall.Count - 1; step++) {
                                                    if ((methodCall[step].tokenType.ToString() == "Identifier") && (variables.ReturnPosition(methodCall[step].name.ToUpper()) != -1)) //if passed parameter is an identifier
                                                    {
                                                        passedParametersValue.Add(variables.ReturnValue(variables.ReturnPosition(methodCall[step].name.ToUpper()))); //gets the value of the passed parameters and adds to list
                                                    }
                                                    else
                                                    {
                                                        if (methodCall[step].tokenType.ToString() == "IntegerLiteral") //if passed parameter is an integer
                                                        {
                                                            passedParametersValue.Add(Convert.ToInt32(methodCall[step].value)); //adds value to the list
                                                        }
                                                        else
                                                        {
                                                            validAfterParse = false; //invalid parameter passed
                                                        }
                                                    }                                                  
                                                }

                                                if (validAfterParse == true) //all parameters are valid
                                                {

                                                    List<Token> methodBodyCopy = methods.GetMethodDefinition(methods.ReturnPosition(tokensReturned[x].name.ToUpper())); //gets method body
                                                    List<Token> methodBody = new List<Token>(); //empty list to store new tokens which represent the method body

                                                    //full clone of the token list
                                                    for (int listItem = 0; listItem<methodBodyCopy.Count; listItem++)
                                                    {
                                                        Token temp = new Token(methodBodyCopy[listItem].tokenType, methodBodyCopy[listItem].name, methodBodyCopy[listItem].value, methodBodyCopy[listItem].lineNumber, methodBodyCopy[listItem].position, methodBodyCopy[listItem].column);
                                                        methodBody.Add(temp);

                                                    }
                                                    
                                                    List<Token> hold = methods.GetMethodDefinition(methods.ReturnPosition(tokensReturned[x].name.ToUpper()));

                                                    if (passedParametersValue.Count == methodParameters.Count) //checks the amount of parameters line up
                                                    {
                                                        for (int tokenInBody = 0; tokenInBody < methodBody.Count; tokenInBody++) //loops over method body
                                                        {

                                                            if (methodBody[tokenInBody].tokenType.ToString() == "Identifier") //if token encountered is an identifier
                                                            {
                                                                if (methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper()) != -1) //checks identifier exists
                                                                {

                                                                    if (tokensOnLine(methodBody, methodBody[tokenInBody].lineNumber) == 3) //checks the correct amount of tokens are on line
                                                                    {
                                                                        string[] methodIfOperators = {"Plus","Minus","Multiply", "Equals"}; //operators
                                                                      
                                                                        if ((Array.IndexOf(methodIfOperators, methodBody[tokenInBody + 1].tokenType.ToString()) != -1) && (methodBody[tokenInBody + 2].tokenType.ToString() == "IntegerLiteral")) //checks tokens
                                                                        {
                                                                            switch (methodBody[tokenInBody + 1].tokenType.ToString())
                                                                            {
                                                                                case "Plus": //additon
                                                                              
                                                                                    passedParametersValue[methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper())] += Convert.ToInt32(methodBody[tokenInBody + 2].value);
                                                                                    
                                                                                    break;
                                                                                case "Minus": //minus
                                                                                    
                                                                                    passedParametersValue[methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper())] -= Convert.ToInt32(methodBody[tokenInBody + 2].value);
                                                                                    
                                                                                    break;
                                                                                case "Multiply": //multiply
                                                                                    
                                                                                    passedParametersValue[methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper())] *= Convert.ToInt32(methodBody[tokenInBody + 2].value);
                                                                                    
                                                                                    break;
                                                                                case "Equals": //equals
                                                                                    
                                                                                    passedParametersValue[methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper())] = Convert.ToInt32(methodBody[tokenInBody + 2].value);
                                                                                    
                                                                                    break;
                                                                            }

                                                                        }

                                                                        methodBody[tokenInBody].tokenType = Tokens.IntegerLiteral; //changes token type for parse
                                                                        methodBody[tokenInBody].value = passedParametersValue[methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper())].ToString(); //changes token value for parse

                                                                    }
                                                                    else
                                                                    {
                                                                        methodBody[tokenInBody].tokenType = Tokens.IntegerLiteral; //changes token type for parse
                                                                        methodBody[tokenInBody].value = passedParametersValue[methodParameters.IndexOf(methodBody[tokenInBody].name.ToUpper())].ToString(); //changes token value for parse
                                                                    }
                                                                }
                                                            }                                                           
                                                        }
                                                        
                                                        parseTokens(methodBody, sender, drawing, canvasPen); //parse tokens
                                                        
                                                    }
                                                    else
                                                    {
                                                        noParseError("Incorrect number of parameters to method call on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                                    }
                                                }
                                                else
                                                {
                                                    noParseError("Invalid parameter passed to method on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                                }
                                            }
                                            else
                                            {
                                                noParseError("Incorrect number of parameters to method call on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                            }

                                        }
                                        else
                                        {
                                            
                                            List<Token> methodCallTokens = allTokensOnLine(tokensReturned, i); //gets the tokens used to call the method
                                            //System.Diagnostics.Debug.WriteLine(methodCallTokens.Count);
                                            if (methodCallTokens.Count == 3) //if method with no parameters
                                            {
                                                if ((methodCallTokens[0].tokenType.ToString() == "Identifier") && (methodCallTokens[1].tokenType.ToString() == "OpenBracket") && (methodCallTokens[2].tokenType.ToString() == "CloseBracket"))
                                                {
                                                    parseTokens(methods.GetMethodDefinition(methods.ReturnPosition(tokensReturned[x].name.ToUpper())), sender, drawing, canvasPen); //parse the tokens

                                                }
                                                else
                                                {
                                                    noParseError("Method not called properly on line " + tokensReturned[x].lineNumber.ToString()); //error message
                                                }
                                            }
                                            else
                                            {
                                                noParseError("Method not called properly it has no parameters on line " + tokensReturned[x].lineNumber.ToString()); //error message
                                            }
                                            
                                            
                                        }
                                        
                                    }
                                    else //will be a variable
                                    {

                                        if (tokensOnLine(tokensReturned, tokensReturned[x].lineNumber) == 3) //syntax checking
                                        {
                                            if (variables.ReturnPosition(tokensReturned[x].name.ToUpper()) != -1) //check variable exists
                                            {
                                                switch (tokensReturned[x + 1].tokenType.ToString())
                                                {
                                                    case "Equals": variables.Assign(variables.ReturnPosition(tokensReturned[x].name.ToUpper()), Convert.ToInt32(tokensReturned[x + 2].value)); break; //Set equals
                                                    case "Plus": variables.Addition(variables.ReturnPosition(tokensReturned[x].name.ToUpper()), Convert.ToInt32(tokensReturned[x + 2].value)); break; //Set plus
                                                    case "Minus": variables.Subtract(variables.ReturnPosition(tokensReturned[x].name.ToUpper()), Convert.ToInt32(tokensReturned[x + 2].value)); break; //Set minus
                                                    case "Multiply": variables.Multiply(variables.ReturnPosition(tokensReturned[x].name.ToUpper()), Convert.ToInt32(tokensReturned[x + 2].value)); break; //Set multiply
                                                    default: noParseError("Invalid operator on variable on line " + tokensReturned[x].lineNumber.ToString()); break; //Not recognised

                                                }

                                            }
                                            else //new variable created
                                            {
                                                if (tokensReturned[x + 1].tokenType.ToString() == "Equals")
                                                {
                                                    tokensReturned[x].value = tokensReturned[x + 2].value;
                                                    variables.AddVariable(tokensReturned[x]); //add variable to list
                                                }
                                                else
                                                {
                                                    noParseError("Variable doesn't exist on line " + tokensReturned[x].lineNumber.ToString()); //error message
                                                }

                                            }
                                        }
                                    }

                                        break;

                                case "Loop": //loop token

                                    List<Token> loopTokens = new List<Token>(); //List which stores all the tokens in a loop
                                    List<string> operators = new List<string>(){"GreaterThan","Equals","LessThan"}; //operators

                                    Boolean endLoopFound = false; //end loop found check
                                    int loopLineStart = x; //loop start 
                                    int loopSetCounter = x+1; //loop counter

                                    //gets all tokens in the loop
                                    while ((endLoopFound == false) && (loopSetCounter < tokensReturned.Count)){
                                        if (tokensReturned[loopSetCounter].tokenType.ToString() == "EndLoop"){endLoopFound = true;} //end loop found
                                        else{
                                            loopTokens.Add(tokensReturned[loopSetCounter]);
                                            loopSetCounter++;
                                        }
                                    }

                                    //for variable iterator
                                    if ((loopTokens[0].tokenType.ToString() == "While") && (variables.ReturnPosition(loopTokens[1].name.ToUpper()) != -1) && (operators.Contains(loopTokens[2].tokenType.ToString())) && (loopTokens[3].tokenType.ToString() == "IntegerLiteral") ){ //checks the format of the loop
                                       
                                        //checks loop has ended
                                        if (endLoopFound == true){

                                            switch(loopTokens[2].tokenType.ToString()){ //checks the comparison operator of the loop
                                                case "Equals": //equals 
                                                    while (variables.ReturnValue(variables.ReturnPosition(loopTokens[1].name.ToUpper())) == Int32.Parse(loopTokens[3].value)){
                                                        parseTokens(loopTokens, sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                case "GreaterThan": //greater than
                                                    while (variables.ReturnValue(variables.ReturnPosition(loopTokens[1].name.ToUpper())) > Int32.Parse(loopTokens[3].value)){
                                                        parseTokens(loopTokens, sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                case "LessThan": //less than
                                                    while (variables.ReturnValue(variables.ReturnPosition(loopTokens[1].name.ToUpper())) < Int32.Parse(loopTokens[3].value)){
                                                        parseTokens(loopTokens, sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                default:
                                                    noParseError("Unrecognised operator in loop statement " + tokensReturned[x].lineNumber.ToString()); //error message
                                                    break;
                                            }
                                                
                                            i = loopTokens[loopTokens.Count - 1].lineNumber + 2; //update counter

                                        }
                                        else{ //loop not ended
                                            noParseError("Loop not ended on line " + tokensReturned[loopSetCounter-1].lineNumber.ToString()); //error message
                                            i = loopTokens[loopTokens.Count-1].lineNumber+3; //update counter
                                        }
                                    
                                    }

                                    //for integer iterator
                                    else if ((loopTokens[0].tokenType.ToString() == "While") && (loopTokens[1].tokenType.ToString() == "IntegerLiteral") && (operators.Contains(loopTokens[2].tokenType.ToString())) && (loopTokens[3].tokenType.ToString() == "IntegerLiteral"))
                                    {
                                        
                                        //end loop check
                                        if (endLoopFound == true)
                                        {

                                            switch (loopTokens[2].tokenType.ToString()) //switch operator
                                            {
                                                case "Equals": //equals
                                                    while (Int32.Parse(loopTokens[1].value) == Int32.Parse(loopTokens[3].value))
                                                    {
                                                        parseTokens(loopTokens, sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                case "GreaterThan": //greater than
                                                    while (Int32.Parse(loopTokens[1].value) > Int32.Parse(loopTokens[3].value))
                                                    {
                                                        parseTokens(loopTokens, sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                case "LessThan": //less than
                                                    while (Int32.Parse(loopTokens[1].value) < Int32.Parse(loopTokens[3].value))
                                                    {
                                                        parseTokens(loopTokens, sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                default:
                                                    noParseError("Unrecognised operator in loop statement " + tokensReturned[x].lineNumber.ToString()); //error message
                                                    break;
                                            }

                                            i = loopTokens[loopTokens.Count - 1].lineNumber + 2; //update counter

                                        }
                                        else
                                        {
                                            noParseError("Loop not ended on line " + tokensReturned[loopSetCounter - 1].lineNumber.ToString()); //error message
                                            i = loopTokens[loopTokens.Count - 1].lineNumber + 3; //update counter
                                        }

                                    }
                                    else
                                    {
                                        noParseError("Loop not created properly on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                        i = loopTokens[loopTokens.Count - 1].lineNumber + 2; //update counter
                                    }

                                    break;


                                case "If":
                                    List<Token> ifTokens = new List<Token>(); //List which stores all the tokens in an if statement
                                    List<string> ifOperators = new List<string>() { "GreaterThan", "Equals", "LessThan" }; //operators
                                    Boolean multiline = false; //multiline check
                                    int ifSetCounter = x + 1; //if counter

                                    //gets the if tokens 
                                    while ((multiline == false) && (ifSetCounter < tokensReturned.Count))
                                    {
                                        if ((tokensReturned[ifSetCounter].tokenType.ToString() == "EndIf")) { multiline = true; } //end if found
                                        else
                                        {
                                            ifTokens.Add(tokensReturned[ifSetCounter]);
                                            ifSetCounter++;
                                        }
                                    }

                                    //if statement is single line
                                    if (multiline == false)
                                    {
                                        ifTokens = allTokensOnLine(ifTokens, i); //gets the if tokens from the single line
                                        if ((variables.ReturnPosition(ifTokens[0].name.ToUpper()) != -1) && (ifOperators.Contains(ifTokens[1].tokenType.ToString())) && (ifTokens[2].tokenType.ToString() == "IntegerLiteral") && (ifTokens[3].tokenType.ToString() == "Then")) //token checks
                                        {
                                            switch (ifTokens[1].tokenType.ToString()) //operator check
                                            {
                                                case "GreaterThan": //greater than
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) > Int32.Parse(ifTokens[2].value)) //get value
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                case "LessThan": //less than
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) < Int32.Parse(ifTokens[2].value)) //get value
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                case "Equals": //equals
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) == Int32.Parse(ifTokens[2].value)) //get value
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen); //parse tokens
                                                    }
                                                    break;
                                                default:
                                                    noParseError("Unrecognised operator in if statement " + tokensReturned[x].lineNumber.ToString()); //error message
                                                    break;


                                            }
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 1; //update counter
                                        }
                                        else if ((ifTokens[0].tokenType.ToString() == "IntegerLiteral") && (ifOperators.Contains(ifTokens[1].tokenType.ToString())) && (ifTokens[2].tokenType.ToString() == "IntegerLiteral") && (ifTokens[3].tokenType.ToString() == "Then"))
                                        {
                                            //System.Diagnostics.Debug.WriteLine("single line integer comparison");
                                            switch (ifTokens[1].tokenType.ToString())
                                            {
                                                case "GreaterThan": //greater than
                                                    if (Int32.Parse(ifTokens[0].value) > Int32.Parse(ifTokens[2].value))
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "LessThan": //less than
                                                    if (Int32.Parse(ifTokens[0].value) < Int32.Parse(ifTokens[2].value))
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "Equals": //equals
                                                    if (Int32.Parse(ifTokens[0].value) == Int32.Parse(ifTokens[2].value))
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                default:
                                                    noParseError("Unrecognised operator in if statement " + tokensReturned[x].lineNumber.ToString()); //error message
                                                    break;

                                            }
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 1; //update counter

                                        }
                                        else
                                        {
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 1; //update counter
                                        }

                                        
                                    }

                                    //multiline if statement
                                    else
                                    {
                                        //syntax checking for variable comparator
                                        if ((variables.ReturnPosition(ifTokens[0].name.ToUpper()) != -1) && (ifOperators.Contains(ifTokens[1].tokenType.ToString())) && (ifTokens[2].tokenType.ToString() == "IntegerLiteral") && (ifTokens[3].tokenType.ToString() == "Then"))
                                        {
                                            
                                            //operator check
                                            switch (ifTokens[1].tokenType.ToString())
                                            {
                                                case "GreaterThan": //greater than
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) > Int32.Parse(ifTokens[2].value))
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "LessThan": //less than
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) < Int32.Parse(ifTokens[2].value))
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "Equals": //equals
                                                    if (variables.ReturnValue(variables.ReturnPosition(ifTokens[0].name.ToUpper())) == Int32.Parse(ifTokens[2].value))
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                default:
                                                    noParseError("Unrecognised operator in if statement " + tokensReturned[x].lineNumber.ToString()); //error message
                                                    break;

                                            }
                                            
                                            
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 2; //update counter
                                            
                                        }

                                        //syntax checking for integer comparator
                                        else if ((ifTokens[0].tokenType.ToString() == "IntegerLiteral") && (ifOperators.Contains(ifTokens[1].tokenType.ToString())) && (ifTokens[2].tokenType.ToString() == "IntegerLiteral") && (ifTokens[3].tokenType.ToString() == "Then"))
                                        {
                                            //operator check
                                            switch (ifTokens[1].tokenType.ToString())
                                            {
                                                case "GreaterThan": //greater than
                                                    if (Int32.Parse(ifTokens[0].value) > Int32.Parse(ifTokens[2].value))
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "LessThan": //less than
                                                    if (Int32.Parse(ifTokens[0].value) < Int32.Parse(ifTokens[2].value))
                                                    { 
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                case "Equals": //equals
                                                    if (Int32.Parse(ifTokens[0].value) == Int32.Parse(ifTokens[2].value))
                                                    {
                                                        parseTokens(ifTokens.GetRange(4, ifTokens.Count - 4), sender, drawing, canvasPen);
                                                    }
                                                    break;
                                                default:
                                                    noParseError("Unrecognised operator in if statement " + tokensReturned[x].lineNumber.ToString()); //error message
                                                    break;

                                            }
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 2; //update counter

                                        }
                                        else
                                        {
                                            i = ifTokens[ifTokens.Count - 1].lineNumber + 2; //update counter
                                        }
                                    }


                                    break;

                                case "Method": //method definition 
                                    
                                    List<Token> methodHeaderTokens = new List<Token>(); //list to hold header tokens
                                    List<Token> methodBodyTokens = new List<Token>(); //List which stores all the tokens in the method declaration statement
                                    methodHeaderTokens = allTokensOnLine(tokensReturned, i); //gets all tokens on header line
                                    
                                    //check for open close brackets
                                    if ((methodHeaderTokens.FindIndex(m => m.tokenType.ToString() == "OpenBracket") != -1) && (methodHeaderTokens.FindIndex(m => m.tokenType.ToString() == "CloseBracket") != -1))
                                    {
                                        int openBracketIndex = methodHeaderTokens.FindIndex(m => m.tokenType.ToString() == "OpenBracket"); //index of open bracket
                                        int closeBracketIndex = methodHeaderTokens.FindIndex(m => m.tokenType.ToString() == "CloseBracket"); //index of close bracket
                                        List<Token> parameters = methodHeaderTokens.GetRange(openBracketIndex, (closeBracketIndex - openBracketIndex)); //gets parameters
                                        List<string> namedParameters = new List<string>(); //list to hold parameters
                                        Boolean validParameters = true; //parameter check
                                        Boolean methodAlreadyDefined = methods.MethodExists(methodHeaderTokens[1].name.ToUpper()); //checks if method already exists
                                        
                                        //if parameters are present
                                        if (parameters.Count > 1)
                                        {
                                            parameters.RemoveAll(t => t.tokenType.ToString() == "Comma"); //remove all commas from parameter list

                                            //loops over parameter list
                                            for (int p = 1; p < parameters.Count; p++)
                                            {
                                                
                                                if (parameters[p].tokenType.ToString() == "Identifier") //parameter is identifer
                                                {
                                                    namedParameters.Add(parameters[p].name.ToUpper()); //add to list
                                                }
                                                else
                                                {
                                                    validParameters = false; //parameters invalid
                                                    noParseError("Parameter declared in method is not an identifier on line number" + tokensReturned[x].lineNumber.ToString()); //error message
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //method with no parameters
                                        }
                                        Boolean endMethodFound = false; //end method check
                                        
                                        int methodSetCounter = x + methodHeaderTokens.Count; //update counter
                                        
                                        //populates the method body
                                        while ((endMethodFound == false) && (methodSetCounter < tokensReturned.Count))
                                        {
                                            
                                            if (tokensReturned[methodSetCounter].tokenType.ToString() == "EndMethod") { endMethodFound = true; } //end method found
                                            else
                                            {
                                                methodBodyTokens.Add(tokensReturned[methodSetCounter]); //adds token to body definition
                                                methodSetCounter++;
                                            }
                                        }

                                        if (endMethodFound == true && validParameters == true) //method valid
                                        {

                                            if (methodAlreadyDefined == false) //method not already defined
                                            {
                                                methods.AddMethod(methodHeaderTokens[1].name.ToUpper(), namedParameters, methodBodyTokens); //creates the method
                                            }
                                            else
                                            {
                                                noParseError("Method with the same name has already been defined on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                            }
                                        }
                                        else
                                        {
                                            noParseError("Method declaration not ended on line " + tokensReturned[x].lineNumber.ToString()); //error message
                                        }

                                        if (methodBodyTokens.Count == 0) //empty method
                                        {
                                            i = tokensReturned[x].lineNumber + 2; //update counter
                                        }
                                        else
                                        {
                                            i = methodBodyTokens[methodBodyTokens.Count - 1].lineNumber + 2; //update counter
                                        }
                                    }
                                    else
                                    {
                                        noParseError("Syntax error. Method not defined properly on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                    }
                                    break;

                                

                                default: //Token not recognised
                                    if (tokensReturned[x].tokenType.ToString() == "Undefined"){
                                        noParseError("Syntax error. Command not recognised on line number " + tokensReturned[x].lineNumber.ToString()); //error message
                                    }

                                    break;
                                }
                            }
                            //empty line encountered
                            else { foundFirst = true; }                   
                        }
                    }while(foundFirst == false);
                i++; //move to next line
                }    
            }    
        }     
    }

