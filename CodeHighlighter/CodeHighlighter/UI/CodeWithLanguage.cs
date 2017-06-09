using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHighlighter.UI
{
    public struct CodeWithLanguage
    {
        public string Code;
        public string Language;

        public CodeWithLanguage(string code, string language)
        {
            Code = code;
            Language = language;
        }
    }
}
