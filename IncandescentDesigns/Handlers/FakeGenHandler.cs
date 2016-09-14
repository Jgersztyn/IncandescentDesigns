using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace IncandescentDesigns.Handlers
{
    public class FakeGenHandler
    {
        private string item;
        private string program;

        public FakeGenHandler(string message)
        {
            if(message == null)
            {
                throw new ArgumentNullException("message", "Message cannot be null");
            }
            item = message;
            makeProgram();
        }

        public override string ToString()
        {

            return program;
        }

        private void makeProgram()
        {
            program += "#include<stdio.h>\nmain()\n{\nprintf(" + item + ");\n}\n";
        }
    }
}