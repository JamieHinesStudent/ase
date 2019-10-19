using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ase
{
    class TokenizerClass
    {

        private readonly Dictionary<string, Regex> Patterns = new Dictionary<string, Regex>()
    {
        {"StringPattern",   new Regex("^(?i)[A-Z]")},
        {"NumberPattern",   new Regex("[0-9]")   }
     };

        private static readonly Dictionary<string, TokenTypeEnum> KeywordToToken = new Dictionary<string, TokenTypeEnum>(StringComparer.OrdinalIgnoreCase)
        {
            ["clear"] = TokenTypeEnum.ClearKeyword,
            ["reset"] = TokenTypeEnum.ResetKeyword,
            ["circle"] = TokenTypeEnum.CircleKeyword,
            ["rectangle"] = TokenTypeEnum.RectangleKeyword,
            ["triangle"] = TokenTypeEnum.TriangleKeyword,
            ["moveto"] = TokenTypeEnum.MoveToKeyword,
            ["drawto"] = TokenTypeEnum.DrawToKeyword,
        };

        private static readonly Dictionary<string, TokenTypeEnum> SymbolToToken = new Dictionary<string, TokenTypeEnum>(StringComparer.OrdinalIgnoreCase)
        {
            [","] = TokenTypeEnum.CommaSeperator,

        };

        public TokenizerClass(string line)
        {
            if (Enum.IsDefined(typeof(TokenTypeEnum), line))
            {
                Console.WriteLine("Match");
            }
        }

        public bool IsCommand()
        {
            return true;
        }
    }
}
