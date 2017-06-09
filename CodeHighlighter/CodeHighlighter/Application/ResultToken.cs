using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeHighlighter.Application {
    public struct ResultToken {
        public string Content;
        public string Type;

        public ResultToken(string content, string type) : this() {
            Content = content;
            Type = type;
        }

        public override bool Equals(object obj) {
            if (obj is ResultToken) {
                var robj = (ResultToken)(obj);
                return Content == robj.Content && Type == robj.Type;
            }
            return false;
        }
    }
}
