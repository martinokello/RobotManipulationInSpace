using System;
using System.IO;
using System.Text;

namespace RobotManipulation.Tests
{
    internal class FakeStreamWriter : TextWriter
    {
        public FakeStreamWriter()
        {
        }

        public override Encoding Encoding
        {
            get { return System.Text.ASCIIEncoding.ASCII; }
        }

        public override void WriteLine(string line)
        {
            Console.Out.WriteLine(line);
        }
    }
}