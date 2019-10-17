using System;

namespace ase
{
    class Tokenizer
    {
        private string stringRegex = "^(?i)[A-Z]";

        private string integerRegex = "[0-9]";

        private readonly Dictionary<string, Regex> Patterns = new Dictionary<string, Regex>()
    {
        {"StringPattern",   new Regex("^(?i)[A-Z]")},
        {"NumberPattern",   new Regex("[0-9]")   }
     };

        private static readonly Dictionary<string, TokenType> KeywordToToken = new Dictionary<string, TokenType>(StringComparer.OrdinalIgnoreCase)
        {
            ["clear"] = TokenType.ClearKeyword,
            ["reset"] = TokenType.ResetKeyword,
            ["circle"] = TokenType.CircleKeyword,
            ["rectangle"] = TokenType.RectangleKeyword,
            ["triangle"] = TokenType.TriangleKeyword,
            ["moveto"] = TokenType.MoveToKeyword,
            ["drawto"] = TokenType.DrawToKeyword,
        };

        private static readonly Dictionary<string, TokenType> SymbolToToken = new Dictionary<string, TokenType>(StringComparer.OrdinalIgnoreCase)
        {
            [","] = TokenType.CommaSeperator,

        };

        public void Tokenizer(string line)
        {
            if (Enum.IsDefined(typeOf(TokenType), line))
            {
                Console.WriteLine("Match");
            }
        }
    }

}